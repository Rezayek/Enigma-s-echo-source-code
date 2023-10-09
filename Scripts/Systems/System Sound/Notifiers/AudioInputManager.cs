using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioInputManager : GenericSingleton<AudioInputManager>
{
   
    private AudioInputActions audioInputActions;


    private void OnEnable()
    {
        audioInputActions = new AudioInputActions();
        audioInputActions.Enable();
    }

    private void OnDisable()
    {
        if (audioInputActions is not null)
            audioInputActions.Disable();
    }

    public Vector2 GetMovement()
    {
        return audioInputActions.Player.Move.ReadValue<Vector2>();
    }

    public bool PlayerSprint()
    {
        return audioInputActions.Player.Sprint.IsPressed();
    }

}
