using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharedAnimationReward : PuzzelRewardAbs
{


    [SerializeField] private BasicPuzzelAnimationNames animationName;

    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        EnableReward();
    }

    public override void ExceuteReward()
    {
        OpenHolyCupHolder();
    }

    private void OpenHolyCupHolder()
    {
        animator.SetTrigger(animationName.ToString());
    }
}
