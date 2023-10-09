using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonasteryHolyCupOfferSM : PuzzelStateMachineAbs
{
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

    private void Awake()
    {
        puzzelController = GetComponent<PuzzelController>();
        SetStates();
    }

    // Start is called before the first frame update
    void Start()
    {
        currentState = puzzels[PuzzelPhases.Phase1];
        currentState.OnEnterState(this);
    }

    // Update is called once per frame
    void Update()
    {
        currentState.UpdateState();
    }
}
