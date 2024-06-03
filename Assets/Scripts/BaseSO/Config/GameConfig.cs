using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/GameConfig")]
public class GameConfig : ScriptableObject
{
    public float moveSpeed;     //player移动速度
    public List<TalentSO> heartUpgradeTalentLevel1;
    public List<TalentSO> heartUpgradeTalentLevel2;
}
