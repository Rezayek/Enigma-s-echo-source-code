using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PuzzelRewardAbs : MonoBehaviour
{
    [SerializeField] private int totalObjectif;
    private int totalNification = 0;
    private bool endReward = false;
    public void StateEndNotifier()
    {
        totalNification += 1;
    }

    public void EnableReward()
    {
        if (totalNification == totalObjectif && !endReward)
        {
            endReward = true;
            ExceuteReward();
            
        }
            
    }

    public abstract void ExceuteReward();
}
