using UnityEngine;

public class SoundPlayerOnce: AbstractSoundPlayer
{
    public SoundPlayerOnce(AudioClip audioClip, AudioSource audioSource, float audioVolume) :base(audioClip, audioSource, audioVolume)
    {
        audioSource.enabled = false;
    }

    public void SetAudioClip(AudioClip audioClip)
    {
        this.audioClip = audioClip;
    } 

    public override void PlayAudio(out bool isPlaying)
    {
        if (audioSource.isPlaying)
        {
            Debug.LogWarning("Audio is already playing.");
            isPlaying = true;
            return;
        }

        if(!audioSource.isActiveAndEnabled)
            audioSource.enabled = true;

        isPlaying = false;
        audioSource.clip = audioClip;
        audioSource.volume = audioVolume;
        audioSource.Play();
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
    }
}

