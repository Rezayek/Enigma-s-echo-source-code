using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundPlayerLoop : AbstractSoundPlayer
{
    
    public float loopInterval = 3f;

    private float loopTimer = 0f;
    private bool isLooping = false;
    private bool isPlaying;

    public SoundPlayerLoop(AudioClip audioClip, AudioSource audioSource, float audioVolume, float loopInterval) :base(audioClip, audioSource, audioVolume) 
    {
        this.loopInterval = loopInterval;
        audioSource.enabled = false;
    }



    public void Update()
    {
        if (isLooping)
        {
            loopTimer += Time.deltaTime;
            if (loopTimer >= loopInterval)
            {
                loopTimer = 0f;
                PlayAudio(out isPlaying);
            }
        }
    }

    public override void PlayAudio(out bool isplaying)
    {
        if (audioSource.isPlaying)
        {
            Debug.LogWarning("Audio is already playing.");
            isplaying = true;
            return;
        }
        if (!audioSource.isActiveAndEnabled)
            audioSource.enabled = true;

        isplaying = false;
        audioSource.clip = audioClip;
        audioSource.volume = audioVolume;
        audioSource.loop = true;
        audioSource.Play();

        if (!isLooping)
        {
            isLooping = true;
            loopTimer = 0f;
        }
    }

    public override void StopAudio()
    {
        if (!audioSource.isPlaying)
        {
            if (audioSource.isActiveAndEnabled)
                audioSource.enabled = false;

            Debug.LogWarning("No audio is currently playing.");
            return;
        }

        audioSource.Stop();
        audioSource.clip = null;
        audioSource.enabled = false;

        if (isLooping)
        {
            isLooping = false;
            loopTimer = 0f;
        }
    }
}
