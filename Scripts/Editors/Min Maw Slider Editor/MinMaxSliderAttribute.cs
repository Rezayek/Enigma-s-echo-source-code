using UnityEngine;

public class MinMaxSliderAttribute : PropertyAttribute
{
    public float minValue;
    public float maxValue;

    public MinMaxSliderAttribute(float minValue, float maxValue)
    {
        this.minValue = minValue;
        this.maxValue = maxValue;
    }
}

[System.Serializable]
public struct FloatRange
{
    public float min;
    public float max;

    public FloatRange(float min, float max)
    {
        this.min = min;
        this.max = max;
    }
}
