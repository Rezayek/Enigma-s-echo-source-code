using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(GameEventListener))]
public class CheckPointsManager : MonoBehaviour
{
    [SerializeField] private Transform initSpawnPoint;
    [SerializeField] private int totalTries = 5;
    [SerializeField] private GameEvent warningCall;
    [SerializeField] private GameEvent loadingCall;


    private Transform lastCheckPoint;
    private Transform playerTransform;
    private void Start()
    {
        lastCheckPoint = initSpawnPoint;
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public void UpdateCheckPoint(Component sender, List<object> data)
    {
        if (sender is not CheckPoint)
            return;
        lastCheckPoint = (Transform)data[0];
    }

    public void CheckPointListener(Component sender, List<object> data)
    {
        if (data.Count != 1)
            return;
        if (data[0] is not CheckPointCallTypes)
            return;
        CheckPointCallTypes calltype = (CheckPointCallTypes)data[0];
        switch (calltype)
        {
            case CheckPointCallTypes.Respawn:
                StartCoroutine(PlayerRespawn());
                break;
            case CheckPointCallTypes.ReduceTries:
                PlayerDeathActions();
                break;
            case CheckPointCallTypes.RemoveTries:
                totalTries = -1;
                break;
        }
    }


    private void PlayerDeathActions() 
    {

        totalTries -= 1;


        if (totalTries >= 0)
        {
            warningCall.Raise(this, new List<object> { WarningTypes.PlayerRevive });
        }
        else
        {
            warningCall.Raise(this, new List<object> { WarningTypes.PlayerLose });
        }
    }

    private IEnumerator PlayerRespawn()
    {
        loadingCall.Raise(this, new List<object> { OtherLoadingCases.Respawn });
        yield return new WaitForSeconds(0.1f); // Adjust the delay as needed

        // Change the player's position and rotation to the checkpoint
        playerTransform.position = lastCheckPoint.position;
        playerTransform.eulerAngles = lastCheckPoint.eulerAngles;
    }

}
