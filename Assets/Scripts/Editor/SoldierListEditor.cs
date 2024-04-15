using UnityEditor;
using UnityEngine;

/// <summary>
/// Add "Refresh" button to IconCollection script
/// </summary>
[CustomEditor(typeof(SoldierListSO))]
public class SoldierListEditor : UnityEditor.Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        var collection = (SoldierListSO)target;

        if (GUILayout.Button("Refresh"))
        {
            collection.Refresh();
        }
    }
}