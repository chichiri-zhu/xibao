using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TalentChooseItem : MonoBehaviour
{
    public static TalentChooseItem Create(TalentSO talent, Transform parentTransform)
    {
        Transform pfTalentChooseItem = Resources.Load<Transform>("pfTalentChooseItem");
        Transform talentChooseItemTransform = Instantiate(pfTalentChooseItem, parentTransform);
        TalentChooseItem talentChooseItem = talentChooseItemTransform.GetComponent<TalentChooseItem>();
        talentChooseItem.SetTalent(talent);
        return talentChooseItem;
    }

    private TalentSO talent;
    [SerializeField] private MyText title;
    [SerializeField] private Button button;

    public event EventHandler<OnTalentChooseArgs> OnChoose;

    //private void Awake()
    //{
    //    button.onClick.AddListener(() => {
    //        Choose();
    //    });
    //}

    public void SetTalent(TalentSO talent)
    {
        this.talent = talent;
        Debug.Log(talent.nameString);
        title.SetText(talent.nameString);
    }

    public void Choose()
    {
        OnChoose?.Invoke(this, new OnTalentChooseArgs { talent = talent });
    }
}
