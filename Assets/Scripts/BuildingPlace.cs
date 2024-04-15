using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingPlace : MonoBehaviour
{
    [SerializeField] private BuildingTypeSO buildingType;
    [SerializeField] private Transform buildingView;
    [SerializeField] private Transform resourcesTransform;

    private float buildingTimerMax;
    private float buildingTime;
    private Coroutine buildingCoroutine;
    private List<ExpendResource> expendResourceList;
    private int paidGoldAmount = 0;

    private void Awake()
    {
        expendResourceList = new List<ExpendResource>();
        buildingView.gameObject?.SetActive(false);
        InitExpendResource();
    }

    private void Start()
    {
        BuildingManager.Instance.OnBuildEnd += Instance_OnBuildEnd;
        _HideResource();
    }

    private void Instance_OnBuildEnd(object sender, OnBuildEndArgs e)
    {
        if(e.buildingPlace == this)
        {
            Destroy(gameObject);
        }
    }

    private int numResources; // 需要资源数量 
    private float spacing = 1f; // 资源之间的间距
    private float squareSize = 4f; // 正方形的边长

    private void InitExpendResource()
    {
        numResources = buildingType.goldAmount;
        Transform pfExpendResource = Resources.Load<Transform>("pfExpendResource");
        //Transform arrowProjectileTransform = Instantiate(pfExpendResource, resourcesTransform.position, Quaternion.identity);
        if (numResources <= 0)
        {
            return;
        }

        int maxColumns = Mathf.CeilToInt(Mathf.Sqrt(numResources)); // 每行最多的资源数量
        //maxColumns = Mathf.Clamp(maxColumns, 3, 4);
        int numRows = Mathf.CeilToInt((float)numResources / maxColumns); // 总共需要的行数

        float startX = -(maxColumns - 1) * spacing / 2f;
        float startY = (numRows - 1) * spacing / 2f;
        
        int currentRow = 0;
        int currentColumn = 0;

        for (int i = 0; i < numResources; i++)
        {
            float x = startX + currentColumn * spacing;
            float y = startY + currentRow * spacing;

            Vector3 newPosition = new Vector3(x, y, 0f);
            Transform resource = Instantiate(pfExpendResource, resourcesTransform.position + newPosition, Quaternion.identity);
            resource.SetParent(resourcesTransform);
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
    }

    public void StartBuilding()
    {
        buildingTimerMax = buildingType.constructionTimerMax;
        buildingCoroutine = StartCoroutine(_HandleStartBuilding());
    }

    private float timer; //按空格时长
    private float expendGoldTimer = 0.5f;//消耗1金币时长
    private List<ExpendResource> paidResourceList;
    public bool isBuildingFinish;
    private IEnumerator _HandleStartBuilding()
    {
        InitResources();
        foreach (ExpendResource resource in expendResourceList)
        {
            if(ResourceManager.Instance.GetGold() <= 0)
            {
                Debug.Log("cancel");
                CancelBuilding();
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
        isBuildingFinish = true;
        //建造完成
        if(buildingType.talents != null && buildingType.talents.Count > 0)
        {
            TalentChooseUI.Instance.Show(buildingType.talents, (TalentSO talent) => { BuildingManager.Instance.Build(buildingType, this); }, () => { CancelBuilding(); });
        }
        else
        {
            BuildingManager.Instance.Build(buildingType, this);
        }
    }

    private void InitResources()
    {
        foreach (ExpendResource resource in expendResourceList)
        {
            resource.Init();
        }
    }

    public void CancelBuilding()
    {
        isBuildingFinish = false;
        if (buildingCoroutine != null)
        {
            StopCoroutine(buildingCoroutine);
            //StopAllCoroutines();
        }
        buildingCoroutine = null;
        InitResources();
        if(paidGoldAmount > 0)
        {
            ResourceManager.Instance.AddGold(paidGoldAmount);
            paidGoldAmount = 0;
        }
    }

    private void _HideResource()
    {
        resourcesTransform.gameObject.SetActive(false);
    }

    private void _OnPlayerEnter()
    {
        buildingView.gameObject?.SetActive(true);
        resourcesTransform.gameObject.SetActive(true);
        InputManager.Instance.AddBuildingEventHandler();
    }

    private void _onPlayerExit()
    {
        buildingView.gameObject?.SetActive(false);
        _HideResource();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _OnPlayerEnter();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        InputManager.Instance.RemoveSpaceEventHandlers();
        if (collision.gameObject.CompareTag("Player"))
        {
            _onPlayerExit();
        }
    }

    private void OnDestroy()
    {
        BuildingManager.Instance.OnBuildEnd -= Instance_OnBuildEnd;
    }
}
