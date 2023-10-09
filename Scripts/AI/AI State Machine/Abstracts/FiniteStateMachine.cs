using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AIUtils))]
[RequireComponent(typeof(AISoundManager))]
[RequireComponent(typeof(AgentUpperAnimationController))]
[RequireComponent(typeof(GameEventListener))]
public abstract class FiniteStateMachine : MonoBehaviour
{
    [SerializeField] Enemy enemy;

    public delegate IEnumerator CoroutineFunction();
    public abstract void SwitchState(States state, States previousState);
    public abstract void CallAniamtorPlay(AIAnimationsNames animation);
    public abstract void CallAniamtorStop(AIAnimationsNames animation);

    public abstract void DealHealthDamage(int healthDamage, float damagePerDuration);

    public abstract void ExecuteCoroutine(CoroutineFunction couroutine);

    public void ExternalStateSwitchCall(Component sender, List<object> data)
    {
        if (data.Count != 2)
            return;
        if (data[0] is not Enemy)
            return;
        if ((Enemy)data[0] != enemy)
            return;
        if (data[1] is not States)
            return;
        SwitchState((States)data[1], States.None);
    }
}
