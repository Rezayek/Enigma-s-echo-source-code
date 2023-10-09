using System;
using System.Collections.Generic;
using UnityEngine;

public class AISoundManager : MonoBehaviour
{
    [SerializeField] private Enemy enemy;
    [SerializeField] private SoundType1 soundType;
    [SerializeField] private GameEvent audioExternalEvent;
    [SerializeField] private AiSoundsData sounds;

    public void PlaySound(EnemySounds enemySounds)
    {

        foreach(AiSoundData s in sounds.soundsData)
        {
            if(s.enemySounds == enemySounds)
            {
                audioExternalEvent.Raise(this, new List<object> { enemy, soundType, s.audioClip });
            }
        }
    }
}
