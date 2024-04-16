using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartUpgradeTwo : UpgradeBase
{
    public void Awake()
    {
        upgradeAmount = 2;
    }

    public override IEnumerator _DoUpgrade()
    {
        HeartBuilding heartBuilding = building as HeartBuilding;
        List<TalentSO> upgradeTalentList = heartBuilding.upgradeTalentList;
        yield return null;
        if (upgradeTalentList != null && upgradeTalentList.Count > 0)
        {
            yield return StartCoroutine(ShowTalentChooseUI(upgradeTalentList));

        }

        yield return null;
    }

    private IEnumerator ShowTalentChooseUI(List<TalentSO> talentList)
    {
        talentChosen = TalentChosenEnum.padding;

        TalentChooseUI.Instance.Show(talentList, (TalentSO talent) =>
        {
            talentList.Remove(talent);
            talentChosen = TalentChosenEnum.done;
        }, () =>
        {
            talentChosen = TalentChosenEnum.cancel;
        });

        yield return null;
    }
}
