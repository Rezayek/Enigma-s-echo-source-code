using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class SharedSlideState : PuzzelStateAbs
{
    [SerializeField] private float duration = 1.0f; // Adjust the duration as needed
    [SerializeField] private List<Transform> objectsToMove;
    [Header("Slide Data")]
    [SerializeField] bool xAxis = true;
    [SerializeField] bool yAxis = false;
    [SerializeField] bool zAxis = false;
    [SerializeField] Vector3 slideDistance;

    private bool slideInProgress;

    private void Start()
    {
        slideInProgress = false;
    }

    public override void OnActionState()
    {
        return;
    }

    public override void OnEnterState(PuzzelStateMachineAbs stateMachine)
    {
        this.stateMachine = stateMachine;
        slideInProgress = false;
    }

    public override void OnExitState()
    {
        return;
    }

    public override void UpdateState()
    {
        if (!slideInProgress) // Only start sliding if it's not already in progress
        {
            slideInProgress = true;
            StartCoroutine(SlideObjects());
        }
    }

    private IEnumerator SlideObjects()
    {
        // Create a list to store progress for each object
        List<float> progressList = new List<float>();
        for (int i = 0; i < objectsToMove.Count; i++)
        {
            progressList.Add(0f);
        }

        while (true)
        {
            bool allReachedTarget = true;

            for (int i = 0; i < objectsToMove.Count; i++)
            {
                Vector3 startPosition = objectsToMove[i].localPosition;
                Vector3 targetPosition = new Vector3(xAxis ? slideDistance.x : startPosition.x, yAxis ? slideDistance.y : startPosition.y, zAxis ? slideDistance.z : startPosition.z);

                // Increment progress for the current object
                progressList[i] += Time.deltaTime / duration;

                // Update the object's position based on the progress
                
                objectsToMove[i].localPosition = Vector3.Lerp(startPosition, targetPosition, progressList[i]);

                // Check if this object has reached its target position
                if (progressList[i] < 1f)
                {
                    allReachedTarget = false;
                }
                else
                {
                    // If the object has reached the target, set its position to the exact target position
                    objectsToMove[i].localPosition = targetPosition;
                }
            }

            
            // If all objects have reached their target positions, break out of the loop
            if (allReachedTarget)
                break;

            yield return null;
        }

        stateMachine.OnSwitchState(nextPhase);
        slideInProgress = false;
    }

}
