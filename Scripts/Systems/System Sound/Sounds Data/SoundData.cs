using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Game Data/Sound/Sound Data")]
public class SoundData : ScriptableObject
{
    public SoundCategory soundCategory;
    public SoundType1 soundType1;
    public SoundType2 soundType2;
    [Range(0, 1f)]
    public float volume;
    [Range(0, 100f)]
    public float loopInterval;
    public AudioClip audioClip;

}




