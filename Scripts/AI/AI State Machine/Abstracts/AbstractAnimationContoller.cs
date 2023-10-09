using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]

public abstract class AbstractAnimationContoller : MonoBehaviour
{
    public Animator animator;


    

    public abstract void PlayAnimation(AIAnimationsNames animation);

    public abstract void StopAnimation(AIAnimationsNames animation);
}
