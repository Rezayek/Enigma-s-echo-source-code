using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateData : ScriptableObject
{
    public event System.Action OnValueUpdated;
    public bool autoUpdate;


    #if UNITY_EDITOR

    public void OnValidate()
    {
        if(autoUpdate)
        {
            UnityEditor.EditorApplication.update += NotifyOfUpdatedValues;
        }
    }

    public void NotifyOfUpdatedValues()
    {

        UnityEditor.EditorApplication.update -= NotifyOfUpdatedValues;

        if (OnValueUpdated != null)
        {
            OnValueUpdated();
        }
    }
    #endif
}
