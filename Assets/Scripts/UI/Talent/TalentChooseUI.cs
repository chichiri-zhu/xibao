using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalentChooseUI : UIbase
{
    [SerializeField] Transform itemTransform;
    [SerializeField] List<TalentSO> test;
    private bool isOnlyShow = false;
    private Action<TalentSO> successCB;
    private Action cancelCB;

    public static TalentChooseUI Instance;

    public override void OnAwake()
    {
        Instance = this;
        //Show(test);
    }

    private void Init()
    {
        for (int i = 0; i < itemTransform.childCount; i++)
        {
            Destroy(itemTransform.GetChild(i).gameObject);
        }
    }

    public void Show(List<TalentSO> talentList, Action<TalentSO> successCB = null, Action cancelCB = null, bool isOnlyShow = false)
    {
        Init();
        this.isOnlyShow = isOnlyShow;
        this.successCB = successCB;
        this.cancelCB = cancelCB;
        foreach (TalentSO talent in talentList)
        {
            TalentChooseItem chooseItem = TalentChooseItem.Create(talent, itemTransform);
            chooseItem.OnChoose += ChooseItem_OnChoose;
        }
        Show();
    }

    private void ChooseItem_OnChoose(object sender, OnTalentChooseArgs e)
    {
        if (!isOnlyShow)
        {
            TalentManager.Instance.AddTalent(e.talent);
        }
        successCB?.Invoke(e.talent);
        Hide();
    }

    public override void HandleEsc()
    {
        cancelCB?.Invoke();
        base.HandleEsc();
    }

    public override void Show()
    {
        InputManager.Instance.EscAction = null;
        base.Show();
    }

    public override void Hide()
    {
        base.Hide();
        successCB = null;
        cancelCB = null;
        InputManager.Instance.EscAction = GameManager.Instance.EscHandle;
    }
}
