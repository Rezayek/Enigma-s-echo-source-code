using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioTester : MonoBehaviour
{
    // Start is called before the first frame update
    [Header("Events")]
    [SerializeField] GameEvent gameEventAudio;
    [SerializeField] bool playOnce;
    [SerializeField] bool playLoop;


    private void Update()
    {
        if (playOnce)
            PlayOnce();
        if (playLoop)
            PlayLoop(true);
    }


    private void PlayOnce()
    {
        List<object> data = new()
        {
            SoundType1.WalkTerrain
        };
        gameEventAudio.Raise(this, data);
         
        
    }

    private void PlayLoop(bool play)
    {
        List<object> data = new()
        {
            SoundType2.Beach,
            play
        };
        
        
        gameEventAudio.Raise(this, data);

        
    }

}
