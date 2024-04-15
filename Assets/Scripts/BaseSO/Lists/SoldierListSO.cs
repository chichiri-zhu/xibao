using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/SoldierListSO")]
public class SoldierListSO : ScriptableObject
{
    public List<ArmsSO> soldierList;
    public List<ArmsSO> enemyList;
    public List<ArmsSO> bossList;

#if UNITY_EDITOR

    public void Refresh()
    {
        soldierList.Clear();
        enemyList.Clear();
        //ListScriptableObjects(Application.dataPath + "/ScriptableObjects/Equips/Armour/");
        ListScriptableObjects("Assets/ScriptableObjects/Soldiers");
    }

    void ListScriptableObjects(string path)
    {
        string[] guids = AssetDatabase.FindAssets("t:ArmsSO", new[] { path });
        foreach (string guid in guids)
        {
            string p = AssetDatabase.GUIDToAssetPath(guid);
            ArmsSO soldier = AssetDatabase.LoadAssetAtPath<ArmsSO>(p);
            if (soldier != null)
            {
                if (soldier.isEnemy)
                {
                    enemyList.Add(soldier);
                }
                else
                {
                    soldierList.Add(soldier);
                }
            }
        }
        EditorUtility.SetDirty(this);
        AssetDatabase.SaveAssets();
    }

#endif
}
