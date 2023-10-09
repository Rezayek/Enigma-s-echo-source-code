using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class NavigateButton : ButtonAbs
{
    [SerializeField] private Button button;
    [SerializeField] private GameObject navToGUI;
    [SerializeField] private GameGUI gameGUI;
    [SerializeField] private UIDisplay state;
    [SerializeField] private GameEvent UIGeneralManager;
    
    

    private void Start()
    {
        button.onClick.AddListener(delegate { ActivateUI(); });
        button.onClick.AddListener(delegate { PlaySound(); });
    }

    private void ActivateUI()
    {
        UIGeneralManager.Raise(this, new List<object> { gameGUI, state, navToGUI });
    }

    
}
