using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
[RequireComponent(typeof(GameEventListener))]
public class WarningSystem : MonoBehaviour
{
    [SerializeField] List<WarningAbs> warnings;
    [SerializeField] Dictionary<WarningTypes, WarningAbs> warningsDict;
    [SerializeField] GameObject warningUI;
    [SerializeField] private GameGUI gameGUI;
    [SerializeField] GameEvent UIGeneralCall;
    [Header("Options")]
    [SerializeField] private List<ButtonComponents> buttonComponents;
    [Header("Warning Infos")]
    [SerializeField] private Image warningTitle;
    [SerializeField] private TextMeshProUGUI warningText;
    private List<Button> optionsButtons;
    private List<Image> optionsImages;
    private void Start()
    {
        SetWarnings();
        SetLists();
    }

    private void SetWarnings()
    {
        warningsDict = new Dictionary<WarningTypes, WarningAbs>();
        foreach (WarningAbs warningAbs in warnings)
        {
            warningsDict.Add(warningAbs.warningData.warningTypes, warningAbs);
        }
    }

    public void ExecuteWarning(Component sender, List<object> data )
    {
        if (data.Count != 1)
            return;
        if (data[0] is not WarningTypes)
            return;
        WarningAbs warning =  warningsDict[(WarningTypes)data[0]];
        ControlButtonVisisbility(warning);
        warningsDict[(WarningTypes)data[0]].ExecuteWarning(this, optionsButtons, optionsImages);
    }
    private void ControlButtonVisisbility(WarningAbs warning)
    {
        switch (warning.warningData.numberOdOptions)
        {
            case WarningOption.OneOption:
                optionsButtons[0].gameObject.SetActive(true);
                optionsButtons[1].gameObject.SetActive(false);
                optionsButtons[2].gameObject.SetActive(false);
                break;
            case WarningOption.TwoOptions:
                optionsButtons[0].gameObject.SetActive(false);
                optionsButtons[1].gameObject.SetActive(true);
                optionsButtons[2].gameObject.SetActive(true);
                break;
        }
    }

    public void SetWarningInfos(Sprite warningImage, string warningText)
    {
        this.warningTitle.sprite = warningImage;
        this.warningText.text = warningText;
    }

    public void DisplayWarning()
    {
        Debug.Log("DisplayCall");
        UIGeneralCall.Raise(this, new List<object> { gameGUI, UIDisplay.On, warningUI });
    }

    public void ExitWarning()
    {
        FreeListeners();
        UIGeneralCall.Raise(this, new List<object> { gameGUI, UIDisplay.Off, warningUI });
    }

    private void FreeListeners()
    {
        foreach(Button button in optionsButtons)
        {
            button.onClick.RemoveAllListeners();
        }
    }

    [System.Serializable]
    private class ButtonComponents
    {
        public Image image;
        public Button button;
    }

    private void SetLists()
    {
        optionsButtons = new List<Button>();
        optionsImages = new List<Image>();
        foreach(ButtonComponents buttonComponents in buttonComponents)
        {
            optionsButtons.Add(buttonComponents.button);
            optionsImages.Add(buttonComponents.image);
        }
    }
}
