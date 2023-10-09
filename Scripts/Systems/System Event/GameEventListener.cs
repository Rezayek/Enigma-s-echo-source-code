using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class CustomGameEvent: UnityEvent<Component, List<object>>
{

}

public class GameEventListener : MonoBehaviour
{
    [Tooltip("Event to register with.")]
    public GameEvent gameEvent;

    [Tooltip("Response to invoke when Event is raised.")]
    public CustomGameEvent response;
    public void OnEnable()
    {
        gameEvent.RegisterListener(this);
    }

    private void OnDisable()
    {
        gameEvent.UnregisterListener(this);
    }

    public void OnEventRaised(Component sender , List<object> data)
    {
        response.Invoke(sender, data);
    }
}
