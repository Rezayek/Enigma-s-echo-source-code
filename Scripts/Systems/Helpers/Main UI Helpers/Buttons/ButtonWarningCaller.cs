using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ButtonWarningCaller : ButtonAbs
{
    [SerializeField] private Button button;
    [SerializeField] private WarningTypes warningTypes;
    [SerializeField] private GameEvent warningCall;

    private void Start()
    {
        button.onClick.AddListener( delegate { CallWarning(); });
        button.onClick.AddListener(delegate { PlaySound(); });
    }

    private void CallWarning()
    {
        warningCall.Raise(this, new List<object> { warningTypes});
    }
}
