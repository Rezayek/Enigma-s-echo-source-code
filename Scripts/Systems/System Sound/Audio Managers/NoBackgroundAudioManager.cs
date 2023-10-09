using System.Collections.Generic;
using UnityEngine;
using System;

[RequireComponent(typeof(AudioActionObserver))]
public class NoBackgroundAudioManager : MonoBehaviour
{
    [SerializeField] SoundsData soundsData;
    [SerializeField] Transform parent;

    private List<SoundData> soundsList;
    private GameObject prefab;
    private float audioVolume;
    private List<GameObject> initAudioHolders = new List<GameObject>();
    private Dictionary<SoundType1, SoundPlayerOnce> soundPlayers = new Dictionary<SoundType1, SoundPlayerOnce>();
    private SoundType1 soundType1 = SoundType1.None;
    private SoundType1 soundType1Previous = SoundType1.None;
    private bool stopPrevious = false;
    private bool isPlaying = false;
    private AudioClip enemyPreviousAudio;
    // Start is called before the first frame update
    void Start()
    {
        soundsList = soundsData.GetAudios(soundCategory: SoundCategory.NoBackground);
        prefab = soundsData.audioPlayerHolder;
        audioVolume = soundsData.audioVolumeNB;
        foreach (SoundData sound in soundsList)
        {
            GameObject audioHolder = Instantiate(prefab, Vector3.zero, Quaternion.identity, parent);
            initAudioHolders.Add(audioHolder);
            AudioSource audioSource = audioHolder.GetComponent<AudioSource>();
            soundPlayers.Add(sound.soundType1, value: new SoundPlayerOnce(audioClip: sound.audioClip, audioSource: audioSource, audioVolume: audioVolume * sound.volume));
        }
        SetDailgoPlayerComponent();
        SetEnemyAudioComponent();

    }

    private void  SetDailgoPlayerComponent()
    {
        GameObject audioHolder = Instantiate(prefab, Vector3.zero, Quaternion.identity, parent);
        initAudioHolders.Add(audioHolder);
        AudioSource audioSource = audioHolder.GetComponent<AudioSource>();
        soundPlayers.Add(SoundType1.NPCDialog, value: new SoundPlayerOnce(audioClip: null, audioSource: audioSource, audioVolume: audioVolume * 0.7f));
    }

    private void SetEnemyAudioComponent()
    {
        // Get all values of the MyEnum enum
        Enemy[] enumValues = (Enemy[]) Enum.GetValues(typeof(Enemy));

        // Loop through the enum values
        foreach (Enemy enemy in enumValues)
        {
            GameObject audioHolder = Instantiate(prefab, Vector3.zero, Quaternion.identity, parent);
            initAudioHolders.Add(audioHolder);
            AudioSource audioSource = audioHolder.GetComponent<AudioSource>();
            soundPlayers.Add((SoundType1)Enum.Parse(typeof(SoundType1), enemy.ToString()), value: new SoundPlayerOnce(audioClip: null, audioSource: audioSource, audioVolume: audioVolume));
        }

    }

    // Update is called once per frame
    void Update()
    {
        UpdateVolumes();

        if (soundType1 == SoundType1.None)
            return;

        if (stopPrevious && soundType1Previous != soundType1 && soundType1Previous != SoundType1.None)
        {
            soundPlayers[soundType1Previous].StopAudio();
            soundType1Previous = soundType1;
        }
            
        soundPlayers[soundType1].PlayAudio(out isPlaying); 
        soundType1 = SoundType1.None;

    }

    public void PlayNPCDialog(AudioClip audio)
    {
        soundPlayers[SoundType1.NPCDialog].StopAudio();
        soundPlayers[SoundType1.NPCDialog].SetAudioClip(audio);
        soundPlayers[SoundType1.NPCDialog].PlayAudio(out isPlaying);
    }

    public void PlayEnemyAudio(SoundType1 enemy, AudioClip audio)
    {
        soundPlayers[enemy].PlayAudio(out isPlaying);
        if (enemyPreviousAudio != audio && isPlaying)
        {
            soundPlayers[enemy].StopAudio();
            soundPlayers[enemy].SetAudioClip(audio);
            soundPlayers[enemy].PlayAudio(out isPlaying);
            enemyPreviousAudio = audio;
        }
        else
        {
            soundPlayers[enemy].SetAudioClip(audio);
            soundPlayers[enemy].PlayAudio(out isPlaying);
        }
    }

    public void StopEnemyAudio(SoundType1 enemy)
    {
        soundPlayers[enemy].StopAudio();
    }

    public void SetCurrentAudio(SoundType1 soundType1, bool stopPrevious)
    {
        this.soundType1 = soundType1;
        this.stopPrevious = stopPrevious;
    }

    public void StopAudio(SoundType1 soundType1)
    {
        soundPlayers[soundType1].StopAudio();
    }

    public void UpdateVolumes()
    {
        if(PlayerPrefs.GetFloat(PlayerPrefsNames.AudioVolumeNB.ToString()) != audioVolume)
        {
            audioVolume = PlayerPrefs.GetFloat(PlayerPrefsNames.AudioVolumeNB.ToString());
            soundsData.SetAudioVolumeNB(audioVolume: audioVolume);
            int count = 0;
            foreach(SoundType1 key in soundPlayers.Keys)
            {
                if(soundPlayers.ContainsKey(key))
                {
                    soundPlayers[key].SetVolume(audioVolume * soundsList[count].volume);
                }
                
            }
        }
    }

    private void OnDestroy()
    {
        for (int i = 0; i < initAudioHolders.Count; i++)
        {
            Destroy(initAudioHolders[i]);

        }
    }
}
