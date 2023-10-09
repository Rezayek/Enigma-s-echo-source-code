using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Game Data/Dialog/Dialog Data")]
public class DialogData : ScriptableObject
{
    public Senarios senarios;
    public SenariosVersion senariosVersion;
    public NPC npc;
    public DialogDataOption dialog;
}

[System.Serializable]
public class DialogDataOption
{
    public Options option;
    public string optionText;
    [TextArea(3, 10)]
    public List<string> dialogText;
    public List<AudioClip> dialogAudio;
    public List<DialogDataOption> dialogs;
}



