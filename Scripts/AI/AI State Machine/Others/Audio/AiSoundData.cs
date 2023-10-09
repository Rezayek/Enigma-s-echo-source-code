using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Game Data/Sound/Enemy Sound Data")]
public class AiSoundData : ScriptableObject
{
    public EnemySounds enemySounds;
    public AudioClip audioClip;
}
