using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationContoller : MonoBehaviour
{

    [SerializeField] private Animator animator;
    private bool animationIsBlocked = false;

    // Start is called before the first frame update
    void Start()
    {
        if(animator == null)
            animator = GetComponent<Animator>();
        
    }

    public void MovementAnimation(float x, float y, float sprintSpeed, float speed, float moveSpeed)
    {
        if (animator == null)
            return;

        if (animationIsBlocked)
            return;


        if(speed > moveSpeed)
        {
            x += x * (speed / sprintSpeed);
            y += y * (speed / sprintSpeed);
        }
        //Debug.Log("passed x: " + x + " passed y: " + y);
        animator.SetFloat("x", x );
        animator.SetFloat("y", y );
    }
}
