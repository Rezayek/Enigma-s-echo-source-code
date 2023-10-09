using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    [SerializeField] private GameEvent checkpointNotify;
    [SerializeField] private Transform spawnPosition;



    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            checkpointNotify.Raise(this, new List<object> {spawnPosition});
        }
    }
}
