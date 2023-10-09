using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static MeshGenerator;
using UnityEditor;

public class MapPreview : MonoBehaviour
{



    public enum DrawMode { NoiseMap, ColorMap, Mesh, FalloffMap}
    public bool autoUpdate;


    [SerializeField] DrawMode drawMode;

    public MeshSettings meshSettings;
    public HeightMapSettings heightMapSettings;
    public TextureData textureData;

    public Material terrainMaterial;

    [Range(0, MeshSettings.numSupportedLevelOfDetails - 1)] [SerializeField] int previewEditorLevelOfDetail;

    public Renderer textureRender;
    public MeshFilter meshFilter;
    public MeshRenderer meshRender;
    public MeshCollider meshCollider;


    private void Awake()
    {
        textureData.ApplyToMaterail(terrainMaterial);
        textureData.UpdateMeshHeight(material: terrainMaterial, minHeight: heightMapSettings.minHeight, maxHeight: heightMapSettings.maxHeight);
    }

    public void DrawMapInEditor()
    {
        textureData.ApplyToMaterail(terrainMaterial);
        textureData.UpdateMeshHeight(material: terrainMaterial, minHeight: heightMapSettings.minHeight, maxHeight: heightMapSettings.maxHeight);
        
        HeightMap heightMap = HeightMapGenerator.GenerateHeightMap(width: meshSettings.numberOfVerticesPerLine, height: meshSettings.numberOfVerticesPerLine, settings: heightMapSettings, sampleCenter: Vector2.zero);


        if (drawMode == DrawMode.NoiseMap)
        {
            DrawTexture(texture: TextureGenerator.TextureFromHeightMap(heightMap: heightMap));
        }
        else if (drawMode == DrawMode.Mesh)
        {

            DrawMesh(
            meshData: MeshGenerator.GenerateTerrainMesh(
                heightMap: heightMap.values,
                meshSettings: meshSettings,
                levelOfDetail: previewEditorLevelOfDetail));
  
        }
        else if (drawMode == DrawMode.FalloffMap)
        {
            DrawTexture(texture: TextureGenerator.TextureFromHeightMap(heightMap: new HeightMap(FalloffGenerator.GnerateFalloffMap(size: meshSettings.numberOfVerticesPerLine), 0, 1)));
        }
        
        
    }

 
    


    public void DrawTexture(Texture2D texture)
    {
        
        textureRender.sharedMaterial.mainTexture = texture;
        textureRender.transform.localScale = new Vector3(texture.width, 1, texture.height) / 10f;

        textureRender.gameObject.SetActive(true);
        meshFilter.gameObject.SetActive(false);
    }

    public void DrawMesh(MeshData meshData)
    {
        meshFilter.sharedMesh = meshData.CreateMesh();
        meshCollider.sharedMesh = meshFilter.sharedMesh;
        textureRender.gameObject.SetActive(false);
        meshFilter.gameObject.SetActive(true);
    }

    void OnValuesUpdated()
    {
        if (!Application.isPlaying)
        {
            DrawMapInEditor();
        }
    }

    void OnTextureValueUpdated()
    {
        textureData.ApplyToMaterail(material: terrainMaterial);
    }

    private void OnValidate()
    {
        if (meshSettings != null)
        {
            meshSettings.OnValueUpdated -= OnValuesUpdated;
            meshSettings.OnValueUpdated += OnValuesUpdated;
        }
        if (heightMapSettings != null)
        {
            heightMapSettings.OnValueUpdated -= OnValuesUpdated;
            heightMapSettings.OnValueUpdated += OnValuesUpdated;
        }
        if (textureData != null)
        {
            textureData.OnValueUpdated -= OnTextureValueUpdated;
            textureData.OnValueUpdated += OnTextureValueUpdated;
        }


    }
    #if UNITY_EDITOR
    public void SaveAsPrefab()
    {
        string childNameToSave = "Preview Mesh";

        Transform childTransform = transform.Find(childNameToSave);

        if (childTransform == null)
        {
            Debug.LogError("Child GameObject not found: " + childNameToSave);
            return;
        }

        GameObject childObject = childTransform.gameObject;

        // Create the directory if it doesn't exist
        string directoryPath = "Assets/Prefabs/Maps";
        if (!AssetDatabase.IsValidFolder(directoryPath))
        {
            AssetDatabase.CreateFolder("Assets/Prefabs", "Maps");
        }

        // Generate a unique name for the prefab
        string prefabName = childObject.name + "_Prefab";
        string prefabPath = directoryPath + "/" + prefabName + ".prefab";

        // Save the child GameObject as a prefab and connect the MeshFilter component
        PrefabUtility.SaveAsPrefabAssetAndConnect(childObject, prefabPath, InteractionMode.UserAction);

        // Refresh the Unity editor to make sure the new prefab is visible
        AssetDatabase.Refresh();

        Debug.Log("Child GameObject saved as prefab: " + prefabPath);
    }
    #endif
}








public enum TerrainName
{
    Water,
    Rock,
    Plain,
    Snow,
    Sand
}
