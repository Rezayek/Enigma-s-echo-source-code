using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;

[RequireComponent(typeof(AIMovement))]
[RequireComponent(typeof(AIAttacks))]
[RequireComponent(typeof(LocationsDetector))]
[RequireComponent(typeof(FollowPlayerDetector))]
[RequireComponent(typeof(GameEventListener))]
public class AIBrain : Agent
{
    [SerializeField] private Transform playerTransform;

    [SerializeField] private int sameRewardLimit;
    [SerializeField] private int samePunishLimit;

    private LocationsDetector locationsDetector;

    private AIMovement aIMovement;
    private AIAttacks aIAttacks;
    

    private int rewardsInc;
    private int punishInc;

    private RewardType previousReward = RewardType.None;
    private PunishType previousPunish = PunishType.None;

    private void Start()
    {
        aIMovement = GetComponent<AIMovement>();
        aIAttacks = GetComponent<AIAttacks>();
        locationsDetector = GetComponent<LocationsDetector>();
    }

    public override void OnEpisodeBegin()
    {
        rewardsInc = 0;
        punishInc = 0;
        previousReward = RewardType.None;
        previousPunish = PunishType.None;
        playerTransform.localPosition = new Vector3(Random.Range(-130, 50), 10, Random.Range(-85, 90));
        transform.localPosition = new Vector3(Random.Range(-130, 50), 10, Random.Range(-85, 90));
    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        float moveX = actions.ContinuousActions[0];
        float moveZ = actions.ContinuousActions[1];
        int sprint = actions.DiscreteActions[0];
        int attack = actions.DiscreteActions[1];

        bool shouldSprint = sprint == 1; // Example condition, adjust as needed
        bool shouldAttack = attack == 1; // Example condition, adjust as needed

        aIMovement.Move(new Vector2(moveX, moveZ), shouldSprint);
        aIAttacks.PreformAttack(shouldAttack);
    }

    //public override void Heuristic(in ActionBuffers actionsOut)
    //{
    //    ActionSegment<float> continuousActions = actionsOut.ContinuousActions;
    //    ActionSegment<int> discreteActions = actionsOut.DiscreteActions;
    //}

    public override void CollectObservations(VectorSensor sensor)
    {
        //Add observation to thes sensors
        sensor.AddObservation(transform.localPosition);
        sensor.AddObservation(playerTransform.localPosition);


        foreach (Transform obj in locationsDetector.GetBigA())
        {
            sensor.AddObservation(obj.localPosition);
        }

        foreach (Transform obj in locationsDetector.GetBigNA())
        {
            sensor.AddObservation(obj.localPosition);
        }

        foreach (Transform obj in locationsDetector.GetSmallA())
        {
            sensor.AddObservation(obj.localPosition);
        }

    }

    public void GiveReward(Component sender, List<object> data)
    {
        if (data.Count != 2)
            return;
        if (data[0] is not RewardCallType)
            return;

        RewardCallType castEnum = (RewardCallType)data[0];
        switch (castEnum)
        {
            case RewardCallType.Reward:
                CheckRewardLimit((RewardType) data[1]);
                break;
            case RewardCallType.Punish:
                CheckPunishLimit((PunishType) data[1]);
                break;
        }

    }

    private void CheckRewardLimit(RewardType rewardType)
    {
        if(rewardType == previousReward && rewardsInc < sameRewardLimit)
        {
            DecideReward(rewardType);
            rewardsInc += 1;
        }
        else if(rewardType == previousReward && rewardsInc >= sameRewardLimit)
        {
            AddReward(-1000f);
            EndEpisode();
        }
        else if(rewardType != previousReward)
        {
            DecideReward(rewardType);
            previousReward = rewardType;
            rewardsInc = 1;
        }
    }

    private void CheckPunishLimit(PunishType punishType)
    {
        if (punishType == previousPunish && punishInc < samePunishLimit)
        {
            DecidePunish(punishType);
            rewardsInc += 1;
        }
        else if (punishType == previousPunish && punishInc >= samePunishLimit)
        {
            AddReward(-1000f);
            EndEpisode();
        }
        else if (punishType != previousPunish)
        {
            DecidePunish(punishType);
            previousPunish = punishType;
            rewardsInc = 1;
        }
    }


    private void DecideReward(RewardType rewardType)
    {
        switch (rewardType)
        {
            case RewardType.NoMoreFront:
                AddReward(+1f);
                break;
            case RewardType.NoMoreBehind:
                AddReward(+1f);
                break;
            case RewardType.BigAllowed:
                AddReward(+1.5f);
                break;
            case RewardType.SmallAllowed:
                AddReward(+0.5f);
                break;
            case RewardType.Attack:
                AddReward(+1.5f);
                break;
        }
    }

    private void DecidePunish(PunishType punishType)
    {
        switch (punishType)
        {
            case PunishType.StillFrontPlayer:
                AddReward(-1f);
                break;
            case PunishType.StillBehindPlayer:
                AddReward(-0.5f);
                break;
            case PunishType.BigNotAlloawed:
                AddReward(-5f);
                break;
            case PunishType.NoAttack:
                AddReward(-1f);
                break;
            case PunishType.NoRepeat:
                AddReward(-0.5f);
                break;
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent<Boundaries>(out Boundaries boundaries))
        {
            AddReward(-10f);
            EndEpisode();
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent<Trees>(out Trees trees))
        {
            AddReward(-0.3f);
        }
        else if (collision.gameObject.TryGetComponent<Boundaries>(out Boundaries boundaries))
        {
            AddReward(-10f);
            EndEpisode();
        }
    }
}
