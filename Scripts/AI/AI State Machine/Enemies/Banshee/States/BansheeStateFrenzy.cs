using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "State/Enemies/Banshee/Banshee State Frenzy")]
public class BansheeStateFrenzy : AbstractState
{
    [SerializeField] private float frenzyCoroutine = 30;
    [SerializeField] private float minDistanceBeforeAttack = 3;
    [SerializeField] private float minDistanceBeforeVanish = 10;
    private enum FrenzyInnerState
    {
        None,
        Locked,
        TimeUp,
    }

    private float counter;
    // Start is called before the first frame update



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
        GetDifficultyValue();
        //Set Difficulty Values
        frenzyCoroutine = 30;
        frenzyCoroutine *= difficultyMultiplier;

        stateMachine.CallAniamtorPlay(animation);

        if (counter <= 0)
        {
            counter = frenzyCoroutine;
        }
        update = true;  
    }

    public override void OnExitState()
    {
        stateMachine.CallAniamtorStop(animation);
    }

    public override void UpdateState()
    {
        CallAudio();
        FrenzyCoroutineCounter(); 
        FollowPlayer(playerTransform);
        CheckStateSwitch();
    }

    private void CheckStateSwitch()
    {
        if (helper.WithInRange(agentTransform.position, playerTransform.position, minDistanceBeforeAttack) && !helper.IsInFront(playerTransform, agentTransform, minDistanceBeforeVanish))
        {
            stateMachine.SwitchState(States.Attack, state);
        }
        else if(helper.IsInFront(playerTransform, agentTransform, minDistanceBeforeVanish))
        {
            stateMachine.SwitchState(States.Vanish, state);
        }
    }


    private void FrenzyCoroutineCounter()
    {
        if (counter > 0)
        {
            counter -= Time.deltaTime;
        }
        else
        {
            stateMachine.SwitchState(States.Wandering, state);
        }
    }
    private void FollowPlayer(Transform target)
    {
        agentMovements.SetDistination(target.position);
    }

    public override void CallAudio()
    {
        soundManager.PlaySound(stateAudio);
    }

    
}
