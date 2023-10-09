using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Map/Noise Data")]
public class HeightMapSettings : UpdateData
{
    public bool useFalloff;
    [Range(0, 100)] public float heightMultiplier;
    public NoiseSettings noiseSettings;
    public AnimationCurve heightCurve;


    public float minHeight
    {
        get
        {
            return heightMultiplier * heightCurve.Evaluate(0);
        }
    }

    public float maxHeight
    {
        get
        {
            return heightMultiplier * heightCurve.Evaluate(1);
        }
    }
    
}
