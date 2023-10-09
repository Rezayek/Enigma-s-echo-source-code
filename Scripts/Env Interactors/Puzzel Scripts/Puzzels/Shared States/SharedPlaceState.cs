using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(GameEventListener))]

public class SharedPlaceState : PuzzelStateAbs
{
    [SerializeField] GameEvent inventoryCall;
    [SerializeField] ItemData item;
    [SerializeField] GameObject hidenGameObject;

    public override void OnActionState()
    {
        RequestItem();
    }

    public override void OnEnterState(PuzzelStateMachineAbs stateMachine)
    {
        this.stateMachine = stateMachine;
    }

    public override void UpdateState()
    {
        return;
    }

    public override void OnExitState()
    {
        return;
    }

    private void RequestItem()
    {
        
        inventoryCall.Raise(this, new List<object> {EnvActions.PuzzelAction, item, this });
    }

    public void GetTheItem(Component sender, List<object> data)
    {
        if (data[0] is not SharedPlaceState)
            return;
        if ((SharedPlaceState)data[0] != this)
            return;
        if (data[1] is not EnvActions.PuzzelAction)
            return;
        if (data[2] is not bool)
            return;
        if (!(bool)data[2])
            return;
        hidenGameObject.SetActive(true);
        stateMachine.OnSwitchState(nextPhase);

    }

    
}
