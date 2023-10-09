using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayerDetector : MonoBehaviour
{
    [SerializeField] private GameEvent rewardEventCall;
    [SerializeField] private float distanceThreshold;
    [SerializeField] private float frontMaxDuration;
    [SerializeField] private float behindMaxDuration;

    private Transform playerTransforms;
    private bool lookState = false;
    // Start is called before the first frame update
    void Start()
    {
        playerTransforms = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        if (!lookState)
            StartCoroutine(IsBehindPlayer());
    }


    private bool CheckDistance()
    {
        Vector3 aiDirection = transform.localPosition - playerTransforms.localPosition;
        aiDirection.Normalize();

        float distance = aiDirection.magnitude;
        return distance < distanceThreshold;
    }

    private bool IsInBehind()
    {
        Vector3 aiDirection = transform.localPosition - playerTransforms.localPosition;
        Vector3 playerDirection = playerTransforms.forward;

        // Normalize the directions to remove the distance component
        playerDirection.Normalize();
        aiDirection.Normalize();

        float dotProduct = Vector3.Dot(playerDirection, aiDirection);
        return dotProduct < 0;
    }

    private IEnumerator IsBehindPlayer()
    {
        

        // Check if the AI is behind the player based on the dot product and distance threshold
        if (IsInBehind() && CheckDistance())
        {
            lookState = true;
            yield return  StartCoroutine(ObserveBehind()); // AI is behind the player
        }
        else if (!IsInBehind() && CheckDistance())
        {
            lookState = true;
            yield return StartCoroutine(ObserveFront()); // AI is in front of the player
        }
    }

    private IEnumerator ObserveFront()
    {
        yield return new WaitForSeconds(frontMaxDuration);
        if(!IsInBehind() && CheckDistance())
        {
            //Call Reward Event punish
            rewardEventCall.Raise(this, new List<object> { RewardCallType.Punish, PunishType.StillFrontPlayer });
        }
        else
        {
            //Call Reward Event reward
            rewardEventCall.Raise(this, new List<object> { RewardCallType.Reward, RewardType.NoMoreFront });
        }
        lookState = false;
    }

    private IEnumerator ObserveBehind()
    {
        yield return new WaitForSeconds(behindMaxDuration);
        if (IsInBehind() && CheckDistance())
        {
            //Call Reward Event punish
            rewardEventCall.Raise(this, new List<object> { RewardCallType.Punish, PunishType.StillBehindPlayer });
        }
        else
        {
            //Call Reward Event reward
            rewardEventCall.Raise(this, new List<object> { RewardCallType.Reward, RewardType.NoMoreBehind });
        }

        lookState = false;
    }

}
