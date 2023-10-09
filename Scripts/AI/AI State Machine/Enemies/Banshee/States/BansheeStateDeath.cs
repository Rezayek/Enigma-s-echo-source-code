using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "State/Enemies/Banshee/Banshee State Death")]
public class BansheeStateDeath : AbstractState
{
    [SerializeField] private GameEvent FinalSceneTriggerCallEvent;
    [SerializeField] private float deathPlayDuration = 6.0f;
    private bool startUpdate;
    private GameObject fire;
    private GameObject pointLight;
    public override void CallAudio()
    {
        soundManager.PlaySound(stateAudio);
    }

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
        update = true;
        startUpdate = false;
        stateMachine.ExecuteCoroutine(PlayDeath);
        agentTransform.Find("Fire").gameObject.SetActive(true);
        agentTransform.Find("Point Light").gameObject.SetActive(true);
    }

    public override void OnExitState()
    {
        return;
    }

    public override void UpdateState()
    {
        if(startUpdate)
            CallAudio();
    }
    private IEnumerator PlayDeath()
    {
        startUpdate = true;
        stateMachine.CallAniamtorPlay(animation);
        yield return new WaitForSeconds(deathPlayDuration);
        startUpdate = false;
        stateMachine.CallAniamtorStop(animation);
        FinalSceneTriggerCallEvent.Raise(stateMachine, new List<object> { WarningTypes.PlayerWin });

    }
}
