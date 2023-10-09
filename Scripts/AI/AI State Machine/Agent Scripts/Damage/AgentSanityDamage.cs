using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AIUtils))]
public class AgentSanityDamage : MonoBehaviour
{
    [SerializeField] private int sanityDamage = 3;
    [SerializeField] private float damagePerDuration = 3;
    [SerializeField] private float distanceToApplyDamage = 10;
    private DamageManager damageManager;
    private AIUtils helper;
    private bool locked;
    private Transform playerTransform;
    private int difficultyMultiplier;
    // Start is called before the first frame update
    void Start()
    {
        GetDifficultyValue();
        locked = false;
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        damageManager = DamageManager.Instance;
        helper = GetComponent<AIUtils>();
    }

    // Update is called once per frame
    void Update()
    {
        if (helper.WithInRange(transform.position, playerTransform.position, distanceToApplyDamage) && !locked)
        {
            locked = true;
            StartCoroutine(ApplySanityDamage());
        }
    }

    private IEnumerator ApplySanityDamage()
    {
        damageManager.SanityDamageRecieved(damage: sanityDamage * difficultyMultiplier);
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
