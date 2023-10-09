
using UnityEngine;


[CreateAssetMenu(menuName = "Game Data/Scenarios/Scenario Data")]
public class ScenarioData : ScriptableObject
{
    public Sprite scenarioTitle;
    public Sprite scenarioHeading;
    public Sprite scenarioImage;
    [TextArea(3,10)]
    public string senarioDescription;
    public ScenarioName scenarioName;
}
