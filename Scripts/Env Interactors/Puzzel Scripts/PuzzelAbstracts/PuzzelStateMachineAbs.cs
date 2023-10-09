using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(GameEventListener))]
public abstract class PuzzelStateMachineAbs : MonoBehaviour
{
    [SerializeField] private List<PuzzelStateAbs> states;
    protected Dictionary<PuzzelPhases, PuzzelStateAbs> puzzels;
    protected PuzzelStateAbs currentState;
    protected PuzzelController puzzelController;
    public abstract void InteractorListener(Component sender, List<object> data);
    public abstract void OnSwitchState(PuzzelPhases phase);

    public void SetStates()
    {
        puzzels = new Dictionary<PuzzelPhases, PuzzelStateAbs>();
        for (int i = 0; i < states.Count; i++)
        {
            puzzels.Add(states[i].GetState(), states[i]);
        }
    }
}
