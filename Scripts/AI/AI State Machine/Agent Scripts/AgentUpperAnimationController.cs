using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentUpperAnimationController : AbstractAnimationContoller
{
    public override void PlayAnimation(AIAnimationsNames animation)
    {
        animator.SetBool(animation.ToString(), true);
    }

    public override void StopAnimation(AIAnimationsNames animation)
    {

        animator.SetBool(animation.ToString(), false);
    }
}
