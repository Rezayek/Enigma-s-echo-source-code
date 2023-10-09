using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

[RequireComponent(typeof(AudioInputManager))]
[RequireComponent(typeof(GameEventListener))]
[RequireComponent(typeof(AudioEventCaller))]
public class AudioActionObserver : MonoBehaviour
{
    [SerializeField] LayerMask ObserverLayer;
    [SerializeField] Transform rayCaster;
    [Range(0.1f,10f)]
    [SerializeField] float rayDistance;
    [SerializeField] float minDistance;


    private NoBackgroundAudioManager instance;
    private AudioInputManager audioInputManager;
    

    
    void Start()
    {
        instance = GetComponent<NoBackgroundAudioManager>();
        audioInputManager = AudioInputManager.Instance;
    }
    private void Update()
    {
        if(PlayerPrefs.GetString(PlayerPrefsNames.UIActive.ToString()) == "off")
        {
            OnMove();
            OnSprint();
        }
        else
        {
            return;
        }
        
    }

    private ObjectsToObserver RayPreformer()
    {
        Ray ray = new Ray(rayCaster.position, Vector3.down);
        RaycastHit[] hits = Physics.RaycastAll(ray, rayDistance, ObserverLayer);

        // Sort hits based on distance
        Array.Sort(hits, (a, b) => a.distance.CompareTo(b.distance));

        if (!(hits.Length > 0))
            return ObjectsToObserver.None;

        foreach (ObjectsToObserver tag in Enum.GetValues(typeof(ObjectsToObserver)))
        {
            //Debug.Log("tag: " + hits[0].collider.tag + " Distance: " + hits[0].distance);
            if (hits[0].collider.tag == tag.ToString() && hits[0].distance < minDistance)
            {
                //Debug.Log("tag: " + tag);
                return tag;
            }

        }

        
        return ObjectsToObserver.Terrain;
    }

    
    private SoundType1 GetCombination(ObjectsToObserver targetObject, SoundType1 targetSound)
    {
        foreach (SoundType1 sound in SoundType1.GetValues(typeof(SoundType1)))
        {
            if(sound.ToString() == targetSound.ToString() + targetObject.ToString())
            {
                return sound;
            }
        }

        return targetSound;
    }

    private void OnMove()
    {
        ObjectsToObserver currentFootHold = RayPreformer();
        if (audioInputManager.GetMovement() != Vector2.zero && !audioInputManager.PlayerSprint())
            instance.SetCurrentAudio(soundType1: currentFootHold == ObjectsToObserver.None? SoundType1.WalkTerrain : GetCombination(currentFootHold, SoundType1.Walk), stopPrevious: true);
        else
            instance.StopAudio(soundType1: currentFootHold == ObjectsToObserver.None ? SoundType1.WalkTerrain : GetCombination(currentFootHold, SoundType1.Walk));

    }

    private void OnSprint()
    {
        ObjectsToObserver currentFootHold = RayPreformer();



        if (audioInputManager.GetMovement() != Vector2.zero && audioInputManager.PlayerSprint())
            instance.SetCurrentAudio(soundType1: currentFootHold == ObjectsToObserver.None ? SoundType1.RunTerrain : GetCombination(currentFootHold, SoundType1.Run), stopPrevious: true);
        else
            instance.StopAudio(soundType1: currentFootHold == ObjectsToObserver.None ? SoundType1.RunTerrain : GetCombination(currentFootHold, SoundType1.Run));
    }

    public void ExposedAudioObserver(Component sender , List<object> data)
    {
        if( data.Count == 1 && data[0] is SoundType1)
        {
            SoundType1 soundType1 = (SoundType1)data[0];
            instance.SetCurrentAudio(soundType1: soundType1, stopPrevious: false);
        }

    }

    public void ExposedAudioObserverEnemyPlay(Component sender, List<object> data)
    {
        if (data.Count != 3)
            return;
        if (data[0] is not Enemy)
            return;
        if (data[1] is not SoundType1)
            return;
        SoundType1 soundType1 = (SoundType1)data[1];
        AudioClip audioClip = (AudioClip)data[2];
        instance.PlayEnemyAudio(soundType1, audioClip);
    }

    public void ExposedAudioObserverDialog(Component sender, List<object> data)
    {
        if (data.Count != 2)
            return;
        if (data[0] is not SoundType1)
            return;
        SoundType1 soundType1 = (SoundType1)data[0];
        if (soundType1 != SoundType1.NPCDialog)
            return;

        AudioClip audioClip = (AudioClip)data[1];
        instance.PlayNPCDialog(audioClip);
    }
}
