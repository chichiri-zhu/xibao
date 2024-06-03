using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using MoreMountains.Tools;
using UnityEngine.UI;

public class TowerMergeUI : UIbase, MMEventListener<TowerMergeEvent>
{
    private Tower tower;
    private TowerMergeSoldierUI selectSoldier;
    private Soldier mergeSoldier;
    [SerializeField] private Transform soldierListTransform;
    [SerializeField] private MyText selectContent;
    [SerializeField] private Image mergeIcon;
    [SerializeField] private MyText mergeContent;
    [SerializeField] private Transform button;

    public void Show(Tower tower)
    {
        this.tower = tower;
        Show();
    }

    public override void Show()
    {
        Initialized();
        base.Show();
    }

    private void Initialized()
    {
        selectSoldier = null;
        mergeSoldier = tower.GetMergeSoldier();
        //List<Soldier> soldierList = SoldierManager.Instance.GetSoldierList().Distinct().ToList();
        List<Soldier> soldierList = SoldierManager.Instance.GetSoldierList().GroupBy(obj => obj.GetArms()).Select(obj => obj.First()).ToList(); 
        
        for (var i = 0; i < soldierListTransform.childCount; i++)
        {
            Destroy(soldierListTransform.GetChild(i).gameObject);
        }

        foreach (Soldier soldier in soldierList)
        {
            if(mergeSoldier != null && soldier.GetArms() == mergeSoldier.GetArms())
            {
                continue;
            }
            TowerMergeSoldierUI towerMergeSoldier = TowerMergeSoldierUI.Create(soldier, soldierListTransform);
        }
        RendererSelectInfo();
        RenderInfo();
    }

    public void MergeSoldier()
    {
        if(selectSoldier == null)
        {
            return;
        }

        if(mergeSoldier != null)
        {
            if (selectSoldier.GetSoldier().GetArms() == mergeSoldier.GetArms())
            {
                return;
            }
        }
        //去除component 恢复soldier TODO
        SoldierMergeBase soldierMergeBase = tower.gameObject.GetComponent<SoldierMergeBase>();
        Debug.Log(soldierMergeBase);
        Destroy(soldierMergeBase);
        mergeSoldier = selectSoldier.GetSoldier();
        tower.SetMergeSoldier(mergeSoldier);
        Initialized();
    }

    private void RenderInfo()
    {
        if(mergeSoldier == null)
        {
            mergeContent.SetText("");
            mergeIcon.sprite = null;
        }
        else
        {
            mergeContent.SetText(mergeSoldier.GetArms().nameString);
            mergeIcon.sprite = mergeSoldier.GetArms().icon;
        }
    }

    private void RendererSelectInfo()
    {
        if(selectSoldier == null)
        {
            selectContent.SetText("");
        }
        else
        {
            selectContent.SetText(selectSoldier.GetSoldier().GetArms().nameString);
        }
    }

    public void OnMMEvent(TowerMergeEvent eventType)
    {
        switch (eventType.towerMergeEventType)
        {
            case TowerMergeEventType.Click:
                selectSoldier = eventType.towerMergeSoldier;
                RendererSelectInfo();
                break;
        }
    }

    protected virtual void OnEnable()
    {
        this.MMEventStartListening<TowerMergeEvent>();
    }

    /// <summary>
    /// On Disable, we stop listening for MMInventoryEvents
    /// </summary>
    protected virtual void OnDisable()
    {
        this.MMEventStopListening<TowerMergeEvent>();
    }
}
