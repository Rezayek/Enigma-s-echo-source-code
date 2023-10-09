using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharedIdleState : PuzzelStateAbs
{
    public override void OnActionState()
    {
        return;
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
