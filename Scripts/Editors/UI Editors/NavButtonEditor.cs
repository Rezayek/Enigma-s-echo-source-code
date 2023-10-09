#if UNITY_EDITOR
using UnityEditor;
[CustomEditor(typeof(NavigateButton))]
public class NavButtonEditor : Editor
{
    private SerializedProperty buttonProp;
    private SerializedProperty navToGUIProp;
    private SerializedProperty gameGUIProp;
    private SerializedProperty stateProp;
    private SerializedProperty UIGeneralManagerProp;
    private void OnEnable()
    {

        buttonProp = serializedObject.FindProperty("button");
        navToGUIProp = serializedObject.FindProperty("navToGUI");
        gameGUIProp = serializedObject.FindProperty("gameGUI");
        stateProp = serializedObject.FindProperty("state");
        UIGeneralManagerProp = serializedObject.FindProperty("UIGeneralManager");

    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();


        EditorGUILayout.PropertyField(buttonProp);
        EditorGUILayout.PropertyField(navToGUIProp);
        EditorGUILayout.PropertyField(gameGUIProp);
        EditorGUILayout.PropertyField(stateProp);
        EditorGUILayout.PropertyField(UIGeneralManagerProp);
        serializedObject.ApplyModifiedProperties();
    }


}
#endif