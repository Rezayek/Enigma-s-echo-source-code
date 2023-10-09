using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerState : MonoBehaviour
{
    public GameEvent checkPointCall;
    public abstract void ListenToChangesIncrease(Component sender, List<object> data);
    public abstract void ListenToChangesDecrease(Component sender, List<object> data);
    public abstract void Increase(int amount);
    public abstract void Decrease(int amount);
    public abstract void NotifyUIState(bool isIncrease);
    public abstract void NotifyCheckPoint();
}
