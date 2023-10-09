using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Game Data/Warning/Warning Data")]
public class WarningData : ScriptableObject
{
    public WarningTypes warningTypes;
    public WarningOption numberOdOptions;
    public List<Sprite> buttonsImages;
    public Sprite warningTitle;
    [TextArea(2, 10)]
    public string warningText;
}
