using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractState: ScriptableObject
{
    public States state;
    public AIAnimationsNames animation;
    public EnemySounds stateAudio;
    protected States previousState;
    protected FiniteStateMachine stateMachine;
    protected AISoundManager soundManager;
    protected AgentMovements agentMovements;
    protected Transform playerTransform;
    protected Transform agentTransform;
    protected AIUtils helper;
    protected float difficultyMultiplier;
    public abstract void OnEnterState(
        FiniteStateMachine stateMachine, 
        States previousState, 
        AISoundManager soundManager,
        AgentMovements agentMovements,
        Transform playerTransform,
        Transform agentTransform,
        AIUtils helper,
        out bool update);
    public abstract void UpdateState();
    public abstract void OnExitState();

    public abstract void CallAudio(); 

    protected void GetDifficultyValue()
    {
        switch(PlayerPrefs.GetInt(PlayerPrefsNames.Difficulty.ToString()))
        {
            case 0:
                difficultyMultiplier = 1f;
                break;
            case 1:
                difficultyMultiplier = 1.75f;
                break;
            case 2:
                difficultyMultiplier = 3f;
                break;
        }
    }
}
