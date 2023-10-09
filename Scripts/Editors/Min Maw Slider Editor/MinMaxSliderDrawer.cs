#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(MinMaxSliderAttribute))]
public class MinMaxSliderDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        MinMaxSliderAttribute rangeAttribute = attribute as MinMaxSliderAttribute;

        EditorGUI.BeginProperty(position, label, property);

        SerializedProperty minProperty = property.FindPropertyRelative("min");
        SerializedProperty maxProperty = property.FindPropertyRelative("max");

        float labelWidth = EditorGUIUtility.labelWidth;
        float fieldWidth = (position.width - labelWidth) / 3f;
        float sliderWidth = position.width - labelWidth - (fieldWidth * 1f) - EditorGUIUtility.standardVerticalSpacing;

        Rect labelPosition = new Rect(position.x, position.y, labelWidth, position.height);
        Rect minFieldPosition = new Rect(position.x + labelWidth, position.y, fieldWidth / 2, position.height);
        Rect sliderPosition = new Rect(position.x + labelWidth + (fieldWidth * .5f) + EditorGUIUtility.standardVerticalSpacing, position.y, sliderWidth, position.height);
        Rect maxFieldPosition = new Rect(position.x + labelWidth + fieldWidth / 2 + sliderWidth + EditorGUIUtility.standardVerticalSpacing, position.y, fieldWidth / 2, position.height);

        EditorGUI.LabelField(labelPosition, label);

        float minValue = minProperty.floatValue;
        float maxValue = maxProperty.floatValue;

        EditorGUI.BeginChangeCheck();
        minValue = EditorGUI.FloatField(minFieldPosition, Mathf.Round(minValue * 1000f) / 1000f);
        EditorGUI.MinMaxSlider(sliderPosition, ref minValue, ref maxValue, rangeAttribute.minValue, rangeAttribute.maxValue);
        maxValue = EditorGUI.FloatField(maxFieldPosition, Mathf.Round(maxValue * 1000f) / 1000f);
        if (EditorGUI.EndChangeCheck())
        {
            minProperty.floatValue = Mathf.Clamp(minValue, rangeAttribute.minValue, maxValue);
            maxProperty.floatValue = Mathf.Clamp(maxValue, minValue, rangeAttribute.maxValue);
        }

        EditorGUI.EndProperty();
    }
}
#endif