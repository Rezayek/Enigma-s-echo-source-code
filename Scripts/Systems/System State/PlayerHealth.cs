using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(GameEventListener))]
public class PlayerHealth : PlayerState
{
    [SerializeField] private int health = 100;
    [SerializeField] GameEvent playerStateUIEvent;
    public override void Decrease(int amount)
    {
        health -= amount;
        if (health < 0)
        {
            health = 0;
            NotifyCheckPoint();
            health = 100;
        }
            
        NotifyUIState(isIncrease: false);
    }

    public override void Increase(int amount)
    {
        health += amount;
        if (health > 100)
            health = 100;
        NotifyUIState(isIncrease: true);
    }

    public override void ListenToChangesDecrease(Component sender, List<object> data)
    {
        if (data.Count <= 1)
            return;
        if (data[0] is not Damage && data[1] is not int)
            return;
        Damage dmg = (Damage)data[0];
        if (dmg != Damage.HealthDamage)
            return;
        int qte = (int)data[1];
        Debug.Log("<color=red> Decrease Health qte:  </color>" + qte);
        Decrease(qte);
    }

    public override void ListenToChangesIncrease(Component sender, List<object> data)
    {
        if (data.Count <= 1)
            return;
        if (data[0] is not ConsummableType && data[1] is not int)
            return;

        ConsummableType castEnum = (ConsummableType)data[0];
        if (castEnum != ConsummableType.Health)
            return;
        int qte = (int)data[1];
        Debug.Log("<color=red> Restore Health qte:  </color>" + qte);
        Increase(qte);
        
    }

    

    public override void NotifyCheckPoint()
    {
        checkPointCall.Raise(this, new List<object> { CheckPointCallTypes.ReduceTries });
    }

    public override void NotifyUIState(bool isIncrease)
    {

        if (isIncrease)
        {
            List<object> data = new List<object>
            {
                PlayerStateEnum.IncreaseHealth,
                health
            };

            playerStateUIEvent.Raise(this, data);
        }
        else
        {
            List<object> data = new List<object>
            {
                PlayerStateEnum.DecreaseHealth,
                health
            };

            playerStateUIEvent.Raise(this, data);
        }
    }

}
