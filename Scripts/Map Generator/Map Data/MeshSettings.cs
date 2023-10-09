using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Map/TerrainData")]
public class MeshSettings : UpdateData
{
    public const int numSupportedLevelOfDetails = 5;
    public const int numSupportedChunkSizes = 8;
    public const int numSupportedFlatShadedChunkSizes = 3;
    public static readonly int[] supportedChunkSizes = { 48, 72, 96, 120, 168, 192, 216, 240 };
    public bool useFlatShading;
    [Range(1, 100)] public float meshScale = 1f;

    [Range(0, numSupportedChunkSizes - 1)] [SerializeField] int chunkSizeIndex;
    [Range(0, numSupportedFlatShadedChunkSizes - 1)] [SerializeField] int flatShadedChunkSizeIndex;


    //numberOfVerticesPerLine of mesh rendered at LOD = 0, Includes the 2 extra vertices that are excluded from final mesh, but used for calculating normals
    public int numberOfVerticesPerLine
    {
        get
        {
            return supportedChunkSizes[(useFlatShading) ? flatShadedChunkSizeIndex : chunkSizeIndex] + 5;
        }
    }

    public float meshWorldSize
    {
        get
        {
            return (numberOfVerticesPerLine - 3) * meshScale;
        }
    }
}
