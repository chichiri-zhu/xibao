using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/BuildingListSO")]
public class BuildingListSO : ScriptableObject
{
    public List<BuildingTypeSO> buildingList;

#if UNITY_EDITOR

    public void Refresh()
    {
        buildingList.Clear();
        //ListScriptableObjects(Application.dataPath + "/ScriptableObjects/Equips/Armour/");
        ListScriptableObjects("Assets/ScriptableObjects/BuildingType");
    }

    void ListScriptableObjects(string path)
    {
        string[] guids = AssetDatabase.FindAssets("t:BuildingTypeSO", new[] { path });
        foreach (string guid in guids)
        {
            string p = AssetDatabase.GUIDToAssetPath(guid);
            BuildingTypeSO buildingType = AssetDatabase.LoadAssetAtPath<BuildingTypeSO>(p);
            if (buildingType != null)
            {
                buildingList.Add(buildingType);
            }
        }
        EditorUtility.SetDirty(this);
        AssetDatabase.SaveAssets();
    }

#endif
}
