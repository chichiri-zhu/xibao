using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingBase : MonoBehaviour
{
    [SerializeField] protected BuildingTypeSO buildingType;
    public Transform resourcesTransform;
    protected AttributeSystem attributeSystem;
    protected HealthSystem healthSystem;
    [SerializeField] protected BuildingStatus buildingStatus = BuildingStatus.Default;
    private Player player;

    private void Awake()
    {
        buildingStatus = BuildingStatus.Default;
        if (buildingType != null)
        {
            attributeSystem = GetComponent<AttributeSystem>();
            if(attributeSystem == null)
            {
                attributeSystem = AttributeSystem.Create(this);
            }
            healthSystem = gameObject.AddComponent<HealthSystem>();
            //healthSystem.Initialize(buildingType.buildingParam.Hp, true);
        }
        OnAwake();
    }

    protected virtual void OnAwake()
    {

    }

    protected virtual void Start()
    {
        player = GameManager.Instance.GetPlayer();
        if (healthSystem != null)
        {
            healthSystem.OnDied += HealthSystem_OnDied;
        }
        GameManager.Instance.OnPrepareStart += Instance_OnPrepareStart;
        OnStart();
    }

    private void Instance_OnPrepareStart(object sender, System.EventArgs e)
    {
        //buildingStatus = BuildingStatus.Default;
        Revive();
    }

    protected virtual void OnStart()
    {

    }

    private void HealthSystem_OnDied(object sender, System.EventArgs e)
    {
        Died();
    }

    private void _OnPlayerEnter()
    {
        UpgradeBase upgrade = GetComponent<UpgradeBase>();
        if(upgrade != null && upgrade.CanUpgrade())
        {
            resourcesTransform.gameObject.SetActive(true);
            InputManager.Instance.AddUpgradeEventHandler();
        }
    }

    private void _OnPlayerExit()
    {
        UpgradeBase upgrade = GetComponent<UpgradeBase>();
        if (upgrade != null)
        {
            upgrade.HideResource();
        }
    }

    private bool isEnter = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && GameManager.Instance.GetGameStatus() == GameStatus.Prepare && player.actionCoroutine == null)
        {
            isEnter = true;
            _OnPlayerEnter();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!isEnter)
        {
            return;
        }

        isEnter = false;
        InputManager.Instance.AddToBattleEventHandler();
        if (collision.gameObject.CompareTag("Player") && GameManager.Instance.GetGameStatus() == GameStatus.Prepare)
        {
            _OnPlayerExit();
        }
    }

    public BuildingTypeSO GetBuildingType()
    {
        return buildingType;
    }

    public BuildingStatus GetBuildingStatus()
    {
        return buildingStatus;
    }

    public virtual void Died()
    {
        buildingStatus = BuildingStatus.Destroy;
        gameObject.SetActive(false);
        //Destroy(transform.gameObject);
        Instantiate(AssetManager.Instance.ruinsPF, transform.position, Quaternion.identity);
    }

    public virtual void Revive()
    {
        buildingStatus = BuildingStatus.Default;
        gameObject.SetActive(true);
        healthSystem.Revive();
    }

    protected virtual void OnDestroy()
    {
        if(healthSystem != null)
        {
            healthSystem.OnDied -= HealthSystem_OnDied;
        }
        GameManager.Instance.OnPrepareStart -= Instance_OnPrepareStart;
    }
}
