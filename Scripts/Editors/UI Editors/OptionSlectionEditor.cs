#if UNITY_EDITOR
using UnityEditor;

[CustomEditor(typeof(OptionSelecter))]
public class OptionSlectionEditor : Editor
{
    private SerializedProperty nextButtonProp;
    private SerializedProperty previousButtonProp;
    private SerializedProperty optionHolderProp;
    private SerializedProperty optionsProp;
    private SerializedProperty selectionTypeProp;
    private SerializedProperty addEventProp;
    private SerializedProperty settingsEventProp;

    void OnEnable()
    {
        nextButtonProp = serializedObject.FindProperty("nextButton");
        previousButtonProp = serializedObject.FindProperty("previousButton");
        optionHolderProp = serializedObject.FindProperty("optionHolder");
        optionsProp = serializedObject.FindProperty("options");
        selectionTypeProp = serializedObject.FindProperty("selectionType");
        addEventProp = serializedObject.FindProperty("addEvent");
        settingsEventProp = serializedObject.FindProperty("settingsEvent");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.PropertyField(nextButtonProp);
        EditorGUILayout.PropertyField(previousButtonProp);
        EditorGUILayout.PropertyField(optionHolderProp);
        EditorGUILayout.PropertyField(optionsProp);
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