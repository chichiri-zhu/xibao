using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerMergeSoldierUI : MonoBehaviour
{
    public static TowerMergeSoldierUI Create(Soldier soldier, Transform parentTransform)
    {
        Transform pfTowerMergeSoldier = Resources.Load<Transform>("pfTowerMergeSoldier");
        Transform towerMergeSoldierTransform = Instantiate(pfTowerMergeSoldier, parentTransform);
        TowerMergeSoldierUI towerMergeSoldier = towerMergeSoldierTransform.GetComponent<TowerMergeSoldierUI>();
        towerMergeSoldier.SetSoldier(soldier);
        return towerMergeSoldier;
    }
    private Soldier soldier;
    [SerializeField] private Image icon;
    [SerializeField] public Button button;

    private void Start()
    {
        button.onClick.AddListener(Click);
    }

    public void SetSoldier(Soldier soldier)
    {
        this.soldier = soldier;
        icon.sprite = soldier.GetArms().icon;
    }

    public Soldier GetSoldier()
    {
        return soldier;
    }

    public void Click()
    {
        Debug.Log("Click");
        TowerMergeEvent.Trigger(TowerMergeEventType.Click, this);
    }
}


