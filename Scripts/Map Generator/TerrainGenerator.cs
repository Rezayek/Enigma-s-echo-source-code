using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static MeshGenerator;

public class TerrainGenerator : MonoBehaviour
{
    
    const float viewerMoveThresholdForChunkUpdate = 25f;
    const float sqrViewerMoveThresholdForChunkUpdate = viewerMoveThresholdForChunkUpdate * viewerMoveThresholdForChunkUpdate;

    public MeshSettings meshSettings;
    public HeightMapSettings heightMapSettings;
    public TextureData textureSettings;

    public int colliderLevelOfDetailIndex;
    public LevelOfDetailInfo[] detailsLevels;
    public Vector2 viewerPosition;

    Vector2 viewerPositionOld;
    public Transform viewer;
    public Material mapMaterial;
    float meshWorldSize;
    int chunksVisibleInViewDistance;

    Dictionary<Vector2, TerrainChunk> terrainChunkDictionary = new Dictionary<Vector2, TerrainChunk>();
    List<TerrainChunk> visisbleTerrainChunks = new List<TerrainChunk>();


    private void Start()
    {
        textureSettings.ApplyToMaterail(material: mapMaterial);
        textureSettings.UpdateMeshHeight(material: mapMaterial, minHeight: heightMapSettings.minHeight, maxHeight: heightMapSettings.maxHeight);
        float maxViewDistance = detailsLevels[detailsLevels.Length - 1].visibleDistanceThreshold;

        meshWorldSize = meshSettings.meshWorldSize;
        chunksVisibleInViewDistance = Mathf.RoundToInt(maxViewDistance / meshWorldSize);


        UpdateVisibelChunks();
    }

    private void Update()
    {
        
        viewerPosition = new Vector2(viewer.position.x, viewer.position.z);

        if(viewerPosition != viewerPositionOld)
        {
            foreach (TerrainChunk chunk in visisbleTerrainChunks)
            {
                chunk.UpdateCollisionMesh();
            }
        }

        if((viewerPositionOld - viewerPosition).sqrMagnitude > sqrViewerMoveThresholdForChunkUpdate)
        {
            viewerPositionOld = viewerPosition;
            UpdateVisibelChunks();
        }

        
    }

    void UpdateVisibelChunks()
    {
        HashSet<Vector2> alreadyUpdatedChunckCoord = new HashSet<Vector2>();

        for(int i = visisbleTerrainChunks.Count - 1; i >=0 ; i--)
        {
            alreadyUpdatedChunckCoord.Add(visisbleTerrainChunks[i].coord);
            visisbleTerrainChunks[i].UpdateTerrainChunk();
        }

        int currentChunckCoordX = Mathf.RoundToInt(viewerPosition.x / meshWorldSize);
        int currentChunckCoordY = Mathf.RoundToInt(viewerPosition.y / meshWorldSize);

        for(int yOffset = -chunksVisibleInViewDistance; yOffset <= chunksVisibleInViewDistance; yOffset++)
        {
            for (int xOffset = -chunksVisibleInViewDistance; xOffset <= chunksVisibleInViewDistance; xOffset++)
            {
                Vector2 viewChunckCoord = new Vector2(currentChunckCoordX + xOffset, currentChunckCoordY + yOffset);
                if(!alreadyUpdatedChunckCoord.Contains(viewChunckCoord))
                {
                    if (terrainChunkDictionary.ContainsKey(viewChunckCoord))
                    {
                        terrainChunkDictionary[viewChunckCoord].UpdateTerrainChunk();
                    }
                    else
                    {
                        TerrainChunk newChunk = new TerrainChunk(
                                coord: viewChunckCoord,
                                heightMapSettings: heightMapSettings,
                                meshSettings: meshSettings,
                                detailsLevels: detailsLevels,
                                colliderLODIndex: colliderLevelOfDetailIndex,
                                parent: transform,
                                viewer: viewer,
                                material: mapMaterial);

                        terrainChunkDictionary.Add(viewChunckCoord, newChunk);
                        newChunk.onVisibilityChanged += onTerrainChunkVisibilityChanged;
                        newChunk.Load();
                    }
                }
            }
        }
    }

    void onTerrainChunkVisibilityChanged(TerrainChunk chunk, bool isVisible)
    {
        if(isVisible)
        {
            visisbleTerrainChunks.Add(chunk);
        }
        else
        {
            visisbleTerrainChunks.Remove(chunk);
        }
    }

}
[System.Serializable]
public struct LevelOfDetailInfo
{
    [Range(0, MeshSettings.numSupportedLevelOfDetails - 1)]
    public int lod;
    public float visibleDistanceThreshold;

    public float sqrVisisbleDistanceThreshold
    {
        get
        {
            return visibleDistanceThreshold * visibleDistanceThreshold;
        }
    }

}