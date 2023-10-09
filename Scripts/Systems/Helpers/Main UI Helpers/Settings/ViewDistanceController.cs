using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(GameEventListener))]
public class ViewDistanceController : AbstractSettings
{

    [SerializeField] Terrain terrain;
    [SerializeField] float detailObjectDistance = 200f;
    [SerializeField] float treeDistance = 1000f;
    void Awake()
    {
        SetValue();
    }

    public override void SetSettingsValue(Component sender, List<object> data)
    {
        if (data[0] is not PlayerPrefsNames)
            return;
        if ((PlayerPrefsNames)data[0] != settingsType)
            return;
        SetValue();
    }
    public override void SetValue()
    {
        terrain.detailObjectDistance = detailObjectDistance * PlayerPrefs.GetFloat(settingsType.ToString());
        terrain.treeDistance = treeDistance * PlayerPrefs.GetFloat(settingsType.ToString());
    }
}
