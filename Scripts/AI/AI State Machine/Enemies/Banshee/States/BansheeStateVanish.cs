using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "State/Enemies/Banshee/Banshee State Vanish")]
public class BansheeStateVanish : AbstractState
{

    [SerializeField] private float distanceToChange = 20;

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

        stateMachine.CallAniamtorPlay(animation);
        agentMovements.StopAgent();
        update = true;
        Debug.Log("Vanish State Entered");
    }

    public override void OnExitState()
    {
        stateMachine.CallAniamtorStop(animation);
    }

    public override void UpdateState()
    {

        VanishCoroutineCounter();
    }

    private void VanishCoroutineCounter()
    {
        if (helper.WithInRange(agentTransform.position, playerTransform.position, distanceToChange))
        {
            CallAudio();
        }
        else
        {
            SelectNewState();
        }
    }

    private void SelectNewState()
    {
            agentMovements.ResumeAgent();
            stateMachine.SwitchState(previousState, state);
    }

    public override void CallAudio()
    {
        soundManager.PlaySound(stateAudio);
    }


}
