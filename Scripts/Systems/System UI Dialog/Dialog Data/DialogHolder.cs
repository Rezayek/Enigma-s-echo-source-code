using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogHolder : MonoBehaviour
{
    [SerializeField] private  List<DialogData> dialogs;

    public DialogData GetDialog()
    {
        string se = PlayerPrefs.GetString(PlayerPrefsNames.CurrentSenario.ToString());
        string sv = PlayerPrefs.GetString(PlayerPrefsNames.CurrentSenarioVersion.ToString());
        for(int i = 0; i < dialogs.Count; i++)
        {
            if(dialogs[i].senarios.ToString() == se && dialogs[i].senariosVersion.ToString() == sv)
            {
                return dialogs[i];
            }
        }
        return null;
    }
}
