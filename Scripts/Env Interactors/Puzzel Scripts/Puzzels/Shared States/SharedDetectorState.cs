using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharedDetectorState : PuzzelStateAbs
{
    public override void OnActionState()
    {
        stateMachine.OnSwitchState(nextPhase);
    }

    public override void OnEnterState(PuzzelStateMachineAbs stateMachine)
    {
        this.stateMachine = stateMachine;
    }

    public override void OnExitState()
    {
        return;
    }

    public override void UpdateState()
    {
        return;
    }

    
}
