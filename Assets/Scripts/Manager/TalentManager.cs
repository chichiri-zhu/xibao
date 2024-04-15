using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TalentManager : SingleBase<TalentManager>
{
    private List<TalentSO> talentList;

    public event EventHandler<OnTalentAddArgs> OnTalentAdd;

    [SerializeField] private TalentSO testTalent;

    protected override void Awake()
    {
        base.Awake();
        talentList = new List<TalentSO>();
    }

    private void Start()
    {
        if(testTalent != null)
        {
            AddTalent(testTalent);
        }
    }

    public void AddTalent(TalentSO talent)
    {
        if (!talentList.Contains(talent))
        {
            talentList.Add(talent);
            Type talentType = Type.GetType(talent.selectedTalent);
            if(talentType != null)
            {
                transform.gameObject.AddComponent(talentType);
                OnTalentAdd?.Invoke(this, new OnTalentAddArgs { talent = talent });
            }
        }
        Debug.Log("add talent：" + talent);
    }
}
