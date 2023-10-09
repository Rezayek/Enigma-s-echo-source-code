using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(GameEventListener))]
public class GameResolution : AbstractSettings
{
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
        switch (PlayerPrefs.GetInt(settingsType.ToString()))
        {
            case 0:
                Screen.SetResolution(1920, 1080, Screen.fullScreen);
                break;
            case 1:
                Screen.SetResolution(2560, 1440, Screen.fullScreen);
                break;
        }
    }
}
