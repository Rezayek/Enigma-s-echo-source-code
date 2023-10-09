#if UNITY_EDITOR
using UnityEditor;
[CustomEditor(typeof(SoundsData))]
public class SoundsDataEditor : Editor
{
    private SerializedProperty audioPlayerHolderProp;
    private SerializedProperty audioVolumeBProp;
    private SerializedProperty audioVolumeNBProp;
    private SerializedProperty soundListProp;
    private void OnEnable()
    {
        audioPlayerHolderProp = serializedObject.FindProperty("audioPlayerHolder");
        audioVolumeNBProp = serializedObject.FindProperty("audioVolumeNB");
        audioVolumeBProp = serializedObject.FindProperty("audioVolumeB");
        soundListProp = serializedObject.FindProperty("soundList");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.PropertyField(audioPlayerHolderProp);
        EditorGUILayout.PropertyField(audioVolumeNBProp);
        EditorGUILayout.PropertyField(audioVolumeBProp);

        EditorGUILayout.Space();

        EditorGUILayout.PropertyField(soundListProp, true);
        serializedObject.ApplyModifiedProperties();
    }
}
#endif