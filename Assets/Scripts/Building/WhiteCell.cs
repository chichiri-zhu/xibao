using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//造血白细胞(近战兵营)
public class WhiteCell : BuildingBase
{
    private UpgradeBase upgradeBase;
    private int level = 1;
    [SerializeField] private SpriteRenderer image;
    [SerializeField] private List<Sprite> levelSpriteList;

    protected override void OnStart()
    {
        upgradeBase = GetComponent<UpgradeBase>();
        if(upgradeBase != null)
        {
            upgradeBase.OnUpgradeDone += UpgradeBase_OnUpgradeDone;
        }
        InitSprite();
    }

    private void UpgradeBase_OnUpgradeDone(object sender, System.EventArgs e)
    {
        level++;
        InitSprite();
    }

    private void InitSprite()
    {
        if(levelSpriteList.Count >= level - 1)
        {
            image.sprite = levelSpriteList[level - 1];
        }
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        if(upgradeBase != null)
        {
            upgradeBase.OnUpgradeDone -= UpgradeBase_OnUpgradeDone;
        }
    }
}
