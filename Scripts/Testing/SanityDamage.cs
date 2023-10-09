using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SanityDamage : MonoBehaviour
{
    private DamageManager damageManager;

    // Start is called before the first frame update

    void Start()
    {
        damageManager = DamageManager.Instance;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
            damageManager.SanityDamageRecieved(damage: 10);
    }
}
