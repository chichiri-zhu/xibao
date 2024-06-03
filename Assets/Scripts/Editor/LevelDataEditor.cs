using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using MoreMountains.Tools;
using UnityEngine.Rendering;

[CustomEditor(typeof(ExcelToLevelData), true)]
[CanEditMultipleObjects]
public class LevelDataEditor : Editor
{
    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        DrawDefaultInspector();

        if (GUILayout.Button("Update Data"))
        {
            var collection = (ExcelToLevelData)target;
            collection.ReadExcel("/Data/test.xlsx");
        }

        serializedObject.ApplyModifiedProperties();
    }
}
