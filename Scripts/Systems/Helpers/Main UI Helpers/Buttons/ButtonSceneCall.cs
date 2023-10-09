using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ButtonSceneCall : ButtonAbs
{
    [SerializeField] private Button button;
    [SerializeField] private GameEvent sceneLoader;
    [SerializeField] private ScenarioName scenarioName;
    void Start()
    {
        button.onClick.AddListener(delegate { SceneLoader(); });
        button.onClick.AddListener(delegate { PlaySound(); });
    }

    private void SceneLoader()
    {
        sceneLoader.Raise(this, new List<object> { scenarioName });
    }
}
