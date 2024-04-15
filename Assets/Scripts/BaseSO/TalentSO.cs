using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Linq;
using System;

[CreateAssetMenu(menuName = "ScriptableObjects/Talent")]
public class TalentSO : ScriptableObject
{
    public string nameString;
    [TalentSelector]
    public string selectedTalent;
    public string content;
}


[CustomPropertyDrawer(typeof(TalentSelectorAttribute))]
public class TalentSelectorDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);

        // 获取所有继承自TalentBase的类的类型
        var talentTypes = typeof(TalentBase).Assembly.GetTypes()
            .Where(type => type.IsSubclassOf(typeof(TalentBase)));


        // 将类型转换为字符串数组
        //var talentTypeNames = talentTypes.Select(type => type.Name).ToArray();
        var talentTypeNames = talentTypes.Select(type => type.Name).ToArray();

        // 获取当前选择的类型的索引
        var selectedIndex = Array.IndexOf(talentTypeNames, property.stringValue);

        // 绘制下拉列表
        selectedIndex = EditorGUI.Popup(position, label.text, selectedIndex, talentTypeNames);

        // 更新选择的类型
        if (selectedIndex >= 0 && selectedIndex < talentTypeNames.Length)
        {
            property.stringValue = talentTypeNames[selectedIndex];
        }

        EditorGUI.EndProperty();
    }
}

[System.AttributeUsage(System.AttributeTargets.Field)]
public class TalentSelectorAttribute : PropertyAttribute
{
    // 可以添加一些自定义的属性参数
}