using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class HeightMapGenerator 
{
    public static HeightMap GenerateHeightMap(int width, int height, HeightMapSettings settings, Vector2 sampleCenter)
    {
        float[,] values = Noise.GenerateNoiseMap(mapWidth: width, mapHeight: height, settings: settings.noiseSettings, sampleCenter: sampleCenter);

        if (settings.useFalloff)
        {
            float[,] falloffValues = FalloffGenerator.GnerateFalloffMap(size: width);
            int x = values.GetLength(0);
            int y = values.GetLength(1);

            for (int i = 0; i < x; i++)
            {
                for (int j = 0; j < y; j++)
                {
                    falloffValues[i, j] = 1f - falloffValues[i, j];
                    values[i, j] *= falloffValues[i, j];
                }
            }        
        }




        AnimationCurve heightCurve_threadSafe = new AnimationCurve(settings.heightCurve.keys);

        float minValue = float.MaxValue;
        float maxValue = float.MinValue;

        for(int i = 0; i < width; i++)
        {
            for(int j =0; j< height; j++)
            {
                values[i, j] *= heightCurve_threadSafe.Evaluate(values[i, j]) * settings.heightMultiplier;

                if(values[i,j] > maxValue)
                {
                    maxValue = values[i, j];
                }
                if(values[i, j] < minValue)
                {
                    values[i, j] = values[i, j];
                }
            }
        }

        return new HeightMap(values: values, minValue: minValue, maxValue: maxValue);
    }
}


public struct HeightMap
{
    public readonly float[,] values;
    public readonly float minValue;
    public readonly float maxValue;

    public HeightMap(float[,] values, float minValue, float maxValue)
    {
        this.values = values;
        this.minValue = minValue;
        this.maxValue = maxValue;
    }

}