using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharedDisplayState : PuzzelStateAbs
{
    [Header("Objects to control")]
    [SerializeField] private Transform parentTransform;
    
    [SerializeField] private float showDistance = 10f;
    [SerializeField] private float hideDistance = 3f;
    [SerializeField] private float sanityDistance = 5f;
    [SerializeField] private float checkInterval = 20f;
    [SerializeField] private int defaultDamage = 2;
    [SerializeField] private SoundType1 sound;
    private List<GameObject> hiddenObjects;
    private Transform playerTransform;
    private DamageManager damageManager;
    private List<GameObject> activeObjects;
    private AudioEventCaller audioEventCaller;
    private int difficultyMultiplier;
    private void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        damageManager = DamageManager.Instance;
        audioEventCaller = AudioEventCaller.Instance;
        hiddenObjects = new List<GameObject>();
        activeObjects = new List<GameObject>();
        GetChildObjects(parentTransform);
        GetDifficultyValue();

    }
    public override void OnActionState()
    {
        return;
    }

    public override void OnEnterState(PuzzelStateMachineAbs stateMachine)
    {
        this.stateMachine = stateMachine;
        StartCoroutine(ShowLoop());
        StartCoroutine(HideLoop());
    }

    public override void OnExitState()
    {
        StopAllCoroutines();
        return;
    }

    public override void UpdateState()
    {
        return;
    }


    private IEnumerator HideLoop()
    {
        while (true)
        {
            for(int index = 0; index < activeObjects.Count; index++)
            {
                float distanceToObj = Vector3.Distance(playerTransform.position, activeObjects[index].transform.position);
                if (distanceToObj <= hideDistance)
                {
                    activeObjects[index].SetActive(false);
                    activeObjects.RemoveAt(index);
                }
                else if (distanceToObj < sanityDistance)
                {
                    yield return new WaitForSeconds(2);
                    damageManager.SanityDamageRecieved(defaultDamage * difficultyMultiplier);
                }
            }
            yield return null;
        }
    }

    private IEnumerator ShowLoop()
    {
        while (true)
        {
            // Check the nearest object to the player
            FindNearestObject();
            yield return new WaitForSeconds(checkInterval);
        }
    }

    private void FindNearestObject()
    {


        foreach (GameObject obj in hiddenObjects)
        {
            float distanceToObj = Vector3.Distance(playerTransform.position, obj.transform.position);
            if (distanceToObj < showDistance && activeObjects.IndexOf(obj) == -1)
            {
                activeObjects.Add(obj);
            }
        }
        ShowNearestObject();
    }

    private void ShowNearestObject()
    {
        if (activeObjects.Count == 0)
            return;
        foreach (GameObject obj in activeObjects)
        {
            // Show the nearest object and hide the others
            obj.SetActive(true);
            audioEventCaller.PlayOnce(SoundType1.WomenEvilLaugh);
            Vector3 directionToTarget = playerTransform.position - obj.transform.position;
            directionToTarget.y = 0f; // Remove vertical component to keep X-axis rotation at 0
            Quaternion targetRotation = Quaternion.LookRotation(directionToTarget);
            obj.transform.rotation = targetRotation;

        }
        
    }

    private void GetChildObjects(Transform parent)
    {
        int childCount = parent.childCount;

        for (int i = 0; i < childCount; i++)
        {
            Transform child = parent.GetChild(i);
            hiddenObjects.Add(child.gameObject);

            // If you want to recursively get children of children, uncomment the next line
            // GetChildObjects(child);
        }
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
