using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocationsDetector : MonoBehaviour
{
    [SerializeField] private GameEvent rewardEventCall;
    [SerializeField] private float bigLocationMinDistance;
    [SerializeField] private float smallLocationMinDistance;
    [SerializeField] private List<Transform> bigLocationsAllwoedTransforms;
    [SerializeField] private List<Transform> bigLocationsNotAllwoedTransforms;
    [SerializeField] private List<Transform> smallLocationsTransforms;



    private Vector3 previousSmallT = Vector3.zero ;
    private Vector3 previousBigT = Vector3.zero;

    private float sInc = 0;
    private float bInc = 0;

    // Update is called once per frame
    void Update()
    {
        CkeckBigAllowed();
        CkeckBigNotAllowed();
        CkeckSmallAllowed();
    }

    public List<Transform> GetBigA()
    {
        return bigLocationsAllwoedTransforms;
    }

    public List<Transform> GetBigNA()
    {
        return bigLocationsNotAllwoedTransforms;
    }
    public List<Transform> GetSmallA()
    {
        return smallLocationsTransforms;
    }



    private void CkeckBigAllowed()
    {

        foreach (Transform t in bigLocationsAllwoedTransforms)
        {
            // Calculate the distance between the AI and the player
            float distance = Vector3.Distance(transform.position, t.position);

            // Check if the distance is less than the minimum distance
            if (distance < bigLocationMinDistance &&  previousBigT != t.position)
            {
                //Call Reward Event reward
                rewardEventCall.Raise(this, new List<object> { RewardCallType.Reward, RewardType.BigAllowed });
            }
            else if (distance < bigLocationMinDistance && previousBigT == t.position)
            {
                //Call Reward Event punish
                rewardEventCall.Raise(this, new List<object> { RewardCallType.Punish, PunishType.NoRepeat });
                previousBigT = t.position;
            }

        }
        
    }

    private void CkeckBigNotAllowed()
    {
        foreach(Transform t in bigLocationsNotAllwoedTransforms)
        {
            // Calculate the distance between the AI and the player
            float distance = Vector3.Distance(transform.position, t.position);

            // Check if the distance is less than the minimum distance
            if (distance < smallLocationMinDistance )
            {
                //Call Reward Event punish
                rewardEventCall.Raise(this, new List<object> { RewardCallType.Punish, PunishType.BigNotAlloawed });
                previousBigT = t.position;
            }
            


        }
    }

    private void CkeckSmallAllowed()
    {
        foreach (Transform t in smallLocationsTransforms)
        {
            // Calculate the distance between the AI and the player
            float distance = Vector3.Distance(transform.position, t.position);

            // Check if the distance is less than the minimum distance
            if (distance < smallLocationMinDistance && previousSmallT != t.position)
            {
                //Call Reward Event reward
                rewardEventCall.Raise(this, new List<object> { RewardCallType.Reward, RewardType.SmallAllowed });
            }
            else if (distance < smallLocationMinDistance && previousSmallT == t.position)
            {
                //Call Reward Event punish
                rewardEventCall.Raise(this, new List<object> { RewardCallType.Punish, PunishType.NoRepeat });
                previousSmallT = t.position;
            }

        }
    }
    
}
