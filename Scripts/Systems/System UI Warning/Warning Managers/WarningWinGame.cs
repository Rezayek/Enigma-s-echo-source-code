using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "Game Data/Warning/Warnings/Warning Win Game")]
public class WarningWinGame : WarningAbs
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
        optionsImages[0].sprite = warningData.buttonsImages[0];
        optionsButtons[0].onClick.AddListener(delegate { CallScene(); });
        warningSystem.SetWarningInfos(warningData.warningTitle, warningData.warningText);
        warningSystem.DisplayWarning();
    }

    private void CallScene()
    {
        sceneLoader.Raise(new Component(), new List<object> { scenarioName });
    }
}
