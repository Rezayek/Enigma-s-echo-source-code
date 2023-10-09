using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Game Data/Scenarios/Scenarios Data")]
public class ScenariosData : ScriptableObject
{
    public List<ScenarioData> scenarios;
}
