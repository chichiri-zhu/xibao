using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum TalentChosenEnum
{
    padding,
    done,
    cancel
}

public class UpgradeBase : MonoBehaviour
{
    public int upgradeAmount = 0;
    public TalentChosenEnum talentChosen = TalentChosenEnum.padding;

    public event EventHandler OnUpgradeDone;

    public BuildingBase building;

    public virtual void Start()
    {
        building = transform.GetComponent<BuildingBase>();
        if(building != null)
        {
            InitExpendResource();
        }
    }

    private float spacing = 1f; // 资源之间的间距
    private List<ExpendResource> expendResourceList;
    private void InitExpendResource()
    {
        for (int i = 0; i < building.resourcesTransform.childCount; i++)
        {
            Destroy(building.resourcesTransform.GetChild(i).gameObject);
        }
        expendResourceList = new List<ExpendResource>();
        Transform pfExpendResource = Resources.Load<Transform>("pfExpendResource");
        //Transform arrowProjectileTransform = Instantiate(pfExpendResource, resourcesTransform.position, Quaternion.identity);
        if (upgradeAmount <= 0)
        {
            return;
        }

        int maxColumns = Mathf.CeilToInt(Mathf.Sqrt(upgradeAmount)); // 每行最多的资源数量
        //maxColumns = Mathf.Clamp(maxColumns, 3, 4);
        int numRows = Mathf.CeilToInt((float)upgradeAmount / maxColumns); // 总共需要的行数

        float startX = -(maxColumns - 1) * spacing / 2f;
        float startY = (numRows - 1) * spacing / 2f;

        int currentRow = 0;
        int currentColumn = 0;

        for (int i = 0; i < upgradeAmount; i++)
        {
            float x = startX + currentColumn * spacing;
            float y = startY + currentRow * spacing;

            Vector3 newPosition = new Vector3(x, y, 0f);
            Transform resource = Instantiate(pfExpendResource, building.resourcesTransform.position + newPosition, Quaternion.identity);
            resource.SetParent(building.resourcesTransform);
            ExpendResource expendResource = resource.GetComponent<ExpendResource>();
            expendResourceList.Add(expendResource);
            expendResource.Init();
            currentColumn++;
            if (currentColumn >= maxColumns)
            {
                currentColumn = 0;
                currentRow++;
            }
        }
        HideResource();
    }

    public void HideResource()
    {
        building.resourcesTransform.gameObject.SetActive(false);
    }

    public virtual bool CanUpgrade()
    {
        //TODO
        return true;
    }

    private Coroutine currentCoroutine;
    public void DoUpgrade()
    {
        currentCoroutine = StartCoroutine(HandleUpgrade());
    }

    private float timer; //按空格时长
    private float expendGoldTimer = 0.5f;//消耗1金币时长
    private int paidGoldAmount = 0;
    //private List<ExpendResource> paidResourceList;
    public bool isUpgradePaid;
    public IEnumerator HandleUpgrade()
    {
        isUpgradePaid = false;
        InitResources();
        foreach (ExpendResource resource in expendResourceList)
        {
            if (ResourceManager.Instance.GetGold() <= 0)
            {
                CancelUpgrade();
                yield return null;
            }
            else
            {
                timer = 0f;
                while (timer <= expendGoldTimer)
                {
                    timer += Time.deltaTime;
                    resource.SetMaskScale(timer / expendGoldTimer);
                    yield return null;
                }
                ResourceManager.Instance.SpendGold(1);
                paidGoldAmount++;
            }
        }

        yield return null;
        //升级完成
        isUpgradePaid = true;
        //upgradeIE = UpgradeDone();
        yield return StartCoroutine(UpgradeDone());
        //StartCoroutine(UpgradeDone());
        //bool result = true;// (bool)upgradeIE.Current;
        //Debug.Log("result:" + result);
        while(talentChosen == TalentChosenEnum.padding)
        {
            yield return null;
        }
        if (talentChosen == TalentChosenEnum.done)
        {
            HideResource();
            OnUpgradeDone?.Invoke(this, EventArgs.Empty);
            Destroy(this);
        }
        else
        {
            isUpgradePaid = false;
            CancelUpgrade();
        }
    }

    private IEnumerator upgradeIE;
    public virtual IEnumerator UpgradeDone()
    {
        yield return true;
    }

    private void InitResources()
    {
        foreach (ExpendResource resource in expendResourceList)
        {
            resource.Init();
        }
    }

    public virtual void CancelUpgrade()
    {
        if (isUpgradePaid)
        {
            return;
        }

        if (currentCoroutine != null)
        {
            StopCoroutine(currentCoroutine);
        }

        InitResources();
        if (paidGoldAmount > 0)
        {
            ResourceManager.Instance.AddGold(paidGoldAmount);
            paidGoldAmount = 0;
        }
    }

}
