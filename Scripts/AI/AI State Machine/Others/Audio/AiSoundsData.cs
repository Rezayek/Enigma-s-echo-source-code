using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Game Data/Sound/Enemy Sounds Data")]
public class AiSoundsData : ScriptableObject
{
    public Enemy enemy;
    public List<AiSoundData> soundsData;
}
