using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(GameEventListener))]
public class PlayerSanity : PlayerState
{
    [SerializeField] private int sanity = 100;
    [SerializeField] GameEvent playerStateUIEvent;
    public override void Decrease(int amount)
    {
        sanity -= amount;
        if (sanity < 0)
        {
            sanity = 0;
            NotifyCheckPoint();
            sanity = 100;
        }
            
        NotifyUIState(isIncrease: false);
    }

    public override void Increase(int amount)
    {
        sanity += amount;
        if (sanity > 100)
            sanity = 100;
        NotifyUIState(isIncrease: true);
    }

    public override void ListenToChangesDecrease(Component sender, List<object> data)
    {
        if (data.Count <= 1)
            return;
        if (data[0] is not Damage && data[1] is not int)
            return;
        Damage dmg = (Damage)data[0];
        if (dmg != Damage.SanityDamage)
            return;
        int qte = (int)data[1];
        Debug.Log("<color=red> Decrease Sanity qte:  </color>" + qte);
        Decrease(qte);
    }

    public override void ListenToChangesIncrease(Component sender, List<object> data)
    {
        if (data.Count <= 1)
            return;
        if (data[0] is not ConsummableType && data[1] is not int)
            return;

        ConsummableType castEnum = (ConsummableType)data[0];
        if (castEnum != ConsummableType.Sanity)
            return;
        int qte = (int)data[1];
        Debug.Log("<color=red> Restore Sanity qte:  </color>"+ qte);
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
                PlayerStateEnum.IncreaseSanity,
                sanity
            };

            playerStateUIEvent.Raise(this, data);
        }
        else
        {
            List<object> data = new List<object>
            {
                PlayerStateEnum.DecreaseSanity,
                sanity
            };

            playerStateUIEvent.Raise(this, data);
        }
    }

}
