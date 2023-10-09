using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioEventCaller : GenericSingleton<AudioEventCaller>
{
    // Start is called before the first frame update
    [Header("Events")]
    [SerializeField] private GameEvent gameEventAudio;

    public void PlayOnce(SoundType1 soundType1)
    {
        List<object> data = new()
        {
            soundType1
        };
        gameEventAudio.Raise(this, data);
    }

    public void PlayOnceDialog(SoundType1 soundType1, AudioClip audioClip)
    {
        List<object> data = new()
        {
            soundType1,
            audioClip,
        };
        gameEventAudio.Raise(this, data);
    }

    public void PlayLoop(SoundType2 soundType2, bool play)
    {
        List<object> data = new()
        {
            soundType2,
            play
        };
        gameEventAudio.Raise(this, data);
    }
}
