using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/BuildingType")]
public class BuildingTypeSO : ScriptableObject
{
    public string nameString;       //细胞名称
    public Transform prefab;        
    public Sprite sprite;
    public float constructionTimerMax;
    public int goldAmount;          //消耗金币
    public BuildingParam buildingParam;
    public List<TalentSO> talents;
    public AttributeParam attributeParam;
}
