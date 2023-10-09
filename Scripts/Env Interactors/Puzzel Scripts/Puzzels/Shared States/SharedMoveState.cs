using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharedMoveState : PuzzelStateAbs
{
    [SerializeField] Transform center;
    [SerializeField] Transform rockTransform;
    [SerializeField] List<DirectionData> directions;
    [SerializeField] float duration = 1.0f; // Adjust the duration as needed
    [SerializeField] SoundType1 sound;
    private Transform playerTransform;
    private bool isMoving;
    private AudioEventCaller audioEventCaller;
    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        rockTransform.position = center.position;
        isMoving = false;
        audioEventCaller = AudioEventCaller.Instance;
    }

    public override void OnActionState()
    {
        if (isMoving)
            return;
        int currentDirection = IsAtFront();
        if (currentDirection < 0)
            return;

        Debug.Log("Move");
        StartCoroutine(MoveCorortine(currentDirection));
    }

    public override void OnEnterState(PuzzelStateMachineAbs stateMachine)
    {
        this.stateMachine = stateMachine;
    }

    public override void OnExitState()
    {
        return;
    }

    public override void UpdateState()
    {
        return;
    }


    private int IsAtFront()
    {
        // Determine the direction the player is facing
        Vector3 playerForward = playerTransform.forward;

        // Find the direction the player is looking at the most
        int closestDirectionIndex = -1;
        float closestDot = -1f;

        for (int i = 0; i < directions.Count; i++)
        {
            Vector3 targetDirection = directions[i].objectTransform.position - center.position;
            float dot = Vector3.Dot(playerForward, targetDirection.normalized);

            if (dot > closestDot)
            {
                closestDirectionIndex = i;
                closestDot = dot;
            }
        }

        return closestDirectionIndex;
    }

    private IEnumerator MoveCorortine(int currentDirection)
    {
        isMoving = true;
        yield return StartCoroutine(MoveObject(rockTransform.position, center.position));
        yield return StartCoroutine(MoveObject(center.position, directions[currentDirection].objectTransform.position));
        if (directions[currentDirection].isFinalDirection)
        {
            stateMachine.OnSwitchState(nextPhase);
            Debug.Log("exit");
        }
        isMoving = false;
    }

    private IEnumerator MoveObject(Vector3 startPosition, Vector3 targetPosition)
    {
        
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            rockTransform.position = Vector3.Lerp(startPosition, targetPosition, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            audioEventCaller.PlayOnce(sound);
            yield return null;
        }

        rockTransform.position = targetPosition;
    }


    [System.Serializable]
    private class DirectionData
    {
        public Transform objectTransform;
        public bool isFinalDirection;
    }
}
