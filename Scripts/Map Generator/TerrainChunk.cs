using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static MeshGenerator;

public class TerrainChunk
{

	const float colliderGenerationDistanceThreshold = 5;
	public event System.Action<TerrainChunk, bool> onVisibilityChanged;
	public Vector2 coord;

	GameObject meshObject;
	Vector2 sampleCentre;
	Bounds bounds;

	MeshRenderer meshRenderer;
	MeshFilter meshFilter;
	MeshCollider meshCollider;

	LevelOfDetailInfo[] detailsLevels;
	LevelOfDetailsMesh[] lodMeshes;
	int colliderLODIndex;

	HeightMap heightMap;
	bool heightMapReceived;
	int previousLODIndex = -1;
	bool hasSetCollider;
	float maxViewDst;

	HeightMapSettings heightMapSettings;
	MeshSettings meshSettings;
	Transform viewer;

	public TerrainChunk(Vector2 coord, HeightMapSettings heightMapSettings, MeshSettings meshSettings, LevelOfDetailInfo[] detailsLevels, int colliderLODIndex, Transform parent, Transform viewer, Material material)
	{
		this.coord = coord;
		this.detailsLevels = detailsLevels;
		this.colliderLODIndex = colliderLODIndex;
		this.heightMapSettings = heightMapSettings;
		this.meshSettings = meshSettings;
		this.viewer = viewer;

		sampleCentre = coord * meshSettings.meshWorldSize / meshSettings.meshScale;
		Vector2 position = coord * meshSettings.meshWorldSize;
		bounds = new Bounds(position, Vector2.one * meshSettings.meshWorldSize);


		meshObject = new GameObject("Terrain Chunk");
		meshRenderer = meshObject.AddComponent<MeshRenderer>();
		meshFilter = meshObject.AddComponent<MeshFilter>();
		meshCollider = meshObject.AddComponent<MeshCollider>();
		meshRenderer.material = material;

		meshObject.transform.position = new Vector3(position.x, 0, position.y);
		meshObject.transform.parent = parent;
		SetVisible(false);

		lodMeshes = new LevelOfDetailsMesh[detailsLevels.Length];
		for (int i = 0; i < detailsLevels.Length; i++)
		{
			lodMeshes[i] = new LevelOfDetailsMesh(detailsLevels[i].lod);
			lodMeshes[i].updateCallback += UpdateTerrainChunk;
			if (i == colliderLODIndex)
			{
				lodMeshes[i].updateCallback += UpdateCollisionMesh;
			}
		}

		maxViewDst = detailsLevels[detailsLevels.Length - 1].visibleDistanceThreshold;

	}

	public void Load()
	{
		ThreadedDataRequester.RequestData(() => HeightMapGenerator.GenerateHeightMap(meshSettings.numberOfVerticesPerLine, meshSettings.numberOfVerticesPerLine, heightMapSettings, sampleCentre), OnHeightMapReceived);
	}



	void OnHeightMapReceived(object heightMapObject)
	{
		this.heightMap = (HeightMap)heightMapObject;
		heightMapReceived = true;

		UpdateTerrainChunk();
	}

	Vector2 viewerPosition
	{
		get
		{
			return new Vector2(viewer.position.x, viewer.position.z);
		}
	}


	public void UpdateTerrainChunk()
	{
		if (heightMapReceived)
		{
			float viewerDstFromNearestEdge = Mathf.Sqrt(bounds.SqrDistance(viewerPosition));

			bool wasVisible = IsVisible();
			bool visible = viewerDstFromNearestEdge <= maxViewDst;

			if (visible)
			{
				int lodIndex = 0;

				for (int i = 0; i < detailsLevels.Length - 1; i++)
				{
					if (viewerDstFromNearestEdge > detailsLevels[i].visibleDistanceThreshold)
					{
						lodIndex = i + 1;
					}
					else
					{
						break;
					}
				}

				if (lodIndex != previousLODIndex)
				{
					LevelOfDetailsMesh lodMesh = lodMeshes[lodIndex];
					if (lodMesh.hasMesh)
					{
						previousLODIndex = lodIndex;
						meshFilter.mesh = lodMesh.mesh;
					}
					else if (!lodMesh.hasRequestedMesh)
					{
						lodMesh.RequestMesh(heightMap, meshSettings);
					}
				}


			}

			if (wasVisible != visible)
			{

				SetVisible(visible);
				if (onVisibilityChanged != null)
				{
					onVisibilityChanged(this, visible);
				}
			}
		}
	}

	public void UpdateCollisionMesh()
	{
		if (!hasSetCollider)
		{
			float sqrDstFromViewerToEdge = bounds.SqrDistance(viewerPosition);

			if (sqrDstFromViewerToEdge < detailsLevels[colliderLODIndex].sqrVisisbleDistanceThreshold)
			{
				if (!lodMeshes[colliderLODIndex].hasRequestedMesh)
				{
					lodMeshes[colliderLODIndex].RequestMesh(heightMap, meshSettings);
				}
			}

			if (sqrDstFromViewerToEdge < colliderGenerationDistanceThreshold * colliderGenerationDistanceThreshold)
			{
				if (lodMeshes[colliderLODIndex].hasMesh)
				{
					meshCollider.sharedMesh = lodMeshes[colliderLODIndex].mesh;
					hasSetCollider = true;
				}
			}
		}
	}

	public void SetVisible(bool visible)
	{
		meshObject.SetActive(visible);
	}

	public bool IsVisible()
	{
		return meshObject.activeSelf;
	}

}


class LevelOfDetailsMesh
{
    public Mesh mesh;
    public bool hasRequestedMesh;
    public bool hasMesh;

    int lod;
    public event System.Action updateCallback;

    public LevelOfDetailsMesh(int lod)
    {
        this.lod = lod;
    }

    void OnMeshDataRecieved(object meshDataObject)
    {

        mesh = ((MeshData)meshDataObject).CreateMesh();
        hasMesh = true;
        updateCallback();
    }

    public void RequestMesh(HeightMap heightMap, MeshSettings meshSettings)
    {
        hasRequestedMesh = true;
        ThreadedDataRequester.RequestData(
            () => MeshGenerator.GenerateTerrainMesh(
                heightMap: heightMap.values, 
                meshSettings: meshSettings,
                levelOfDetail: lod), 
            OnMeshDataRecieved);

    }

}