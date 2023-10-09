using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class PuzzelStateAbs : MonoBehaviour
{
    public PuzzelPhases currentPhase;
    public PuzzelPhases nextPhase;
    protected PuzzelStateMachineAbs stateMachine;
    
    public abstract void OnEnterState(PuzzelStateMachineAbs stateMachine);

    public abstract void UpdateState();
    public abstract void OnActionState();
    public abstract void OnExitState();
    public PuzzelPhases GetState()
    {
        return currentPhase;
    }
}
