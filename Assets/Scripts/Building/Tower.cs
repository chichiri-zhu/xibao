using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//箭楼
public class Tower : BuildingBase
{
    [SerializeField] private Transform projectileSpawnPosition;
    private float shootTimer;
    private float shootTimerMax;

    private float targetMaxRadius;
    private float defaultTargetMaxRadius = 1f;

    private int damageAmount = 0;
    private UnitBase targetEnemy;
    private Soldier mergeSoldier;

    public event EventHandler<DoHitArgs> OnHit;

    protected override void OnStart()
    {
        attributeSystem.OnAttributeAmountUpdate += AttributeSystem_OnAttributeAmountUpdate;
        UpdateAttribute();
    }

    private void Update()
    {
        HandleTarget();
        HandleHit();
    }

    private void AttributeSystem_OnAttributeAmountUpdate(object sender, OnAttributeAmountUpdateArgs e)
    {
        UpdateAttribute();
    }

    private void UpdateAttribute()
    {
        AttributeParam attributeParam = attributeSystem.GetAttributeParam();
        damageAmount = (int)attributeParam.Atk;
        shootTimerMax = 1 / attributeParam.AtkSpeed;
        targetMaxRadius = defaultTargetMaxRadius + attributeParam.Rof;
    }

    private float lookForTargetTimer;
    private float lookForTargetTimerMax = .2f;
    private void HandleTarget()
    {
        lookForTargetTimer -= Time.deltaTime;
        if (lookForTargetTimer < 0)
        {
            lookForTargetTimer += lookForTargetTimerMax;
            LookForTargets();
        }
    }

    private void LookForTargets()
    {
        Collider2D[] enemyList = Physics2D.OverlapCircleAll(transform.position, targetMaxRadius);
        //List<Transform> enemyList = BattleManager.Instance.GetEnemyList();
        if (enemyList == null)
        {
            return;
        }


        //foreach (Transform obj in enemyList)
        foreach (Collider2D obj in enemyList)
        {
            if (obj == null)
            {
                continue;
            }

            EnemyUnit enemy = obj.GetComponent<EnemyUnit>();
            if (enemy != null)
            {
                if (targetEnemy == null)
                {
                    targetEnemy = enemy;
                }
                else
                {
                    if (Vector3.Distance(transform.position, enemy.transform.position) <
                        Vector3.Distance(transform.position, targetEnemy.transform.position))
                    {
                        targetEnemy = enemy;
                    }
                }
            }
        }
    }

    private void HandleHit()
    {
        if(targetEnemy == null)
        {
            return;
        }

        shootTimer -= Time.deltaTime;
        if (shootTimer < 0)
        {
            shootTimer = shootTimerMax;
            if (targetEnemy != null)
            {
                Arrow(targetEnemy);
                OnHit?.Invoke(this, new DoHitArgs { targetUnit = targetEnemy });
            }
        }
    }

    public void Arrow(UnitBase unit)
    {
        ArrowProjectile.Create(projectileSpawnPosition.position, unit, damageAmount, GetComponent<UnitBase>());
    }

    public UnitBase GetTargetEnemy()
    {
        return targetEnemy;
    }

    public void SetMergeSoldier(Soldier soldier)
    {
        mergeSoldier = soldier;
        Type mergeType = Type.GetType(soldier.GetArms().soldierMerge);
        if (mergeType != null)
        {
            gameObject.AddComponent(mergeType);
        }
    }

    public float GetHitRof()
    {
        return targetMaxRadius;
    }

    public Soldier GetMergeSoldier()
    {
        return mergeSoldier;
    }
}
