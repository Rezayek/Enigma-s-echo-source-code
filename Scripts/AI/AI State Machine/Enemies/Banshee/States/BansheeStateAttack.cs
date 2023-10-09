using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "State/Enemies/Banshee/Banshee State Attack")]
public class BansheeStateAttack : AbstractState
{
    [SerializeField] private float minDistanceToExit = 4;
    [SerializeField] private int healthDamage = 8;
    [SerializeField] private float damagePerDuration = 3;
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


        stateMachine.CallAniamtorPlay(animation);

        update = true;
        Debug.Log("Attack State Entered");
    }

    public override void OnExitState()
    {
        Debug.Log("Attack State Exit");
        stateMachine.CallAniamtorStop(animation);
    }

    public override void UpdateState()
    {
        stateMachine.DealHealthDamage(healthDamage, damagePerDuration);
        FollowPlayer();
        CallAudio();
        CheckStateSwitch();
    }

    private void FollowPlayer()
    {
        agentMovements.SetDistination(playerTransform.position);
    }

    public override void CallAudio()
    {
        soundManager.PlaySound(stateAudio);
    }

    private void CheckStateSwitch()
    {
        if (helper.OutOfRange(agentTransform.position, playerTransform.position, minDistanceToExit))
        {
            stateMachine.SwitchState(previousState, state);
        }

    }


}
