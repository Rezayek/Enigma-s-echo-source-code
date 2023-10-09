using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public abstract class WarningAbs : ScriptableObject
{
    public WarningData warningData;
    protected WarningSystem warningSystem;
    protected List<Button> optionsButtons;
    protected List<Image> optionsImages;
    public abstract void ExecuteWarning(WarningSystem warningSystem, List<Button> optionsButtons, List<Image> optionsImages);
}
