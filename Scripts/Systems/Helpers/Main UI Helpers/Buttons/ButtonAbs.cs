using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ButtonAbs : MonoBehaviour
{
    public SoundType1 buttonSound = SoundType1.UIButtonTap;
    private AudioEventCaller audioEventCaller;
    private void Awake()
    {
        audioEventCaller = AudioEventCaller.Instance;
    }
    public void PlaySound()
    {
        if(audioEventCaller != null)
            audioEventCaller.PlayOnce(buttonSound);
    }
}
