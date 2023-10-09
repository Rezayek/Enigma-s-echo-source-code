#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;


[CustomEditor (typeof(MapPreview))]
public class MapGeneratorEditor : Editor
{
    public override void OnInspectorGUI()
    {
        MapPreview mapPreview = (MapPreview)target;
        if(DrawDefaultInspector())
        {
            if(mapPreview.autoUpdate)
            {
                mapPreview.DrawMapInEditor();
            }
        }
        if(GUILayout.Button ("Gnerate"))
        {
            mapPreview.DrawMapInEditor();
        }
        if (GUILayout.Button("Save"))
        {
            mapPreview.SaveAsPrefab();
        }


    }
}
#endif