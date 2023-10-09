using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(GameEventListener))]
public class NavigateButtonExternalCall : ButtonAbs
{
    [SerializeField] private GameObject navToGUI;
    [SerializeField] private GameGUI gameGUI;
    [SerializeField] private UIDisplay state;
    [SerializeField] private GameEvent UIGeneralManager;


    public void ActivateUI(Component sender, List<object> data)
    {

        if (data[0] is not GameGUI)
            return;
        if (gameGUI != (GameGUI)data[0])
            return;
        UIGeneralManager.Raise(this, new List<object> { gameGUI, state, navToGUI });
    }

}
