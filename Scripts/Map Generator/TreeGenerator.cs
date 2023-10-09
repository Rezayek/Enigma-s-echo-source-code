using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst;
using Unity.Collections;
using Unity.Jobs;
using UnityEngine;
public class TreeGenerator : MonoBehaviour
{
    [SerializeField] int frameSub = 5;
    [SerializeField] private GameObject treesParent;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private MeshSettings meshSettings;
    [SerializeField] private HeightMapSettings heightMapSettings;
    [SerializeField] private TreeSettings treeSettings;
    [SerializeField] GameObject terrainObj;
    [SerializeField] bool useUnityJob = true;
    [SerializeField] int possibleTreesBatchSize = 32;
    [SerializeField] Vector2 xBounds; 
    [SerializeField] Vector2 yBounds;
    private List<List<HashSet<GameObject>>> trees;
    private List<float> regionsGlobalHeights;

    private Camera mainCamera;
    private Transform transformCache;
    private int frameCounter;
    private int bounds;
    private float activationDistance;
    private float colliderActiveDistance;

    private float holeMeshRadius;
    private int numOfLayers;
    private float distanceRange;
    private HashSet<GameObject> selectedTrees = new HashSet<GameObject>();
    private TreeUtilis treeUtilis;

    private void Start()
    {
        treeUtilis = new TreeUtilis(meshSettings: meshSettings, heightMapSettings: heightMapSettings);

        holeMeshRadius = treeUtilis.GetMeshRadius(terrainObj: terrainObj);

        mainCamera = Camera.main;

        numOfLayers = treeSettings.horizantalLayers;

        activationDistance = treeSettings.activationDistance;

        colliderActiveDistance = treeSettings.colliderActiveDistance;

        distanceRange = holeMeshRadius / numOfLayers;

        regionsGlobalHeights = new List<float>();

        bounds = treeSettings.verticalLayers;

        frameCounter = 0;


        if(!useUnityJob)
        {
            trees = DrawTrees();
        }
        else
        {
            StartCoroutine(DrawTreesCoroutine((result) =>
            {
                trees = result;
            }));
        }

        int totalGameObjects = 0;

        foreach (List<HashSet<GameObject>> region in trees)
        {
            foreach (HashSet<GameObject> layer in region)
            {
                totalGameObjects += layer.Count;
            }
        }

        Debug.Log("Total game objects: " + totalGameObjects);




    }

    private void Update()
    {
                
        frameCounter++;

        if (frameCounter % frameSub != 0)
        {
            return;
        }

        if (regionsGlobalHeights.Count == 0)
        {
            return;
        }

        transformCache = transform;

        treeUtilis.SelectBounds(
            selectedBounds: out List<int> selectedBounds,
            currentPlayerHeight: transformCache.position.y,
            desiredBounds: bounds,
            regionsGlobalHeights: regionsGlobalHeights);

        selectedTrees.Clear();

        if (selectedBounds.Count > 0)
        {
            int currentHorizantalLayer = treeUtilis.GetCurrentLayers(
                playerPosition: transformCache.position,
                terrainPosition: terrainObj.transform.position,
                numOfLayers: numOfLayers,
                distanceRange: distanceRange); ;

            bool isBoundaryLayer = currentHorizantalLayer == 0 || currentHorizantalLayer + 1 == numOfLayers;

            foreach (int i in selectedBounds)
            {
                selectedTrees.UnionWith(GetTreesInLayer(i, currentHorizantalLayer));
                if (!isBoundaryLayer)
                {
                    for (int j = 1; j <= treeSettings.horizantalLayersCoef; j++)
                    {
                        int lowerLayer = currentHorizantalLayer - j;
                        int upperLayer = currentHorizantalLayer + j;
                        if (lowerLayer >= 0)
                        {
                            selectedTrees.UnionWith(GetTreesInLayer(i, lowerLayer));
                        }
                        if (upperLayer <= numOfLayers)
                        {
                            selectedTrees.UnionWith(GetTreesInLayer(i, upperLayer));
                        }
                    }

                }
                else if (currentHorizantalLayer == 0)
                {
                    selectedTrees.UnionWith(GetTreesInLayer(i, currentHorizantalLayer));

                    for (int j = 1; j <= treeSettings.horizantalLayersCoef; j++)
                    {
                        selectedTrees.UnionWith(GetTreesInLayer(i, currentHorizantalLayer + j));
                    }
                }
                else if (currentHorizantalLayer + 1 == numOfLayers)
                {
                    selectedTrees.UnionWith(GetTreesInLayer(i, currentHorizantalLayer));
                    for (int j = 1; j <= treeSettings.horizantalLayersCoef; j++)
                    {
                        selectedTrees.UnionWith(GetTreesInLayer(i, currentHorizantalLayer - j));
                    }
                }
            }
        }

        UpdateTreeVisibility(treesToUpdate: selectedTrees);

    }

    private HashSet<GameObject> GetTreesInLayer(int regionIndex, int layerIndex)
    {
        HashSet<GameObject> treesInLayer = new HashSet<GameObject>();

        if (regionIndex >= 0 && regionIndex < trees.Count && layerIndex >= 0 && layerIndex < numOfLayers)
        {
            treesInLayer = trees[regionIndex][layerIndex];
        }

        return treesInLayer;
    }

    private void UpdateTreeVisibility(HashSet<GameObject> treesToUpdate)
    {
        Vector3 currentPosition = transformCache.position;
        Plane[] frustumPlanes = GeometryUtility.CalculateFrustumPlanes(mainCamera);

        foreach (GameObject tree in treesToUpdate)
        {
            bool isTreeActive = tree.activeSelf;

            if (!isTreeActive)
            {
                float distance = Vector3.Distance(currentPosition, tree.transform.position);
                if (distance <= activationDistance && IsTreeVisible(tree, frustumPlanes))
                {
                    tree.SetActive(true);
                }
            }
            else
            {
                float distance = Vector3.Distance(currentPosition, tree.transform.position);
                if (distance > activationDistance)
                {
                    tree.SetActive(false);
                }
                else
                {
                    MeshCollider meshCollider = tree.GetComponent<MeshCollider>();
                    if (meshCollider != null)
                    {
                        meshCollider.enabled = distance < colliderActiveDistance;
                    }
                }
            }
        }
    }

    private bool IsTreeVisible(GameObject tree, Plane[] frustumPlanes)
    {
        Bounds treeBounds = CalculateTreeBounds(tree);
        return GeometryUtility.TestPlanesAABB(frustumPlanes, treeBounds);
    }

    private Bounds CalculateTreeBounds(GameObject tree)
    {
        Bounds bounds = new Bounds();
        Renderer[] renderers = tree.GetComponentsInChildren<Renderer>();

        for (int i = 0; i < renderers.Length; i++)
        {
            if (i == 0)
            {
                bounds = renderers[i].bounds;
            }
            else
            {
                bounds.Encapsulate(renderers[i].bounds);
            }
        }

        return bounds;
    }

    private List<List<HashSet<GameObject>>> DrawTrees()
    {
        float[,] heightValues = treeUtilis.GetHeightMap();

        return GenerateForest(heightValues: heightValues, treeSettings: treeSettings, treesParent: treesParent, indexControl: (int)meshSettings.meshScale);
    }

    private List<List<HashSet<GameObject>>> GenerateForest(float[,] heightValues, TreeSettings treeSettings, GameObject treesParent, int indexControl)
    {


        int width = heightValues.GetLength(0);
        int height = heightValues.GetLength(1);

        List<List<HashSet<GameObject>>> listRegions = new List<List<HashSet<GameObject>>>();

        for (int treeIndex = 0; treeIndex < treeSettings.treeData.Count; treeIndex++)
        {
            TreeData data = treeSettings.treeData[treeIndex];
            float minRange = data.positionRange.min;
            float maxRange = data.positionRange.max;
            HashSet<GameObject> listTrees = new HashSet<GameObject>();
            List<float> treeCoordY = new List<float>();

            for (int x = 0; x < width * indexControl; x++)
            {
                for (int y = 0; y < height * indexControl; y++)
                {
                    int indexX = x / indexControl;
                    int indexY = y / indexControl;
                    float heightValue = heightValues[indexX % width, indexY % height];
                    float randomValue = UnityEngine.Random.value;
                    float combinedProbability = heightValue * (1f - data.treesProbability) + randomValue * data.treesProbability;

                    if (combinedProbability >= treeSettings.nonTreeDensity)
                    {
                        float xRandom = UnityEngine.Random.Range(-data.treeOffset * indexControl, data.treeOffset * indexControl);
                        float yRandom = UnityEngine.Random.Range(-data.treeOffset * indexControl, data.treeOffset * indexControl);

                        if (xBounds.y <= x * xRandom && x * xRandom <= xBounds.x && yBounds.y <= y * yRandom && y * yRandom <= yBounds.x)
                        {
                            Vector3 position = new Vector3(x * xRandom, treeSettings.rayCastHeight, y * yRandom);

                            if (TryRaycastTerrain(position, out Vector3 hitPoint, out float distance))
                            {
                                if (minRange < distance && distance < maxRange)
                                {
                                    float randomScale = UnityEngine.Random.Range(data.scale.min, data.scale.max);
                                    Vector3 scale = new Vector3(randomScale, randomScale, randomScale);

                                    GameObject tree = Instantiate(data.treePrefab, hitPoint, Quaternion.identity, treesParent.transform);
                                    tree.transform.localScale = scale;
                                    tree.SetActive(false);
                                    treeCoordY.Add(hitPoint.y);
                                    listTrees.Add(tree);
                                }
                            }
                        }
                            
                    }
                }
            }

            //NativeArray<RaycastCommand> raycastCommands = new NativeArray<RaycastCommand>();
            regionsGlobalHeights.Add(treeUtilis.CalculateGlobalHeight(treeCoordY: treeCoordY));
            listRegions.Add(
                treeUtilis.CreateLayeredObjects(
                    listTrees: listTrees,
                    terrainPosition: terrainObj.transform.position,
                    numOfLayers: numOfLayers,
                    distanceRange: distanceRange));

        }
        return listRegions;
    }

    private bool TryRaycastTerrain(Vector3 origin, out Vector3 hitPoint, out float distance)
    {
        RaycastHit hit;
        Vector3 raycastOrigin = origin + Vector3.up;
        Vector3 raycastDirection = Vector3.down;

        if (Physics.Raycast(raycastOrigin, raycastDirection, out hit, Mathf.Infinity, layerMask) && hit.collider.CompareTag("Terrain"))
        {
            hitPoint = hit.point;
            distance = hit.distance;
            return true;
        }

        hitPoint = Vector3.zero;
        distance = 0f;
        return false;
    }


    private IEnumerator DrawTreesCoroutine(Action<List<List<HashSet<GameObject>>>> callback)
    {
        yield return StartCoroutine(GenerateForestCoroutine(heightValues: treeUtilis.GetHeightMap(), treeSettings: treeSettings, treesParent: treesParent, indexControl: (int)meshSettings.meshScale, callback: callback));
    }


    private IEnumerator GenerateForestCoroutine(float[,] heightValues, TreeSettings treeSettings, GameObject treesParent, int indexControl, Action<List<List<HashSet<GameObject>>>> callback)
    {

        Vector3 hitPoint;
        float distance;
        List<List<HashSet<GameObject>>> listRegions = new List<List<HashSet<GameObject>>>();

        for (int treeIndex = 0; treeIndex < treeSettings.treeData.Count; treeIndex++)
        {
            TreeData treeData = treeSettings.treeData[treeIndex];

            List<RaycastCommand> raycastList = CalCulatePossiblePosition(
                heightValues: heightValues,
                indexControl: indexControl,
                treeData: treeData,
                rayCastHeight: treeSettings.rayCastHeight,
                nonTreeDensity: treeSettings.nonTreeDensity
                );


            int batchSize = (int)treeData.jobBatchSize;

            float minRange = treeData.positionRange.min;
            float maxRange = treeData.positionRange.max;

            HashSet<GameObject> generatedTrees = new HashSet<GameObject>();

            List<float> treeCoordY = new List<float>();



            Debug.Log("total rays : " + raycastList.Count);
            int numBatches = Mathf.CeilToInt((float)raycastList.Count / batchSize);
            Debug.Log("numBatches : " + numBatches);

            for (int batchIndex = 0; batchIndex < numBatches; batchIndex++)
            {
                int startIndex = batchIndex * batchSize;
                int endIndex = Mathf.Min(startIndex + batchSize, raycastList.Count);

                NativeArray<RaycastCommand> raycastCommands = new NativeArray<RaycastCommand>(endIndex - startIndex, Allocator.TempJob);
                NativeArray<RaycastHit> raycastHits = new NativeArray<RaycastHit>(endIndex - startIndex, Allocator.TempJob);

                for (int i = startIndex; i < endIndex; i++)
                {
                    raycastCommands[i - startIndex] = raycastList[i];


                }

                PerformRaycasts(raycastCommands, raycastHits);

                foreach (RaycastHit hit in raycastHits)
                {
                    hitPoint = hit.point;
                    distance = hit.distance;

                    if (minRange < distance && distance < maxRange && hit.collider.gameObject.tag == "Terrain")
                    {
                        float randomScale = UnityEngine.Random.Range(treeData.scale.min, treeData.scale.max);
                        Vector3 scale = new Vector3(randomScale, randomScale, randomScale);

                        GameObject tree = Instantiate(treeData.treePrefab, hitPoint, Quaternion.identity, treesParent.transform);
                        tree.transform.localScale = scale;
                        tree.SetActive(false);
                        treeCoordY.Add(hitPoint.y);
                        generatedTrees.Add(tree);
                    }
                }

                raycastCommands.Dispose();
                raycastHits.Dispose();
            }


            regionsGlobalHeights.Add(treeUtilis.CalculateGlobalHeight(treeCoordY: treeCoordY));

            listRegions.Add(
                treeUtilis.CreateLayeredObjects(
                    listTrees: generatedTrees,
                    terrainPosition: terrainObj.transform.position,
                    numOfLayers: numOfLayers,
                    distanceRange: distanceRange));

        }
        callback.Invoke(listRegions);
        yield return null;
    }


    private List<RaycastCommand> CalCulatePossiblePosition(float[,] heightValues, int indexControl, TreeData treeData, float rayCastHeight, float nonTreeDensity)
    {
        int width = heightValues.GetLength(0);
        int height = heightValues.GetLength(1);
        NativeArray<float> heightValuesNative = new NativeArray<float>(width * height, Allocator.TempJob);
        NativeQueue<RaycastCommand> raycastCommandsQueue = new NativeQueue<RaycastCommand>(Allocator.TempJob);
        List<RaycastCommand> raycastList = new List<RaycastCommand>();

        // Convert heightValues to NativeArray
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                heightValuesNative[i + j * width] = heightValues[i, j];
            }
        }

        try
        {
            CalculatePossiblePositionJob job = new CalculatePossiblePositionJob
            {
                heightValues = heightValuesNative,
                indexControl = indexControl,
                treeOffset = treeData.treeOffset,
                treesProbability = treeData.treesProbability,
                width = width,
                height = height,
                rayCastHeight = treeSettings.rayCastHeight,
                nonTreeDensity = treeSettings.nonTreeDensity,
                xBounds = xBounds,
                yBounds = yBounds,
                raycastCommandsQueue = raycastCommandsQueue.AsParallelWriter(),
                random = new Unity.Mathematics.Random((uint)UnityEngine.Random.Range(int.MinValue, int.MaxValue)) // Initialize Unity.Mathematics.Random with a seed
            };

            JobHandle jobHandle = job.Schedule(width * height * indexControl * indexControl, possibleTreesBatchSize); // Schedule the job

            jobHandle.Complete(); // Wait for the job to complete

            raycastList = new List<RaycastCommand>(raycastCommandsQueue.Count);

            while (raycastCommandsQueue.TryDequeue(out RaycastCommand raycastCommand))
            {
                raycastList.Add(raycastCommand);
            }
            Debug.Log("possible rays: " + raycastList.Count);

            // Dispose the NativeArrays to release memory
            raycastCommandsQueue.Dispose();// Dispose the NativeArray to release memory
            heightValuesNative.Dispose(); // Dispose the NativeArray to release memory

        }
        catch (Exception e)
        {
            Debug.Log("error: " + e.ToString());
            raycastCommandsQueue.Dispose();// Dispose the NativeArray to release memory
            heightValuesNative.Dispose(); // Dispose the NativeArray to release memory
        }

        return raycastList;

    }

    [BurstCompile]
    private void PerformRaycasts(NativeArray<RaycastCommand> raycastCommands, NativeArray<RaycastHit> raycastHits)
    {
        JobHandle jobHandle = RaycastCommand.ScheduleBatch(raycastCommands, raycastHits, minCommandsPerJob: 500);
        jobHandle.Complete();
    }

    [BurstCompile]
    public struct CalculatePossiblePositionJob : IJobParallelFor
    {
        [ReadOnly] public NativeArray<float> heightValues;
        public int indexControl;
        public int width;
        public int height;
        public float rayCastHeight;
        public float nonTreeDensity;
        public float treeOffset;
        public float treesProbability;
        public Vector2 xBounds;
        public Vector2 yBounds;
        public NativeQueue<RaycastCommand>.ParallelWriter raycastCommandsQueue;
        public Unity.Mathematics.Random random; // Unity.Mathematics.Random


        
        public void Execute(int index)
        {
            int y = index / (width * indexControl);
            int x = index - (y * width * indexControl);

            int indexX = x / indexControl;
            int indexY = y / indexControl;

            float heightValue = heightValues[indexX % width + indexY % height * width];

            float randomValue = random.NextFloat(); // Generate a random value using Unity.Mathematics.Random

            float combinedProbability = heightValue * (1f - treesProbability) + randomValue * treesProbability;

            if (combinedProbability > nonTreeDensity)
            {
                float xRandom = random.NextFloat(-treeOffset * indexControl, treeOffset * indexControl); // Generate random x value
                float yRandom = random.NextFloat(-treeOffset * indexControl, treeOffset * indexControl); // Generate random y value

                if (xBounds.y <= x * xRandom && x * xRandom <= xBounds.x && yBounds.y <= y * yRandom && y * yRandom <= yBounds.x)
                {
                    int commandIndex = index * 2;
                    raycastCommandsQueue.Enqueue(
                        new RaycastCommand(
                            from: new Vector3(x * xRandom, rayCastHeight, y * yRandom),
                            direction: Vector3.down));

                    raycastCommandsQueue.Enqueue(
                        new RaycastCommand(
                            from: new Vector3(x * xRandom, rayCastHeight, y * yRandom),
                            direction: Vector3.up));
                }
            }
        }
    }

}








