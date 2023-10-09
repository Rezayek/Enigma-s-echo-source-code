using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonasteryMannequinsSM : PuzzelStateMachineAbs
{
    [SerializeField]private DetectorController detectorController;
    public override void InteractorListener(Component sender, List<object> data)
    {
        if (data.Count != 1)
            return;
        if (sender is not DetectorController)
            return;
        if (sender != detectorController)
            return;
        currentState.OnActionState();

    }

    public override void OnSwitchState(PuzzelPhases phase)
    {
        Debug.Log("phase : "+ phase);
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
