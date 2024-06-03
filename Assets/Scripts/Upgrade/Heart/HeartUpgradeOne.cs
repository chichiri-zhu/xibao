using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartUpgradeOne : UpgradeBase
{
    private void Awake()
    {
        upgradeAmount = 4;
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
        //HeartBuilding heartBuilding = building as HeartBuilding;
        //List<TalentSO> upgradeTalentList = heartBuilding.upgradeTalentList;
        List<TalentSO> upgradeTalentList = ConfigManager.Instance.gameConfig.heartUpgradeTalentLevel1;
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
            //talentList.Remove(talent);
            talentChosen = TalentChosenEnum.done;
        }, () =>
        {
            talentChosen = TalentChosenEnum.cancel;
        });

        yield return null;
    }
}
