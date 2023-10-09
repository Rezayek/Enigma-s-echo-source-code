using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(GameEventListener))]
public class DoorController : AbstractInteractor
{
    private Animator animator;
    void Start()
    {
        action = EnvActions.DoorAction;
        animator = GetComponent<Animator>();
    }

    public void DoorListener(Component sender, List<object> data)
    {
        if (data.Count != 2)
            return;

        if (data[0] is not EnvActions.DoorAction)
            return;

        if ((DoorController)data[1] != this)
            return;
        OpenDoor();

    }

    private void OpenDoor()
    {
        animator.SetTrigger("Door Open");
    }

    public override EnvActions GetEvent()
    {
        return action;
    }
}
