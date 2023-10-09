using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractSettings : MonoBehaviour
{
    public PlayerPrefsNames settingsType;
    public abstract void SetSettingsValue(Component sender, List<object> data);
    public abstract void SetValue();
}
