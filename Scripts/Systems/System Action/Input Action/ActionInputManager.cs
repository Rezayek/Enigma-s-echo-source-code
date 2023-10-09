using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionInputManager : GenericSingleton<ActionInputManager>
{
    private PlayerActionInput inputActions;

    private void OnEnable()
    {
        // Instantiate or assign a valid instance of PlayerActionInput
        inputActions = new PlayerActionInput();
        inputActions.Enable();
    }

    private void OnDisable()
    {
        if (inputActions is not null)
            inputActions.Disable();
    }

    public bool TorchControl()
    {
        return inputActions.PlayerAction.Torch.triggered;
    }

    public bool OnLoot()
    {
        return inputActions.PlayerAction.Global.triggered;
    }

    public bool OnInspect()
    {
        return inputActions.PlayerAction.Inspect.triggered;
    }

    public bool OnHealth()
    {
        return inputActions.PlayerAction.Health.triggered;
    }

    public bool OnSanity()
    {
        return inputActions.PlayerAction.Sanity.triggered;
    }
    public Vector2 OnMouseMove()
    {
        return inputActions.PlayerAction.Mouse.ReadValue<Vector2>();
    }

    public bool OnMouseLeftPress()
    {
        return inputActions.PlayerAction.MousePressed.IsPressed();
    }

    public bool OnMouseLeftClick()
    {
        return inputActions.PlayerAction.Click.triggered;
    }

    public bool OnBag()
    {
        return inputActions.PlayerAction.Bag.triggered;
    }

    public bool OnPause()
    {
        return inputActions.PlayerAction.Pause.triggered;
    }
}
