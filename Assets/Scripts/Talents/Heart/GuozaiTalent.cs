using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuozaiTalent : TalentBase
{
    private void Start()
    {
        GameManager.Instance.towerUpgradeDec += 3;
    }
}
