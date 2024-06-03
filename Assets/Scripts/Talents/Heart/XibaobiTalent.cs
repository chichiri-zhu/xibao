using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XibaobiTalent : TalentBase
{
    //超级细胞壁:超级细胞受到攻击时反射5点伤害
    private HealthSystem healthSystem;
    private void Start()
    {
        Player player = GameManager.Instance.GetPlayer();
        if(player != null)
        {
            healthSystem = player.GetComponent<HealthSystem>();
            if(healthSystem != null)
            {
                healthSystem.OnDamaged += HealthSystem_OnDamaged;
            }
        }
    }

    private void HealthSystem_OnDamaged(object sender, OnDamagedArgs e)
    {
        UnitBase sourceUnit = e.sourceUnit;
        if(sourceUnit != null)
        {
            HealthSystem sourceHealthSystem = sourceUnit.GetComponent<HealthSystem>();
            if(sourceHealthSystem != null)
            {
                sourceHealthSystem.Damage(5);
            }
        }
    }

    private void OnDestroy()
    {
        if (healthSystem != null)
        {
            healthSystem.OnDamaged -= HealthSystem_OnDamaged;
        }
    }
}
