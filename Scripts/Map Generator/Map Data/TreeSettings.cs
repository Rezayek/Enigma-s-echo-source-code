using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Map/Tree Data")]
public class TreeSettings : ScriptableObject
{

    [Range(0, 1000)] public float activationDistance;
    [Range(0, 100)] public float colliderActiveDistance;
    public int verticalLayers;

    public int horizantalLayers;
    public int horizantalLayersCoef;
    public List<TreeData> treeData;

    [Range(1, 1000)] public float rayCastHeight;
    [Range(0, 1)] public float nonTreeDensity;



}


[System.Serializable]
public class TreeData
{
    public GameObject treePrefab;
    [MinMaxSlider(0f, 300f)] public FloatRange positionRange;
    [MinMaxSlider(0f, 100f)] public FloatRange scale;
    [Range(0, 50)] public float treeOffset;
    [Range(0, 1)] public float treesProbability;
    [Range(10, 10000)] public float jobBatchSize;
}