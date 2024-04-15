using UnityEditor;
using UnityEngine;

/// <summary>
/// Add "Refresh" button to IconCollection script
/// </summary>
[CustomEditor(typeof(BuildingListSO))]
public class BuildingTypeListEditor : UnityEditor.Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        var collection = (BuildingListSO)target;

        if (GUILayout.Button("Refresh"))
        {
            collection.Refresh();
        }
    }
}