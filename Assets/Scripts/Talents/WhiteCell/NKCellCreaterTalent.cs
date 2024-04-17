using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class NKCellCreaterTalent : TalentBase
{
    //生成NK细胞
    private void Start()
    {
        GenerativeCell generativeCell = transform.gameObject.GetComponent<GenerativeCell>();
        if(generativeCell == null)
        {
            Destroy(this);
            return;
        }

        if (!generativeCell.isActiveAndEnabled)
        {
            generativeCell.enabled = true;
        }
        ArmsSO arms = AssetManager.Instance.soldierListSO.soldierList.FirstOrDefault(obj => obj.name == "NK");
        generativeCell.InitCell(arms, 4);
        //添加升级组件

        //删除组件
        Destroy(this);
    }
}
