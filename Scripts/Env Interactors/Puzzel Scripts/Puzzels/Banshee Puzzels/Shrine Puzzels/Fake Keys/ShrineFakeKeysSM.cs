using System.Collections.Generic;
using UnityEngine;

public class ShrineFakeKeysSM : PuzzelStateMachineAbs
{
    private void Awake()
    {
        puzzelController = GetComponent<PuzzelController>();
        SetStates();
    }
    void Start()
    {
        
        currentState = puzzels[PuzzelPhases.Phase1];
        currentState.OnEnterState(this);
    }
    private void Update()
    {
        currentState.UpdateState();
    }
    public override void InteractorListener(Component sender, List<object> data)
    {
        if (data.Count != 2)
            return;
        if (data[0] is not EnvActions.PuzzelAction)
            return;
        if ((PuzzelController)data[1] != puzzelController)
            return;

        currentState.OnActionState();
    }

    public override void OnSwitchState(PuzzelPhases phase)
    {
        currentState.OnExitState();
        currentState = puzzels[phase];
        currentState.OnEnterState(this);
    }

    
}
