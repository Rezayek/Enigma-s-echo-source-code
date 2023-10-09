using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharedRotationState : PuzzelStateAbs
{

    [SerializeField] private float rotationSpeed = 50f; // Adjust this value to control the rotation speed
    [SerializeField] private Vector3 rotationAngle;
    [SerializeField] private bool rotateOnX = false;
    [SerializeField] private bool rotateOnY = false;
    [SerializeField] private bool rotateOnZ = false;
    [SerializeField] private Transform desiredDirection;
    [SerializeField] private SoundType1 sound;
    private bool isRotating = false;
    private Vector3 targetEulerAngles;
    private AudioEventCaller audioEventCaller;

    public override void OnActionState()
    {
        StartRotation();
    }

    public override void OnEnterState(PuzzelStateMachineAbs stateMachine)
    {
        this.stateMachine = stateMachine;
        audioEventCaller = AudioEventCaller.Instance;
    }

    public override void UpdateState()
    {
        if (CheckTarget())
            stateMachine.OnSwitchState(nextPhase);
    }

    public override void OnExitState()
    {
        return;
    }

    // Call this method to start the rotation
    private void StartRotation()
    {
        if (!isRotating)
        {
            isRotating = true;
            targetEulerAngles = transform.eulerAngles;

            if (rotateOnX) targetEulerAngles.x += rotationAngle.x;
            if (rotateOnY) targetEulerAngles.y += rotationAngle.y;
            if (rotateOnZ) targetEulerAngles.z += rotationAngle.z;

            StartCoroutine(RotateTowardsTarget());
        }
    }

    private IEnumerator RotateTowardsTarget()
    {
        while (Quaternion.Angle(transform.rotation, Quaternion.Euler(targetEulerAngles)) > 0.01f)
        {
            audioEventCaller.PlayOnce(sound);
            // Calculate the step to rotate towards the target rotation
            float step = rotationSpeed * Time.deltaTime;
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(targetEulerAngles), step);
            yield return null;
        }

        // Ensure the rotation finishes at the exact target angles.
        transform.eulerAngles = targetEulerAngles;
        isRotating = false;
    }

    private bool CheckTarget()
    {
        Vector3 toDesiredDirection = desiredDirection.position - transform.position;
        float dotProduct = Vector3.Dot(transform.forward, toDesiredDirection.normalized);

        // Adjust the threshold (0.95f in this case) to define how close is acceptable.
        if (dotProduct >= 0.95f)
        {
            return true;
            // Place any actions you want to execute when the object faces the desired direction here.
        }
        else
        {
            return false;
            // Place any actions you want to execute when the object doesn't face the desired direction here.
        }

    }
}
