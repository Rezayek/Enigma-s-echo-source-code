#if UNITY_EDITOR
using UnityEditor;

[CustomEditor(typeof(SoundData))]
public class SoundDataEditor : Editor
{
    private SerializedProperty soundCategoryProp;
    private SerializedProperty soundType1Prop;
    private SerializedProperty soundType2Prop;
    private SerializedProperty audioClipProp;
    private SerializedProperty loopIntervalProp;
    private SerializedProperty volumeProp;
    private void OnEnable()
    {
        soundCategoryProp = serializedObject.FindProperty("soundCategory");
        soundType1Prop = serializedObject.FindProperty("soundType1");
        soundType2Prop = serializedObject.FindProperty("soundType2");
        audioClipProp = serializedObject.FindProperty("audioClip");
        loopIntervalProp = serializedObject.FindProperty("loopInterval");
        volumeProp = serializedObject.FindProperty("volume");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.PropertyField(soundCategoryProp);
        SoundCategory selectedCategory = (SoundCategory)soundCategoryProp.enumValueIndex;

        if (selectedCategory == SoundCategory.NoBackground)
        {
            EditorGUILayout.PropertyField(soundType1Prop);
            serializedObject.ApplyModifiedProperties();
        }
        else
        {
            EditorGUILayout.PropertyField(soundType2Prop);
            EditorGUILayout.PropertyField(loopIntervalProp);
            serializedObject.ApplyModifiedProperties();
        }
        EditorGUILayout.PropertyField(volumeProp);

        SoundType1 selectedSoundType = (SoundType1)soundType1Prop.enumValueIndex;

        EditorGUILayout.PropertyField(audioClipProp);

        serializedObject.ApplyModifiedProperties();
        


        
        
    }
}
#endif