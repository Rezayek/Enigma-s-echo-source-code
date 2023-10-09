using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocationData : MonoBehaviour
{
    [SerializeField] private LocationIds LocationId;
    [SerializeField] private List<Transform> minLocations;
    [SerializeField] private GameEvent wanderingCall;

    private void Start()
    {
        NotifierWanderingState();
    }

    private void OnDestroy()
    {
        NotifierWanderingStateDestroy();
    }

    private void NotifierWanderingState()
    {
        wanderingCall.Raise(this, new List<object> { LocationState.Available, LocationId, minLocations });
    }

    private void NotifierWanderingStateDestroy()
    {
        wanderingCall.Raise(this, new List<object> { LocationState.NoAvailable, LocationId });
    }

    
}
