using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "State/Enemies/Banshee/Banshee State Wandering")]
public class BansheeStateWandering : AbstractState
{
    [SerializeField] private float minDistanceBeforeSwitch = 5;
    [SerializeField] private float minDistanceToPlayer = 30;
    [SerializeField] private float minDistanceSound = 100;
    [SerializeField] private GameEvent frenzyModeCall;
    [SerializeField] private float errorFixDurationCheck = 10;

    private enum WanderingInnerState
    {
        None,
        OffWandering,
        OnWandering,
    }

    private List<Locations> locations;
    private List<Transform> minLocations;
    private WanderingInnerState wanderingInnerState;
    private int currentLocation;
    private int currentTargetIndex = 0;
    private Vector3 lastPosition;
    private float lastLocationCheckTime;

    public override void OnEnterState(
        FiniteStateMachine stateMachine,
        States previousState,
        AISoundManager soundManager,
        AgentMovements agentMovements,
        Transform playerTransform,
        Transform agentTransform,
        AIUtils helper,
        out bool update)
    {
        this.stateMachine = stateMachine;
        this.previousState = previousState;
        this.soundManager = soundManager;
        this.agentMovements = agentMovements;
        this.playerTransform = playerTransform;
        this.agentTransform = agentTransform;
        this.helper = helper;

        
        wanderingInnerState = WanderingInnerState.None;
        currentTargetIndex = 0;
        currentLocation = 0;
        lastLocationCheckTime = 0.0f;
        stateMachine.CallAniamtorPlay(animation);
        update = true;
        
    }

    public override void UpdateState()
    {
        //Debug.Log("wanderingInnerState: " + wanderingInnerState);
        switch (wanderingInnerState)
        {
            case WanderingInnerState.None:
                DecideInnerState();
                break;
            case WanderingInnerState.OnWandering:
                OnDestinationReached();
                break;
            case WanderingInnerState.OffWandering:
                StartTheHunt();
                break;
            
        }

        CallAudio();
    }

    public override void OnExitState()
    {
        stateMachine.CallAniamtorStop(animation);
        minLocations.Clear();
    }

    private int SelectLocation()
    {
        if (locations.Count == 0)
            return -1;
        
        return Random.Range(0, locations.Count);
    }

    private void DecideInnerState()
    {

        currentLocation = SelectLocation();
        //Debug.Log("currentLocation: " + currentLocation);
        if (currentLocation == -1)
        {
            wanderingInnerState = WanderingInnerState.OffWandering;
        }
        else
        {
            minLocations =  new List<Transform>(locations[currentLocation].minLocations);
            currentTargetIndex = 0;
            wanderingInnerState = WanderingInnerState.OnWandering;
        }
    }

    private void OnDestinationReached()
    {
        if (currentTargetIndex >= minLocations.Count)
        {
            //Debug.Log("currentTargetIndex(OffWandering): " + currentTargetIndex + " minLocations.Count(OffWandering): " + minLocations.Count + " Locations: " + locations.Count + " currentLocation : " + currentLocation + " Tag: " + locations[currentLocation].locationId);
            wanderingInnerState = WanderingInnerState.OffWandering;
        }
        else
        {
            MoveToNextLocation(minLocations[currentTargetIndex]);

            //Debug.Log("currentTargetIndex(OnWandering): " + currentTargetIndex + " minLocations.Count(OnWandering): " + minLocations.Count);

            if (helper.WithInRange(agentTransform.position, playerTransform.position, minDistanceToPlayer))
            {
                stateMachine.SwitchState(States.Frenzy, state);
            }
            else if (helper.WithInRange(agentTransform.position, locations[currentLocation].minLocations[currentTargetIndex].position, minDistanceBeforeSwitch))
            {
                currentTargetIndex += 1;
            }

            CheckLocationChange();
        }
        
    }
    private void StartTheHunt()
    {
        if (SelectLocation() == -1)
        {
            stateMachine.SwitchState(States.Hunt, state);
        }
        else
        {
            wanderingInnerState = WanderingInnerState.None;
        }
        
    }

    private void CheckLocationChange()
    {
        if (Time.time - lastLocationCheckTime >= errorFixDurationCheck)
        {
            lastLocationCheckTime = Time.time;

            if (Vector3.Distance(agentTransform.position, lastPosition) < 0.1f)
            {
                wanderingInnerState = WanderingInnerState.OffWandering;
            }

            lastPosition = agentTransform.position;
        }
    }

    public override void CallAudio()
    {
        if(helper.WithInRange(agentTransform.position, playerTransform.position, minDistanceSound))
        {
            soundManager.PlaySound(stateAudio);
        }
        
    }

    private void MoveToNextLocation(Transform target)
    {
        agentMovements.SetDistination(target.position);
    }

    public void FrenzyModeListener(Component sender, List<object> data)
    {
        if (data.Count != 1)
            return;
        if (data[0] is not States.Frenzy)
            return;

        stateMachine.SwitchState(States.Frenzy, state);


    }

    public void AddToAvailableLocations(Component sender, List<object> data)
    {
        if (data.Count != 3)
            return;
        if (data[0] is not LocationState.Available)
            return;

        if (locations is null)
            locations = new List<Locations>();

        locations.Add(new Locations((LocationIds)data[1], (List<Transform>)data[2]));


    }

    public void RemoveFromAvailableLocations(Component sender, List<object> data)
    {
        if (data.Count != 2)
            return;
        if (data[0] is not LocationState.NoAvailable)
            return;
        foreach (Locations loc in locations)
        {
            if (loc.locationId == (LocationIds)data[1])
            {
                //Debug.Log("Tag to remove : " + (LocationIds)data[1]);
                locations.Remove(loc);
                return;
            }
        }



    }

    private class Locations
    {
        public LocationIds locationId;
        public List<Transform> minLocations;
        public Locations(LocationIds locationId, List<Transform> minLocations)
        {
            this.locationId = locationId;
            this.minLocations = minLocations;
        }
    }
}