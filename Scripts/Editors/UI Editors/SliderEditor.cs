#if UNITY_EDITOR
using UnityEditor;


[CustomEditor(typeof(SliderValueSaver))]
public class SliderEditor : Editor
{
    private SerializedProperty sliderProp;
    private SerializedProperty minValueProp;
    private SerializedProperty maxValueProp;
    private SerializedProperty selectionTypeProp;
    private SerializedProperty addEventProp;
    private SerializedProperty settingsEventProp;

    void OnEnable()
    {
        sliderProp = serializedObject.FindProperty("slider");
        minValueProp = serializedObject.FindProperty("minValue");
        maxValueProp = serializedObject.FindProperty("maxValue");
        selectionTypeProp = serializedObject.FindProperty("selectionType");
        addEventProp = serializedObject.FindProperty("addEvent");
        settingsEventProp = serializedObject.FindProperty("settingsEvent");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.PropertyField(sliderProp);
        EditorGUILayout.PropertyField(minValueProp);
        EditorGUILayout.PropertyField(maxValueProp);
        EditorGUILayout.PropertyField(selectionTypeProp);
        EditorGUILayout.PropertyField(addEventProp);
        bool addEvent = addEventProp.boolValue;

        if (addEvent)
        {
            EditorGUILayout.PropertyField(settingsEventProp);
            serializedObject.ApplyModifiedProperties();
        }

        serializedObject.ApplyModifiedProperties();
    }
}
#endif