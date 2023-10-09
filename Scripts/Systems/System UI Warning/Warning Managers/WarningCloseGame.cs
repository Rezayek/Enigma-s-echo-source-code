using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "Game Data/Warning/Warnings/Warning Close Game")]
public class WarningCloseGame : WarningAbs
{
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
        optionsButtons[1].onClick.AddListener(delegate { QuitGame(); });
        optionsButtons[2].onClick.AddListener(delegate { warningSystem.ExitWarning(); });
        warningSystem.SetWarningInfos(warningData.warningTitle, warningData.warningText);
        warningSystem.DisplayWarning();
    }


    private void QuitGame()
    {
#if UNITY_EDITOR
        // If running in the Unity Editor, stop the play mode
        UnityEditor.EditorApplication.isPlaying = false;
#else
        // If running in a standalone build, quit the application
        Application.Quit();
#endif
    }
}
