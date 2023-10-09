using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
[CreateAssetMenu(menuName = "Map/Texture Data")]
public class TextureData : UpdateData
{
    const int textureSize = 512;
    const TextureFormat textureFormat = TextureFormat.RGB565;

    public Layer[] layers;
    float savedMinHeight;
    float savedMaxHeight;
    public void ApplyToMaterail(Material material)
    {
        material.SetInt("layerCount", layers.Length);
        material.SetColorArray("baseColors", layers.Select(x => x.tint).ToArray());
        material.SetFloatArray("baseStartHeights", layers.Select(x => x.startHeight).ToArray());
        material.SetFloatArray("baseBlends", layers.Select(x => x.blendStrength).ToArray());
        material.SetFloatArray("baseColorsStrength", layers.Select(x => x.tintStrength).ToArray());
        material.SetFloatArray("baseTextureScales", layers.Select(x => x.textureScale).ToArray());
        Texture2DArray texturesArray = GenerateTextureArray(textures: (layers.Select(x => x.texture).ToArray()));
        material.SetTexture("baseTextures", texturesArray);
        //Texture2DArray normalMapsArray = GenerateTextureArray(textures: layers.Select(x => x.normalMap).ToArray());
        //material.SetTexture("baseNormalMaps", normalMapsArray);
        UpdateMeshHeight(material: material, minHeight: savedMinHeight, maxHeight: savedMaxHeight);
    }


    public void UpdateMeshHeight(Material material, float minHeight, float maxHeight)
    {
        savedMinHeight = minHeight; 
        savedMaxHeight = maxHeight;
        material.SetFloat("minHeight", minHeight);
        material.SetFloat("maxHeight", maxHeight);
    }

    Texture2DArray GenerateTextureArray(Texture2D[] textures)
    {
        Texture2DArray textureArray = new Texture2DArray(textureSize, textureSize, textures.Length, textureFormat, true);

        for (int i = 0; i< textures.Length; i ++)
        {
            textureArray.SetPixels(textures[i].GetPixels(), i);
        }
        textureArray.Apply();
        return textureArray;
    }

    [System.Serializable]
    public class Layer
    {
        public Texture2D texture;
        //public Texture2D normalMap;
        public Color tint;
        [Range(0, 1)]
        public float tintStrength;
        [Range(0, 1)]
        public float startHeight;
        [Range(0, 1)]
        public float blendStrength;
        public float textureScale;

    }
}
