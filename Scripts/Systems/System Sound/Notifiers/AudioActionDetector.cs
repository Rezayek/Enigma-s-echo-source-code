using UnityEngine;
using System.Collections.Generic;
using System.Linq;

[RequireComponent(typeof(GameEventListener))]
[RequireComponent(typeof(AudioEventCaller))]
public class AudioActionDetector : MonoBehaviour
{
    [SerializeField] private DetectorsData detectorsData;
    [SerializeField] private Transform playerTransform;
    [SerializeField] private SoundType2 selectedOptions;

    private SoundType2 SelectedOptions
    {
        get { return selectedOptions; }
        set { selectedOptions = value; }
    }

    private Dictionary<SoundType2, List<Transform>> detectorsGroup = new Dictionary<SoundType2, List<Transform>>();
    private BackgroundAudioManager instance;

    private bool soundFree;

    // Start is called before the first frame update
    void Start()
    {
        instance = GetComponent<BackgroundAudioManager>();
        soundFree = false;
        CollectDetectors();
    }

    // Update is called once per frame
    void Update()
    {

        if (PlayerPrefs.GetString(PlayerPrefsNames.UIActive.ToString()) == "off")
        {
            if (soundFree)
            {
                soundFree = false;
                DetecteUI();
            }

            GlobalDetector();
            DetecteWind(maxDistance: detectorsData.GetDistance(tag: SoundType2.Wind));
            DetecteBeach(maxDistance: detectorsData.GetDistance(tag: SoundType2.Beach));
        }
        else
        {
            if(!soundFree)
            {
                soundFree = true;
                DetecteUI();
            }
                
            instance.AddAudioToPlay(soundType2: SoundType2.MenuMusic);
        }

            
    }

    private void CollectDetectors()
    {
        List<DetectorData> detectorData = detectorsData.detectorsData;
        foreach(DetectorData detector in detectorData)
        {
            detectorsGroup.Add(key: detector.DetectorTagName, value: GameObject.FindGameObjectsWithTag(detector.DetectorTagName.ToString()).Select(go => go.transform).ToList<Transform>());
        }
    }

    private void DetecteWind(float maxDistance)
    {
        bool isActive = false;

        foreach (Transform t in detectorsGroup[SoundType2.Wind])
        {
            float distance = Vector3.Distance(playerTransform.position, t.position);

            if (distance < maxDistance)
            {
                isActive = true;
                break;
            }
     
        }

        if (isActive)
        {
            instance.AddAudioToPlay(soundType2: SoundType2.Wind);
        }

        else
        {
            instance.RemoveAudioToPlay(soundType2: SoundType2.Wind);
        }

    }

    private void DetecteBeach(float maxDistance)
    {
        bool isActive = false;

        foreach (Transform t in detectorsGroup[SoundType2.Beach])
        {
            float distance = Vector3.Distance(playerTransform.position, t.position);
            if (distance < maxDistance)
            {
                isActive = true;
                break;
            }
        }

        if (isActive)
        {
            instance.AddAudioToPlay(soundType2: SoundType2.Beach);
        }
        else
        {
            instance.RemoveAudioToPlay(soundType2: SoundType2.Beach);
        }

    }

    private void GlobalDetector()
    {
        foreach (SoundType2 option in System.Enum.GetValues(typeof(SoundType2)))
        {
            if ((selectedOptions & option) != 0)
            {
                bool isActive = false;

                foreach (Transform t in detectorsGroup[option])
                {
                    float distance = Vector3.Distance(playerTransform.position, t.position);
                    if (distance < detectorsData.GetDistance(tag: option))
                    {
                        isActive = true;
                        break;
                    }
                }

                if (isActive)
                {
                    instance.AddAudioToPlay(soundType2: option);
                }
                else
                {
                    instance.RemoveAudioToPlay(soundType2: option);
                }
            }
        }
    }

    private void DetecteUI()
    {
        instance.DisableAllAudios();
    }

    public void ExposedAudioDetector(Component sender, List<object> data)
    {

        if (data.Count == 2)
        {
            SoundType2 soundType2 = (SoundType2)data[0];
            bool activate = (bool)data[1];
            if (activate)
                instance.AddAudioToPlay(soundType2: soundType2);
            else
                instance.RemoveAudioToPlay(soundType2: soundType2);
        }

    }
}


