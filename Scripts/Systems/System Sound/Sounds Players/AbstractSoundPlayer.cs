using UnityEngine;

public abstract class AbstractSoundPlayer 
{
    // Start is called before the first frame update
    protected AudioClip audioClip { get; set;}
    protected AudioSource audioSource;
    protected float audioVolume;

    public AbstractSoundPlayer(AudioClip clip, AudioSource source, float volume)
    {
        audioClip = clip;
        audioSource = source;
        audioVolume = volume;
    }

    public abstract void PlayAudio(out bool isPlaying);

    public abstract void StopAudio();

    public void SetVolume(float volume)
    {
        audioVolume = volume;
    }
}
