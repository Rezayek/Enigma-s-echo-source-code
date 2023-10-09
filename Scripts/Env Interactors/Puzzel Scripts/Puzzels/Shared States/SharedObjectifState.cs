using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharedObjectifState : PuzzelStateAbs
{
    [SerializeField] List<PuzzelRewardAbs> callReward;
    public override void OnActionState()
    {
        return;
    }

    public override void OnEnterState(PuzzelStateMachineAbs stateMachine)
    {
        foreach(PuzzelRewardAbs puzzelRewardAbs in callReward)
        {
            puzzelRewardAbs.StateEndNotifier();
        }
        
    }

    public override void UpdateState()
    {
        return;
    }

    public override void OnExitState()
    {
        return;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
