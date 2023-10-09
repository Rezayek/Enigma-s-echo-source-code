using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageManager : GenericSingleton<DamageManager>
{
    [SerializeField] private GameEvent damageEvent;


    public void HeathDamageRecieved(int damage)
    {
        List<object> data = new List<object>
        {
            Damage.HealthDamage,
            damage
        };
        damageEvent.Raise(this, data);
    }
    public void SanityDamageRecieved(int damage)
    {
        List<object> data = new List<object>
        {
            Damage.SanityDamage,
            damage
        };
        damageEvent.Raise(this, data);
    }
}
