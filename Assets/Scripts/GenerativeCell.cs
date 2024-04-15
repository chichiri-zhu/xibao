using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class GenerativeCell : MonoBehaviour
{
    [SerializeField] private ArmsSO generativeCellType;
    [SerializeField] private Transform generativeTransform;
    [SerializeField] private Transform createTransform;
    [SerializeField] private int soldierAmount;
    private bool isPrepare = true;

    private List<SoldierBase> soldierList = new List<SoldierBase>();

    private float createTimerMax = 5;
    private float createTimer = 0;

    public event EventHandler<OnSoldierCreateArgs> OnSoldierCreate;

    private void Start()
    {
        if(generativeCellType != null)
        {
            InitCell();
        }
        GameManager.Instance.OnBattleEnd += Instance_OnBattleEnd;
        GameManager.Instance.OnPrepareStart += Instance_OnPrepareStart;
        GameManager.Instance.OnBattleStart += Instance_OnBattleStart;
    }

    private void Update()
    {
        if (!isPrepare)
        {
            HandleCreate();
        }
    }

    private void Instance_OnPrepareStart(object sender, EventArgs e)
    {
        isPrepare = true;
        InitCell();
    }

    private void Instance_OnBattleEnd(object sender, EventArgs e)
    {
        ClearSoldier();
    }

    private void Instance_OnBattleStart(object sender, EventArgs e)
    {
        isPrepare = false;
    }

    public void InitCell(ArmsSO arms, int amount)
    {
        generativeCellType = arms;
        soldierAmount = amount;
        InitCell();
    }

    public void InitCell()
    {
        if(soldierAmount > 0)
        {
            for (int i = 1; i <= soldierAmount; i++)
            {
                Vector2 pos = generativeTransform.GetChild(i - 1).transform.position;
                CreateSoldier(pos);
            }
        }
    }

    private void ClearSoldier()
    {
        foreach (var soldier in soldierList)
        {
            if(soldier != null)
            {
                Destroy(soldier.gameObject);
            }
        }
        soldierList = new List<SoldierBase>();
    }

    private void CreateSoldier(Vector2 pos)
    {
        Transform newSoldier = SoldierManager.Instance.AddSoldier(generativeCellType, pos);
        SoldierBase soldier = newSoldier.GetComponent<SoldierBase>();
        if (soldier != null)
        {
            soldierList.Add(soldier);
            OnSoldierCreate?.Invoke(this, new OnSoldierCreateArgs { soldierBase = soldier });
        }
    }

    private void HandleCreate()
    {
        int soldierListAmount = soldierList.FindAll(obj => obj != null).Count;
        int amountDiff = soldierAmount - soldierListAmount;
        if(amountDiff > 0)
        {
            Debug.Log(createTimer);
            if(createTimer > createTimerMax)
            {
                createTimer = 0;
                CreateSoldier(createTransform.position);
            }
            else
            {
                createTimer += Time.deltaTime;
            }
        }
    }

    public void ResetAmount(int amount)
    {
        if(amount <= soldierAmount)
        {
            return;
        }

        for (int i = 1; i <= amount - soldierAmount; i++)
        {
            Vector2 pos = generativeTransform.GetChild(soldierAmount + i - 1).transform.position;
            CreateSoldier(pos);
        }

        soldierAmount = amount;
    }

    private void OnDestroy()
    {
        GameManager.Instance.OnBattleEnd -= Instance_OnBattleEnd;
        GameManager.Instance.OnPrepareStart -= Instance_OnPrepareStart;
        GameManager.Instance.OnBattleStart -= Instance_OnBattleStart;
    }
}
