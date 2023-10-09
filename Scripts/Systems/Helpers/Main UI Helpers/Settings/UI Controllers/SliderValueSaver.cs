using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
public class SliderValueSaver : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private float minValue;
    [SerializeField] private float maxValue;
    [SerializeField] private PlayerPrefsNames selectionType;
    [SerializeField] private bool addEvent;
    [SerializeField] private GameEvent settingsEvent;
    private float currentValue ;
    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.HasKey(selectionType.ToString()))
        {
            slider.value = PlayerPrefs.GetFloat(selectionType.ToString());

        }
        else
        {
            PlayerPrefs.SetFloat(selectionType.ToString(), maxValue);
            slider.value = maxValue;
        }
        currentValue = slider.value;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentValue != slider.value)
            UpdateValue();
    }
    private void UpdateValue()
    {
        PlayerPrefs.SetFloat(selectionType.ToString(), slider.value);
        if (addEvent)
            settingsEvent.Raise(this, new List<object> { selectionType });
    }
}
