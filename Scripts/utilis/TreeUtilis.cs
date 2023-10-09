using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class TreeUtilis 
{
    MeshSettings meshSettings;
    HeightMapSettings heightMapSettings;

    public TreeUtilis(MeshSettings meshSettings, HeightMapSettings heightMapSettings)
    {
        this.meshSettings = meshSettings;
        this.heightMapSettings = heightMapSettings;
    }

    public float GetMeshRadius(GameObject terrainObj)
    {
        MeshFilter meshFilter = terrainObj.GetComponent<MeshFilter>();
        Mesh mesh = meshFilter.sharedMesh;

        Bounds meshBounds = mesh.bounds;
        Vector3 meshCenter = meshBounds.center;
        Vector3 meshExtents = meshBounds.extents;

        Vector3[] corners = new Vector3[8];

        // Calculate the corners of the bounding box
        corners[0] = meshCenter + new Vector3(meshExtents.x, meshExtents.y, meshExtents.z);
        corners[1] = meshCenter + new Vector3(-meshExtents.x, meshExtents.y, meshExtents.z);
        corners[2] = meshCenter + new Vector3(-meshExtents.x, -meshExtents.y, meshExtents.z);
        corners[3] = meshCenter + new Vector3(meshExtents.x, -meshExtents.y, meshExtents.z);
        corners[4] = meshCenter + new Vector3(meshExtents.x, meshExtents.y, -meshExtents.z);
        corners[5] = meshCenter + new Vector3(-meshExtents.x, meshExtents.y, -meshExtents.z);
        corners[6] = meshCenter + new Vector3(-meshExtents.x, -meshExtents.y, -meshExtents.z);
        corners[7] = meshCenter + new Vector3(meshExtents.x, -meshExtents.y, -meshExtents.z);

        float maxDistance = 0f;

        foreach (Vector3 corner in corners)
        {
            float distance = Vector3.Distance(meshCenter, corner);
            maxDistance = Mathf.Max(maxDistance, distance);
        }

        return maxDistance;
    }

    public float[,] GetHeightMap()
    {
        float[,] heightValues;

        heightValues = HeightMapGenerator.GenerateHeightMap(meshSettings.numberOfVerticesPerLine, meshSettings.numberOfVerticesPerLine, heightMapSettings, Vector2.zero).values;
        if (heightMapSettings.useFalloff)
        {
            float[,] falloffValues = FalloffGenerator.GnerateFalloffMap(size: meshSettings.numberOfVerticesPerLine);
            int x = falloffValues.GetLength(0);
            int y = falloffValues.GetLength(1);

            for (int i = 0; i < x; i++)
            {
                for (int j = 0; j < y; j++)
                {
                    falloffValues[i, j] = 1f - falloffValues[i, j];
                    heightValues[i, j] *= falloffValues[i, j];
                }
            }
        }

        return heightValues;
    }

    public void SelectBounds(out List<int> selectedBounds, float currentPlayerHeight, int desiredBounds, List<float> regionsGlobalHeights)
    {
        selectedBounds = new List<int>();

        if (desiredBounds >= regionsGlobalHeights.Count)
        {
            selectedBounds.AddRange(Enumerable.Range(0, regionsGlobalHeights.Count));
            return;
        }

        int playerHeightIndex = regionsGlobalHeights.FindLastIndex(h => h <= currentPlayerHeight);

        if (playerHeightIndex == -1)
        {
            for (int i = 0; i < desiredBounds; i++)
            {
                selectedBounds.Add(i);
            }
        }
        else if (playerHeightIndex == regionsGlobalHeights.Count - 1)
        {
            int lastIndex = regionsGlobalHeights.Count - 1;
            int startIndex = lastIndex - desiredBounds + 1;
            for (int i = startIndex; i <= lastIndex; i++)
            {
                selectedBounds.Add(i);
            }
        }
        else
        {
            int startIndex = playerHeightIndex - (desiredBounds / 2);
            int endIndex = startIndex + desiredBounds - 1;
            startIndex = Mathf.Clamp(startIndex, 0, regionsGlobalHeights.Count - desiredBounds);
            endIndex = Mathf.Clamp(endIndex, startIndex, regionsGlobalHeights.Count - 1);
            for (int i = startIndex; i <= endIndex; i++)
            {
                selectedBounds.Add(i);
            }
        }
    }

    public int GetCurrentLayers(Vector3 playerPosition, Vector3 terrainPosition, int numOfLayers, float distanceRange)
    {
        for (int i = 0; i < numOfLayers; i++)
        {
            float distanceRangeMultiplier = distanceRange * i;
            if (Vector3.Distance(playerPosition, terrainPosition) <= distanceRange + distanceRangeMultiplier)
            {
                return i;
            }
        }

        return -1;
    }

    public List<HashSet<GameObject>> CreateLayeredObjects(HashSet<GameObject> listTrees, Vector3 terrainPosition, int numOfLayers, float distanceRange)
    {
        List<HashSet<GameObject>> layers = new List<HashSet<GameObject>>(numOfLayers);
        for (int i = 0; i < numOfLayers; i++)
        {
            layers.Add(new HashSet<GameObject>());
        }

        foreach (GameObject obj in listTrees)
        {
            float distance = Vector3.Distance(obj.transform.position, terrainPosition);
            int layerIndex = Mathf.FloorToInt(distance / distanceRange);

            layers[layerIndex].Add(obj);
        }

        return layers;
    }

    public float CalculateGlobalHeight(List<float> treeCoordY)
    {
        float totalHeight = 0f;
        int count = treeCoordY.Count;

        foreach (float height in treeCoordY)
        {
            totalHeight += height;
        }

        return totalHeight / count;
    }
}


