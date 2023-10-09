using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AIUtils))]
public class AgentHealthDamage : MonoBehaviour
{
    private DamageManager damageManager;
    private bool locked;
    private int difficultyMultiplier;
    // Start is called before the first frame update
    void Start()
    {
        GetDifficultyValue();
        locked = false;
        damageManager = DamageManager.Instance;
    }

    public void Attack(int healthDamage, float damagePerDuration)
    {
        if (!locked)
        {
            locked = true;
            StartCoroutine(ApplyHealthDamage(healthDamage * difficultyMultiplier, damagePerDuration));
        }
            
    }

    private IEnumerator ApplyHealthDamage(int healthDamage, float damagePerDuration)
    {
        damageManager.HeathDamageRecieved(damage: healthDamage);
        yield return new WaitForSeconds(damagePerDuration);
        locked = false;
    }

    private void GetDifficultyValue()
    {
        switch (PlayerPrefs.GetInt(PlayerPrefsNames.Difficulty.ToString()))
        {
            case 0:
                difficultyMultiplier = 1;
                break;
            case 1:
                difficultyMultiplier = 2;
                break;
            case 2:
                difficultyMultiplier = 3;
                break;
        }
    }
}
