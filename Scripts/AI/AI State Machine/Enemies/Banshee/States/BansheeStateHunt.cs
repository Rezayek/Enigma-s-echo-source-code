using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "State/Enemies/Banshee/Banshee State Hunt")]
public class BansheeStateHunt : AbstractState
{
    [SerializeField] private float minDistanceBeforeAttack = 3;
    [SerializeField] private float minDistanceBeforeVanish = 10;
    [SerializeField] private GameEvent CheckPointCall;
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
        CheckPointCall.Raise(stateMachine, new List<object> { CheckPointCallTypes.RemoveTries });
        update = true;
        Debug.Log("Hunt State Entered");

    }

    public override void OnExitState()
    {
        Debug.Log("Hunt State Exit");
        stateMachine.CallAniamtorStop(animation);
    }

    public override void UpdateState()
    {
        CallAudio();
        FollowPlayer();
        CheckStateSwitch();
    }

    private void CheckStateSwitch()
    {
        if (helper.WithInRange(agentTransform.position, playerTransform.position, minDistanceBeforeAttack) && !helper.IsInFront(playerTransform, agentTransform, minDistanceBeforeVanish))
        {
            stateMachine.SwitchState(States.Attack, state);
        }
        else if (helper.IsInFront(playerTransform, agentTransform, minDistanceBeforeVanish))
        {
            stateMachine.SwitchState(States.Vanish, state);
        }
    }

    public override void CallAudio()
    {
        soundManager.PlaySound(stateAudio);
    }

    private void FollowPlayer()
    {
        agentMovements.SetDistination(playerTransform.position);
    }
}
