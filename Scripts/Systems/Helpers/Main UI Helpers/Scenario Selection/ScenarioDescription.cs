using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[RequireComponent(typeof(GameEventListener))]
[RequireComponent(typeof(UIMoveCall))]
public class ScenarioDescription : ButtonAbs
{
    [SerializeField] private Image heading;
    [SerializeField] private Image image;
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private Button play;
    [SerializeField] private GameEvent sceneLoader;
    private ScenarioData scenarioData;
    private UIMoveCall uIMoveCall;


    private void Awake()
    {
        uIMoveCall = GetComponent<UIMoveCall>();
    }
    private void Start()
    {
        play.onClick.AddListener(delegate { Play(); });
        play.onClick.AddListener(delegate { PlaySound(); });
    }

    public void ChangeSenario(Component sender, List<object> data)
    {
        if (data[0] is not ScenarioData)
            return;
        scenarioData = (ScenarioData)data[0];
        heading.sprite = scenarioData.scenarioHeading;
        image.sprite = scenarioData.scenarioImage;
        text.text = scenarioData.senarioDescription;
        uIMoveCall.PlayMoveExposed();
    }

    private void Play()
    {
        sceneLoader.Raise(this, new List<object> { scenarioData.scenarioName });
    }
}
