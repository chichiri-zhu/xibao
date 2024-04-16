using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartUpgradeOne : UpgradeBase
{
    private void Awake()
    {
        upgradeAmount = 7;
    }

    //public override void HandleUpgrade()
    //{
    //    Debug.Log("heart upgrade one");
    //}

    //public override IEnumerator UpgradeDone()
    //{
    //    HeartBuilding heartBuilding = building as HeartBuilding;
    //    List<TalentSO> upgradeTalentList = heartBuilding.upgradeTalentList;
    //    yield return null;
    //    if (upgradeTalentList != null && upgradeTalentList.Count > 0)
    //    {
    //        TalentChooseUI.Instance.Show(upgradeTalentList, (TalentSO talent) => {
    //            upgradeTalentList.Remove(talent);
    //            yield return true;
    //        }, () => {
    //            Debug.Log("canel");
    //            yield return false;
    //        });
    //    }
    //    //yield return true;
    //}

    
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
        //talentChosen == 1 ? true : false;
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
        //Debug.Log(talentChosen);
        //while (talentChosen == 0)
        //{
        //    yield return null;
        //}
        //Debug.Log(talentChosen == 1);
        //yield return talentChosen == 1 ? true : false;
    }
}
