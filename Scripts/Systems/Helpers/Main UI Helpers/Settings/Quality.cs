using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(GameEventListener))]
public class Quality : AbstractSettings
{

    // Start is called before the first frame update
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
        QualitySettings.SetQualityLevel(PlayerPrefs.GetInt(settingsType.ToString()));
    }
}
