using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(AudioActionDetector))]
public class BackgroundAudioManager : MonoBehaviour
{

    [SerializeField] SoundsData soundsData;
    [SerializeField] Transform parent;

    private List<SoundData> soundsList;
    private GameObject prefab;
    private float audioVolume;
    private List<GameObject> initAudioHolders = new List<GameObject>();
    private Dictionary<SoundType2, SoundPlayerLoop> soundPlayers = new Dictionary<SoundType2, SoundPlayerLoop>();
    private Dictionary<SoundType2, SoundPlayerLoop> soundPlayersLoop = new Dictionary<SoundType2, SoundPlayerLoop>();
    private bool isPlaying;

    // Start is called before the first frame update
    void Start()
    {
        soundsList = soundsData.GetAudios(soundCategory: SoundCategory.Background);
        prefab = soundsData.audioPlayerHolder;
        audioVolume = soundsData.audioVolumeB;
        foreach (SoundData sound in soundsList)
        {
            GameObject audioHolder = Instantiate(prefab, Vector3.zero, Quaternion.identity, parent);
            initAudioHolders.Add(audioHolder);
            AudioSource audioSource = audioHolder.GetComponent<AudioSource>();
            soundPlayers.Add(sound.soundType2, value: new SoundPlayerLoop(audioClip: sound.audioClip, audioSource: audioSource, audioVolume: audioVolume * sound.volume, loopInterval: sound.loopInterval));
        }
    }

    // Update is called once per frame
    void Update()
    {
        UpdateVolumes();

        foreach (SoundType2 key in soundPlayersLoop.Keys)
        {
            soundPlayersLoop[key].Update();
        }
    }

    public void AddAudioToPlay(SoundType2 soundType2)
    {
        if (soundPlayersLoop.ContainsKey(soundType2))
            return;

        soundPlayersLoop.Add(key: soundType2, value: soundPlayers[soundType2]);
        soundPlayersLoop[soundType2].PlayAudio(out isPlaying);
    }

    public void RemoveAudioToPlay(SoundType2 soundType2)
    {
        if (soundPlayersLoop.ContainsKey(soundType2))
            soundPlayersLoop[soundType2].StopAudio();
            soundPlayersLoop.Remove(soundType2);
    }

    public void DisableAllAudios()
    {

        foreach (SoundType2 key in soundPlayersLoop.Keys)
        {
            soundPlayersLoop[key].StopAudio();
        }
        soundPlayersLoop.Clear();
    }

    public void UpdateVolumes()
    {
        if (PlayerPrefs.GetFloat(PlayerPrefsNames.AudioVolumeB.ToString()) != audioVolume)
        {
            audioVolume = PlayerPrefs.GetFloat(PlayerPrefsNames.AudioVolumeB.ToString());
            soundsData.SetAudioVolumeB(audioVolume: audioVolume);
            int count = 0;
            foreach (SoundType2 key in soundPlayers.Keys)
            {
                soundPlayers[key].SetVolume(audioVolume * soundsList[count].volume);
            }

            DisableAllAudios();

        }
        
    }


    private void OnDestroy()
    {
        for(int i = 0; i < initAudioHolders.Count; i++)
        {
            Destroy(initAudioHolders[i]);   
        }
    }
}
