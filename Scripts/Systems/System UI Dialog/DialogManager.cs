using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[RequireComponent(typeof(GameEventListener))]
public class DialogManager : MonoBehaviour
{
    [Header("Dialog")]
    [SerializeField] private UnityEngine.GameObject dialogUI;
    [SerializeField] private TextMeshProUGUI npcName;
    [SerializeField] private TextMeshProUGUI dialogText;
    [SerializeField] private Button nextButton;
    [SerializeField] private GameEvent optionsCall;
    [SerializeField] private GameEvent generalUICall;

    [Header("Option One")]
    [SerializeField] private Button optionOneButton;

    [Header("Option Two")]
    [SerializeField] private Button optionTwoButton;

    [Header("Option Three")]
    [SerializeField] private Button optionThreeButton;

    private DialogDataOption dialogDataOption;
    private int currentText = 0;
    // Start is called before the first frame update
    void Start()
    {
        nextButton.onClick.AddListener(delegate { PlayNext();});
        optionOneButton.onClick.AddListener(delegate { ChooseOptionOne();});
        optionTwoButton.onClick.AddListener(delegate { ChooseOptionTwo();});
        optionThreeButton.onClick.AddListener(delegate { ChooseOptionThree();});
    }

    public void DialogListener(Component sender, List<object> data)
    {
        if (data.Count != 2)
            return;
        if (data[0] is not RequestsType && data[1] is not DialogData)
            return;
        RequestsType castEnum1 = (RequestsType)data[0];

        if (castEnum1 != RequestsType.Talk)
            return;

        DialogData dialog = (DialogData)data[1];
        npcName.text = dialog.npc.ToString();
        dialogDataOption = dialog.dialog;

        PlayNext();

        generalUICall.Raise(this, new List<object> { GameGUI.DialogGUI, UIDisplay.On, dialogUI });

    }

    public void ChooseOptionOne()
    {
        if (dialogDataOption.dialogs.Count >= 1)
        {
            DisableOptions();
            dialogDataOption = dialogDataOption.dialogs[0];
            
            currentText = 0;
            PlayNext();
        }
            
            
    }

    public void ChooseOptionTwo()
    {
        if (dialogDataOption.dialogs.Count >= 2)
        {
            DisableOptions();
            dialogDataOption = dialogDataOption.dialogs[1];
            
            currentText = 0;
            PlayNext();
        }
           
    }

    public void ChooseOptionThree()
    {
        if (dialogDataOption.dialogs.Count >= 3)
        {
            DisableOptions();
            dialogDataOption = dialogDataOption.dialogs[2];
            
            currentText = 0;
            PlayNext();
        }
           
    }


    public void PlayNext()
    {

        if (currentText == (dialogDataOption.dialogText.Count - 1) && dialogDataOption.dialogs.Count != 0)
        {

            StopAllCoroutines();
            StartCoroutine(TypeSentences(dialogDataOption.dialogText[currentText]));
            EnableOptions();
            return;
        }
        else if (currentText == (dialogDataOption.dialogText.Count - 1) && dialogDataOption.dialogs.Count == 0)
        {

            generalUICall.Raise(this, new List<object> { GameGUI.DialogGUI, UIDisplay.Off, dialogUI });
            return;
        }
        StopAllCoroutines();
        StartCoroutine(TypeSentences(dialogDataOption.dialogText[currentText]));
        currentText += 1;
    }

    private IEnumerator TypeSentences(string sentence)
    {
        dialogText.text = "";
        foreach(char letter in sentence.ToCharArray())
        {
            dialogText.text += letter;
            yield return null;
        }
    }

    private void EnableOptions()
    {
        for(int i = 0; i < dialogDataOption.dialogs.Count; i++)
        {
            optionsCall.Raise(this, new List<object> { i, UIDisplay.On, dialogDataOption.dialogs[i].optionText });
        }
    }

    private void DisableOptions()
    {
        for (int i = 0; i < dialogDataOption.dialogs.Count; i++)
        {
            optionsCall.Raise(this, new List<object> { i, UIDisplay.Off});
        }
    }
}
