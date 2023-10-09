using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Noise 
{
    public enum NormalizeMode { Local, Global}

    public static float[,] GenerateNoiseMap(int mapWidth, int mapHeight, NoiseSettings settings, Vector2 sampleCenter)
    {
        float[,] noiseMap = new float[mapWidth, mapHeight];

        System.Random prng = new System.Random(settings.seed);
        Vector2[] octavesOffSets = new Vector2[settings.octaves];

        float maxPossibleHeight = 0;
        float amplitude = 1;
        float frequency = 1;

        for (int i = 0; i < settings.octaves; i++)
        {
            float offsetX = prng.Next(-100000, 100000) + settings.offset.x + sampleCenter.x;
            float offsetY = prng.Next(-100000, 100000) - settings.offset.y - sampleCenter.y;

            octavesOffSets[i] = new Vector2(offsetX, offsetY);

            maxPossibleHeight += amplitude;
            amplitude *= settings.presistance;
        }


        float maxLocalNoiseHeight = float.MinValue;
        float minLocelNoiseHeight = float.MaxValue;

        float halfWidth = mapWidth / 2;
        float halfHeight = mapHeight / 2;



        for (int y = 0; y < mapHeight; y++)
        {
            for(int x = 0; x < mapWidth; x++)
            {
                amplitude = 1;
                frequency = 1;
                float noiseHeight = 0;

                for(int i = 0; i < settings.octaves; i++)
                {
                    float sampleX = (x - halfWidth + octavesOffSets[i].x) / settings.scale * frequency ;
                    float sampleY = (y - halfHeight + octavesOffSets[i].y) / settings.scale * frequency ;

                    float perlinValue = Mathf.PerlinNoise(sampleX, sampleY) * 2 - 1;
                    noiseHeight += perlinValue * amplitude;

                    amplitude *= settings.presistance;
                    frequency *= settings.lacunarity;

                }

                if(noiseHeight > maxLocalNoiseHeight)
                {
                    maxLocalNoiseHeight = noiseHeight;
                }

                if (noiseHeight < minLocelNoiseHeight)
                {
                    minLocelNoiseHeight = noiseHeight;
                }
                noiseMap[x, y] = noiseHeight;


                if(settings.normalizeMode == NormalizeMode.Global)
                {
                    float normalizedHeight = (noiseMap[x, y] + 1) / (2f * maxPossibleHeight / 1.75f);
                    noiseMap[x, y] = Mathf.Clamp(normalizedHeight, 0, int.MaxValue);
                }

            }
        }


        if (settings.normalizeMode == NormalizeMode.Local)
        {
            for (int y = 0; y < mapHeight; y++)
            {
                for (int x = 0; x < mapWidth; x++)
                {

                    noiseMap[x, y] = Mathf.InverseLerp(minLocelNoiseHeight, maxLocalNoiseHeight, noiseMap[x, y]);

                }
            }
        }

           

                return noiseMap;
    }
}

[System.Serializable]
public class NoiseSettings
{
    public Noise.NormalizeMode normalizeMode;

    [Range(0, 1000)] public float scale = 50;
    [Range(0, 100)] public int octaves = 1;
    [Range(0, 1)] public float presistance = 0.5f;
    [Range(0, 100)] public float lacunarity = 0.7f;
    [Range(0, 100000)] public int seed;
    public Vector2 offset;

    public void ValidateValues()
    {
        scale = Mathf.Max(scale, 0.01f);
        octaves = Mathf.Max(octaves, 1);
        lacunarity = Mathf.Max(lacunarity, 1);
        presistance = Mathf.Clamp01(presistance);
    }
}