using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhiteCellUpgrade : UpgradeBase
{
    [SerializeField] private List<TalentSO> upgradeTalentList = new List<TalentSO>();
    public override IEnumerator UpgradeDone()
    {
        if (upgradeTalentList != null && upgradeTalentList.Count > 0)
        {
            yield return StartCoroutine(ShowTalentChooseUI(upgradeTalentList));

        }
        transform.gameObject.AddComponent<WhiteCellUpgradeTwo>();
        yield return null;
    }

    private IEnumerator ShowTalentChooseUI(List<TalentSO> talentList)
    {
        talentChosen = TalentChosenEnum.padding;

        TalentChooseUI.Instance.Show(talentList, (TalentSO talent) =>
        {
            Type talentType = Type.GetType(talent.selectedTalent);
            if (talentType != null)
            {
                transform.gameObject.AddComponent(talentType);
            }
            talentChosen = TalentChosenEnum.done;
        }, () =>
        {
            talentChosen = TalentChosenEnum.cancel;
        },
        true);

        yield return null;
    }
}
