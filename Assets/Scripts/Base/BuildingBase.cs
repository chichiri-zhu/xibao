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
        if(healthSystem != null)
        {
            healthSystem.OnDied += HealthSystem_OnDied;
        }
        GameManager.Instance.OnPrepareStart += Instance_OnPrepareStart;
        OnStart();
    }

    private void Instance_OnPrepareStart(object sender, System.EventArgs e)
    {
        buildingStatus = BuildingStatus.Default;
    }

    protected virtual void OnStart()
    {

    }

    private void HealthSystem_OnDied(object sender, System.EventArgs e)
    {
        buildingStatus = BuildingStatus.Destroy;
        //Destroy(transform.gameObject);
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
            InputManager.Instance.RemoveSpaceEventHandlers();
        }
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
        if (collision.gameObject.CompareTag("Player"))
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

    protected virtual void OnDestroy()
    {
        if(healthSystem != null)
        {
            healthSystem.OnDied -= HealthSystem_OnDied;
        }
        GameManager.Instance.OnPrepareStart -= Instance_OnPrepareStart;
    }
}
