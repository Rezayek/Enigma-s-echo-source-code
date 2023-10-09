using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Game Data/Sound/Detectors Data")]
public class DetectorsData : ScriptableObject
{
    public List<DetectorData> detectorsData;

    public float GetDistance(SoundType2 tag)
    {
        foreach(DetectorData data in detectorsData)
        {
            if (data.DetectorTagName == tag)
                return data.AudioActivationDistance;
        }
        return 0.0f;
    }
}

