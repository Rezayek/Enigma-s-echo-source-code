#if UNITY_EDITOR
using UnityEditor;

[CustomEditor(typeof(TreeSettings))]
public class TreeSettingsEditor : Editor
{
    private SerializedProperty treeDataProperty;
    private SerializedProperty horizantalLayersProperty;

    private void OnEnable()
    {
        treeDataProperty = serializedObject.FindProperty("treeData");
        horizantalLayersProperty = serializedObject.FindProperty("horizantalLayers");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        TreeSettings treeSettings = (TreeSettings)target;

        treeSettings.activationDistance = EditorGUILayout.Slider("Activation Distance", treeSettings.activationDistance, 0f, 1000f);
        treeSettings.colliderActiveDistance = EditorGUILayout.Slider("Collider Active Distance", treeSettings.colliderActiveDistance, 0f, 100f);
        treeSettings.nonTreeDensity = EditorGUILayout.Slider("Non-Tree Density", treeSettings.nonTreeDensity, 0f, 1f);
        treeSettings.rayCastHeight = EditorGUILayout.Slider("Raycast Height", treeSettings.rayCastHeight, 1f, 1000f);
       
        treeSettings.verticalLayers = EditorGUILayout.IntSlider("Vertical Layers", treeSettings.verticalLayers, 1, 10);
        treeSettings.horizantalLayers = EditorGUILayout.IntSlider("Horizantal Layers", treeSettings.horizantalLayers, 1, 50);
        treeSettings.horizantalLayersCoef = EditorGUILayout.IntSlider("Horizantal Layers Coef", treeSettings.horizantalLayersCoef, 1, 50);





        EditorGUILayout.Space();

        EditorGUILayout.PropertyField(treeDataProperty, true);

        serializedObject.ApplyModifiedProperties();
    }
}
#endif