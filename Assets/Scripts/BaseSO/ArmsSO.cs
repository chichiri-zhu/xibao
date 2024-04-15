using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Arms")]
public class ArmsSO : ScriptableObject
{
    public string nameString;
    public ArmsType armsType;
    public bool canCammand; //能否被指挥
    public bool isEnemy;
    public bool isBoss;
    public Transform prefab;
    public AttributeParam attributeParam;
}
