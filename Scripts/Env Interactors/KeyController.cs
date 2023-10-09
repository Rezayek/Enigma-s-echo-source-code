using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(GameEventListener))]
public class KeyController : AbstractInteractor
{
    [SerializeField] GameEvent inventoryCall;
    [SerializeField] ItemData key;

    private Animator animator;
    void Start()
    {
        action = EnvActions.KeyAction;
        animator = GetComponent<Animator>();
    }


    public void GetKeyListenerRequest(Component sender, List<object> data)
    {
        if (data.Count != 2)
            return;

        if (data[0] is not EnvActions.KeyAction)
            return;

        if ((KeyController)data[1] != this)
            return;

        inventoryCall.Raise(this, new List<object>
        {
            action,
            key,
        });

    }

    public void RecieveKeyListener(Component sender, List<object> data)
    {
        if (data.Count != 3)
            return;

        if (data[0] is not KeyController)
            return;

        if ((KeyController)data[0] != this)
            return; 

        if (data[1] is not EnvActions.KeyAction)
            return;

        if (data[2] is not bool)
            return;

        if ((bool)data[2] != true)
            return;

        OpenDoor();
    }

    private void OpenDoor()
    {
        animator.SetBool("Open Door", true);
    }

    public override EnvActions GetEvent()
    {
        return action;
    }

}
