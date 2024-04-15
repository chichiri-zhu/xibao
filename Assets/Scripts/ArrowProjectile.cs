using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowProjectile : MonoBehaviour
{
    public static ArrowProjectile Create(Vector3 position, UnitBase targetUnit, int damage, UnitBase sourceUnit = null, string pfString = "pfArrowProjectile")
    {
        Transform pfArrowProjectile = Resources.Load<Transform>(pfString);
        Transform arrowProjectileTransform = Instantiate(pfArrowProjectile, position, Quaternion.identity);

        ArrowProjectile arrowProjectile = arrowProjectileTransform.GetComponent<ArrowProjectile>();
        arrowProjectile.SetTarget(targetUnit);
        arrowProjectile.SetSourceUnit(sourceUnit);
        arrowProjectile.SetDamageAmount(damage);
        return arrowProjectile;
    }

    [SerializeField] private UnitBase targetUnit;
    private UnitBase sourceUnit;
    private Vector3 lastMoveDir;
    private float timeToDie = 2f;
    private int damageAmount = 1;

    private void Update()
    {
        if (targetUnit == null)
        {
            Destroy(gameObject);
        }
        Vector3 moveDir;
        if (targetUnit != null)
        {
            moveDir = (targetUnit.transform.position - transform.position).normalized;
            lastMoveDir = moveDir;
        }
        else
        {
            moveDir = lastMoveDir;
        }

        float moveSpeed = 20f;
        transform.position += moveDir * moveSpeed * Time.deltaTime;
        transform.eulerAngles = new Vector3(0, 0, UtilsClass.GetAngleFromVector(moveDir));

        timeToDie -= Time.deltaTime;
        if (timeToDie < 0)
        {
            Destroy(gameObject);
        }
    }

    private void SetTarget(UnitBase targetUnit)
    {
        if (targetUnit == null)
        {
            return;
        }
        this.targetUnit = targetUnit;
        Vector3 moveDir = (targetUnit.transform.position - transform.position).normalized;
        transform.eulerAngles = new Vector3(0, 0, UtilsClass.GetAngleFromVector(moveDir));
    }

    private void SetSourceUnit(UnitBase sourceUnit)
    {
        if (sourceUnit != null)
        {
            this.sourceUnit = sourceUnit;
        }
    }

    private void SetDamageAmount(int damageAmount)
    {
        this.damageAmount = damageAmount;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //UnitBase unit = UtilsClass.GetUnit(collision.transform);
        UnitBase unit = collision.gameObject.GetComponent<UnitBase>();
        if (unit == targetUnit && unit != null)
        {
            if (sourceUnit != null)
            {
                HitBase hit = sourceUnit.GetComponent<HitBase>();
                if (hit != null)
                {
                    hit.DoDamage(targetUnit);
                }
                else
                {
                    unit?.GetComponent<HealthSystem>()?.Damage(damageAmount, sourceUnit);
                }
            }
            else
            {
                unit.GetComponent<HealthSystem>().Damage(damageAmount, sourceUnit);
            }

            Destroy(gameObject);
        }
    }
}
