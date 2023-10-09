using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Game Data/Sound/Sounds Data")]
public class SoundsData : ScriptableObject
{
    public GameObject audioPlayerHolder;
    [Range(0,1f)]
    public float audioVolumeNB;
    [Range(0, 1f)]
    public float audioVolumeB;
    public List<SoundData> soundList = new List<SoundData>();

    public List<SoundData> GetAudios(SoundCategory soundCategory)
    {
        List<SoundData> sounds = new List<SoundData>();
        foreach (SoundData sound in soundList)
        {
            if (sound.soundCategory == soundCategory)
                sounds.Add(sound);
        }

        return sounds;
    }

    public void SetAudioVolumeNB(float audioVolume)
    {
        this.audioVolumeNB = audioVolume;
    }
    public void SetAudioVolumeB(float audioVolume)
    {
        this.audioVolumeB = audioVolume;
    }
}
