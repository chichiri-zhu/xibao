using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private HealthSystem healthSystem;
    [SerializeField] private bool isPermanent;//是否永久显示学条
    [SerializeField] private SpriteRenderer barSprite;

    private Transform barTransform;

    private void Awake()
    {
        barTransform = transform.Find("bar");
        
    }

    private void Start()
    {
        if(healthSystem == null)
        {
            healthSystem = transform.parent.GetComponent<HealthSystem>();
            if(healthSystem == null)
            {
                Destroy(gameObject);
                return;
            }
        }
        healthSystem.OnDamaged += HealthSystem_OnDamaged;
        healthSystem.OnHpRecover += HealthSystem_OnHpRecover;
        if (!isPermanent)
        {
            gameObject.SetActive(false);
        }

        //UnitBase unitBase = GetComponent<UnitBase>();

        //if (UtilsClass.IsEnemy(healthSystem.transform))
        //{
        //    barSprite.color = Color.red;
        //}
        //else
        //{
        //    barSprite.color = new Color32(59, 255, 77, 255);
        //}
        UpdateBar();
    }

    private void HealthSystem_OnDamaged(object sender, OnDamagedArgs e)
    {
        UpdateBar();
    }

    private void HealthSystem_OnHpRecover(object sender, System.EventArgs e)
    {
        UpdateBar();
    }

    private void UpdateBar()
    {
        UpdateHealthBarVisible();
        barTransform.localScale = new Vector3(healthSystem.GetHealthAmountNormalized(), 1, 1);
    }

    private void UpdateHealthBarVisible()
    {
        if (isPermanent)
        {
            gameObject.SetActive(true);
        }
        else
        {
            if (healthSystem.IsFullHealth())
            {
                gameObject.SetActive(false);
            }
            else
            {
                gameObject.SetActive(true);
            }
        }
    }

    private void OnDestroy()
    {
        if(healthSystem != null)
        {
            healthSystem.OnDamaged -= HealthSystem_OnDamaged;
            healthSystem.OnHpRecover -= HealthSystem_OnHpRecover;
        }
    }
}
