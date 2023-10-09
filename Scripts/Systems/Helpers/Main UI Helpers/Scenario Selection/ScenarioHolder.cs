using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScenarioHolder : MonoBehaviour
{
    [SerializeField] private Button button;
    [SerializeField] private Image scenarioTitle;
    [SerializeField] private GameEvent scenarioDesciptionCall;

    private ScenarioData scenarioData;



    public void SetElements(ScenarioData scenarioData, bool isTheFirst)
    {
        this.scenarioData = scenarioData;
        scenarioTitle.sprite = scenarioData.scenarioTitle;
        button.onClick.AddListener(delegate { CallDescription(); });
        if (isTheFirst)
            CallDescription();

    }

    private void CallDescription()
    {
        scenarioDesciptionCall.Raise(this, new List<object> { scenarioData });
    }
}
