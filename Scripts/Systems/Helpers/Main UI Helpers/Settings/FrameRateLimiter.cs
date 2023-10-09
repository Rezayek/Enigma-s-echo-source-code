using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(GameEventListener))]
public class FrameRateLimiter : AbstractSettings
{
    [SerializeField] private TextMeshProUGUI frameText;

    [SerializeField] private float updateInterval = 0.5f; // Interval to update the FPS display
    private float accum = 0.0f; // FPS accumulated over the interval
    private int frames = 0; // Frames drawn over the interval
    private float timeLeft; // Time left for current interval
    private float currentFPS; // Current calculated FPS
    void Awake()
    {
        SetValue();
        timeLeft = updateInterval;
    }
    private void Update()
    {
        timeLeft -= Time.deltaTime;
        accum += Time.timeScale / Time.deltaTime;
        frames++;

        // Interval ended - update the FPS display
        if (timeLeft <= 0.0f)
        {
            currentFPS = accum / frames;
            timeLeft = updateInterval;
            accum = 0.0f;
            frames = 0;

            // Display or use the currentFPS value here
            frameText.text =  currentFPS.ToString("F2") + " fps";
        }
        
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
                Application.targetFrameRate = 30;
                break;
            case 1:
                Application.targetFrameRate = 45;
                break;
            case 2:
                Application.targetFrameRate = 60;
                break;
        }
    }
}
