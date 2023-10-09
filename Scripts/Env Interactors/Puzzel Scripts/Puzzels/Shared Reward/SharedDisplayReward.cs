using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharedDisplayReward : PuzzelRewardAbs
{
    [SerializeField] private GameObject reward;

    private void Update()
    {
        EnableReward();
    }
    public override void ExceuteReward()
    {
        reward.SetActive(true);
    }

}
