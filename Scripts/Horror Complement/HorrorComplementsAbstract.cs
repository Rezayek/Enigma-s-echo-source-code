using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorrorComplementsAbstract : MonoBehaviour
{
    [SerializeField] GameObject obj;
    [SerializeField] float distanceToShow;
    [SerializeField] float distanceToHide;
    [SerializeField] int totalActive = 3;
    [Header("Audio")]
    [SerializeField] SoundType1 soundType;
    protected AudioEventCaller audioCaller;
    protected Transform playerTransform;
    protected bool isUsed;


    private float GetDistanceToPlayer()
    {
        if (playerTransform == null )
        {
            Debug.LogWarning("Player or target transform is not assigned!");
            return -1f; // Return a negative value to indicate an error or invalid distance
        }

        return Vector3.Distance(playerTransform.position, transform.position);
    }

    public void PlayerIsNear()
    {
        if (isUsed)
            return; 

        if(totalActive <= 0)
        {
            isUsed = true;
            return;
        }

        float distance = GetDistanceToPlayer();

        //Debug.Log("distance: " + distance + "totalActive :" + totalActive);

        if (distance < distanceToHide)
        {
            HideObj();
            totalActive -= 1;
        }

        
        else if (distanceToHide <= distance && distance <= distanceToShow)
        {
            ShowObj();
            PlayAudio();
            
        }
            
        
            
    }

    public void ShowObj()
    {
        if(!isUsed && !obj.activeSelf)
            obj.SetActive(true);
    }

    public void HideObj()
    {
        if (!isUsed && obj.activeSelf)
            obj.SetActive(false);
            
    }
    public void PlayAudio()
    {
        audioCaller.PlayOnce(soundType);
    }
}
