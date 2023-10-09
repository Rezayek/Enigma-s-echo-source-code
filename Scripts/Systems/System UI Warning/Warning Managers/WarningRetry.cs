using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[CreateAssetMenu(menuName = "Game Data/Warning/Warnings/Warning Retry Game")]
public class WarningRetry : WarningAbs
{

    [SerializeField] private ScenarioName scenarioName;
    [SerializeField] private GameEvent sceneLoader;
    [SerializeField] private GameEvent checkPointEvent;
    public override void ExecuteWarning(WarningSystem warningSystem, List<Button> optionsButtons, List<Image> optionsImages)
    {
        this.warningSystem = warningSystem;
        this.optionsButtons = optionsButtons;
        this.optionsImages = optionsImages;
        SetButtons();
    }

    private void SetButtons()
    {
        optionsImages[1].sprite = warningData.buttonsImages[0];
        optionsImages[2].sprite = warningData.buttonsImages[1];
        optionsButtons[1].onClick.AddListener(delegate { CallCheckPoint(); });
        optionsButtons[2].onClick.AddListener(delegate { CallScene(); });
        warningSystem.SetWarningInfos(warningData.warningTitle, warningData.warningText);
        warningSystem.DisplayWarning();
    }

    private void CallCheckPoint()
    {
        warningSystem.ExitWarning();
        checkPointEvent.Raise(warningSystem, new List<object> { CheckPointCallTypes.Respawn });
        
    }

    private void CallScene()
    {
        sceneLoader.Raise(new Component(), new List<object> { scenarioName });
    }
}
