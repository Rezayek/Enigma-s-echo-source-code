using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "Game Data/Warning/Warnings/Warning Return Main")]
public class WarningReturnMain : WarningAbs
{
    [SerializeField] private ScenarioName scenarioName;
    [SerializeField] private GameEvent sceneLoader;
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
        optionsButtons[1].onClick.AddListener(delegate { CallScene(); });
        optionsButtons[2].onClick.AddListener(delegate { warningSystem.ExitWarning(); });
        warningSystem.SetWarningInfos(warningData.warningTitle, warningData.warningText);
        warningSystem.DisplayWarning();
    }

    private void CallScene()
    {
        sceneLoader.Raise(new Component(), new List<object> { scenarioName });
    }
}
