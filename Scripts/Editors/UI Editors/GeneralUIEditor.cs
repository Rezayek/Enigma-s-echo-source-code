#if UNITY_EDITOR
using UnityEditor;
[CustomEditor(typeof(SystemUIGeneral))]
public class GeneralUIEditor : Editor
{
    private SerializedProperty isGameSceneProp;
    private SerializedProperty playModeGUIProp;
    private SerializedProperty crossHairProp;
    private SerializedProperty cinemachineBrainProp;
    private SerializedProperty UIEffectCallProp;
    private SerializedProperty mainMenuProp;
    private SerializedProperty mainMenuEnumProp;

    private void OnEnable()
    {
        isGameSceneProp = serializedObject.FindProperty("isGameScene");
        playModeGUIProp = serializedObject.FindProperty("playModeGUI");
        crossHairProp = serializedObject.FindProperty("crossHair");
        cinemachineBrainProp = serializedObject.FindProperty("cinemachineBrain");
        UIEffectCallProp = serializedObject.FindProperty("UIEffectCall");
        mainMenuProp = serializedObject.FindProperty("mainMenu");
        mainMenuEnumProp = serializedObject.FindProperty("mainMenuEnum");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.PropertyField(isGameSceneProp);
        bool isGameScene = isGameSceneProp.boolValue;

        if (isGameScene)
        {
            EditorGUILayout.PropertyField(playModeGUIProp);
            EditorGUILayout.PropertyField(crossHairProp);
            EditorGUILayout.PropertyField(cinemachineBrainProp);
            serializedObject.ApplyModifiedProperties();
        }
        else
        {
            EditorGUILayout.PropertyField(mainMenuProp);
            EditorGUILayout.PropertyField(mainMenuEnumProp);
            serializedObject.ApplyModifiedProperties();
        }

        EditorGUILayout.PropertyField(UIEffectCallProp);
        serializedObject.ApplyModifiedProperties();





    }
}
#endif