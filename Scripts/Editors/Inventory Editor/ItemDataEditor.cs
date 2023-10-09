#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ItemData))]
public class ItemDataEditor : Editor
{
    private SerializedProperty NameProp;
    private SerializedProperty categoryProp;
    private SerializedProperty objectTypeProp;
    private SerializedProperty consummableTypeProp;
    private SerializedProperty QteProp;
    private SerializedProperty spriteProp;
    private SerializedProperty itemPrefabProp;
    private SerializedProperty consummeGainProp;
    private SerializedProperty itemInspectorScaleProp;
    private SerializedProperty itemDescriptionProp;
    private SerializedProperty itemPagesProp;

    private const float TextAreaHeightMultiplier = 8f;

    private void OnEnable()
    {
        NameProp = serializedObject.FindProperty("itemName");
        categoryProp = serializedObject.FindProperty("category");
        objectTypeProp = serializedObject.FindProperty("objectType");
        consummableTypeProp = serializedObject.FindProperty("consummableType");
        QteProp = serializedObject.FindProperty("Qte");
        itemPrefabProp = serializedObject.FindProperty("itemPrefab");
        spriteProp = serializedObject.FindProperty("sprite");
        consummeGainProp = serializedObject.FindProperty("consummeGain");
        itemInspectorScaleProp = serializedObject.FindProperty("itemInspectorScale");
        itemDescriptionProp = serializedObject.FindProperty("itemDescription");
        itemPagesProp = serializedObject.FindProperty("pages");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        EditorGUILayout.PropertyField(NameProp);
        EditorGUILayout.PropertyField(categoryProp);
        Category selectedCategory = (Category)categoryProp.enumValueIndex;

        if (selectedCategory == Category.Consummable)
        {
            EditorGUILayout.PropertyField(consummableTypeProp);
            EditorGUILayout.PropertyField(QteProp);
            EditorGUILayout.PropertyField(consummeGainProp);
            serializedObject.ApplyModifiedProperties();
        }
        else
        {
            EditorGUILayout.PropertyField(objectTypeProp);
            
            serializedObject.ApplyModifiedProperties();
        }
        ObjectType selectedType = (ObjectType)objectTypeProp.enumValueIndex;

        if (selectedType == ObjectType.Book)
        {
            EditorGUILayout.PropertyField(itemPagesProp);
            serializedObject.ApplyModifiedProperties();
        }

        EditorGUILayout.PropertyField(spriteProp);
        EditorGUILayout.PropertyField(itemPrefabProp);
        EditorGUILayout.PropertyField(itemInspectorScaleProp);
        // Draw the label for itemDescriptionProp
        GUIContent descriptionLabel = new GUIContent(itemDescriptionProp.displayName);
        EditorGUILayout.LabelField(descriptionLabel);

        // Get the height of the text area based on the length of the string
        float height = EditorStyles.textArea.CalcHeight(new GUIContent(itemDescriptionProp.stringValue), EditorGUIUtility.currentViewWidth);
        Rect controlRect = EditorGUILayout.GetControlRect(false, height * TextAreaHeightMultiplier);

        // Draw the resizable text area
        EditorGUI.BeginChangeCheck();
        string newDescription = EditorGUI.TextArea(controlRect, itemDescriptionProp.stringValue, EditorStyles.textArea);
        if (EditorGUI.EndChangeCheck())
        {
            itemDescriptionProp.stringValue = newDescription;
        }

        serializedObject.ApplyModifiedProperties();
    }

}
#endif