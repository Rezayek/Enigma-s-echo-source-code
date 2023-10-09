using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class AIAttacks : MonoBehaviour
{
    [SerializeField] private GameEvent rewardEventCall;
    [SerializeField] private float minDistance;
    [SerializeField] private Transform playerTransform;

    public void PreformAttack(bool preformAttack)
    {
        if (preformAttack && DistanceCheck())
        {
            //Call Reward Event reward
            rewardEventCall.Raise(this, new List<object> { RewardCallType.Reward, RewardType.Attack });
        }
        else
        {
            //Call Reward Event Punish
            rewardEventCall.Raise(this, new List<object> { RewardCallType.Punish, PunishType.NoAttack });
        }          
    }

    private bool DistanceCheck()
    {
        // Calculate the distance between the AI and the player
        float distance = Vector3.Distance(transform.localPosition, playerTransform.localPosition);

        // Check if the distance is less than the minimum distance
        if (distance < minDistance)
        {
            return true; // AI is within the minimum distance
        }

        return false; // AI is not within the minimum distance
        
    }
}
