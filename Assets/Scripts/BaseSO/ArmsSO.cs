using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
using System.Linq;

[CreateAssetMenu(menuName = "ScriptableObjects/Arms")]
public class ArmsSO : ScriptableObject
{
    public string nameString;
    public ArmsType armsType;
    public Sprite icon;
    public bool canCammand; //能否被指挥
    public bool isEnemy;
    public bool isBoss;
    public Transform prefab;
    [SoldierMergeSelector]
    public string soldierMerge;
    public AttributeParam attributeParam;
}

[CustomPropertyDrawer(typeof(SoldierMergeSelectorAttribute))]
public class SoldierMergeSelectorDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);

        // 获取所有继承自TalentBase的类的类型
        var talentTypes = typeof(SoldierMergeBase).Assembly.GetTypes()
            .Where(type => type.IsSubclassOf(typeof(SoldierMergeBase)));


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
public class SoldierMergeSelectorAttribute : PropertyAttribute
{
    // 可以添加一些自定义的属性参数
}
