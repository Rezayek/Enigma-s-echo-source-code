using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AgentMovements))]
[RequireComponent(typeof(GameEventListener))]
[RequireComponent(typeof(AgentSanityDamage))]
[RequireComponent(typeof(AgentHealthDamage))]
public class BansheeStateMachine : FiniteStateMachine
{
    [SerializeField] private List<AbstractState> states;
    private Transform playerTransform;
    private AbstractState currentState;
    private bool update;

    private AgentHealthDamage agentHealthDamage;
    private AgentMovements agentMovements;
    private AIUtils helper;
    private AISoundManager soundManager;
    private AgentUpperAnimationController upperAnimationController;

    
    // Start is called before the first frame update
    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        agentHealthDamage = GetComponent<AgentHealthDamage>();
        agentMovements = GetComponent<AgentMovements>() ;
        helper = GetComponent<AIUtils>();
        soundManager = GetComponent<AISoundManager>();
        upperAnimationController = GetComponent<AgentUpperAnimationController>();
        update = false;
        currentState = SelectState(States.Wandering);
        currentState.OnEnterState(
            this, 
            States.None, 
            soundManager,
            agentMovements,
            playerTransform,
            transform,
            helper,
            out update);
    }

    // Update is called once per frame
    void Update()
    {
        if (update)
            currentState.UpdateState();
    }

    public override void SwitchState(States state, States previousState)
    {
        update = false;
        currentState.OnExitState();
        currentState = SelectState(state);
        currentState.OnEnterState(
            this, 
            previousState, 
            soundManager, 
            agentMovements,
            playerTransform,
            transform,
            helper, 
            out update);
    }

    private AbstractState SelectState(States state)
    {
        foreach(AbstractState st in states)
        {
            if (st.state == state)
                return st;
        }
        return currentState;
    }

    public override void CallAniamtorPlay(AIAnimationsNames animation)
    {
        //Debug.Log("Play:" + animation);
        upperAnimationController.PlayAnimation(animation);
    }

    public override void CallAniamtorStop(AIAnimationsNames animation)
    {
        //Debug.Log("Stop:" + animation);
        upperAnimationController.StopAnimation(animation);
    }

    public override void DealHealthDamage(int healthDamage, float damagePerDuration)
    {
        agentHealthDamage.Attack(healthDamage, damagePerDuration);
    }

    public override void ExecuteCoroutine(CoroutineFunction couroutine)
    {
        StartCoroutine(couroutine());
    }
}
