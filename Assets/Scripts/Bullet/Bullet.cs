using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BulletType
{
    Position,
    TargetUnit
}

public class Bullet : MonoBehaviour
{
    public static Bullet Create(Vector3 position, UnitBase targetUnit, int damage, UnitBase sourceUnit = null)
    {
        Transform pfBullet = Resources.Load<Transform>("pfBullet");
        Transform bulletTransform = Instantiate(pfBullet, position, Quaternion.identity);

        Bullet bullet = bulletTransform.GetComponent<Bullet>();
        bullet.SetTarget(targetUnit);
        bullet.SetSourceUnit(sourceUnit);
        bullet.SetDamageAmount(damage);
        return bullet;
    }

    public static Bullet Create(Vector3 position, Vector3 targetPosition, int damage, UnitBase sourceUnit = null)
    {
        Transform pfBullet = Resources.Load<Transform>("pfBullet");
        Transform bulletTransform = Instantiate(pfBullet, position, Quaternion.identity);

        Bullet bullet = bulletTransform.GetComponent<Bullet>();
        bullet.SetTargetPosition(targetPosition);
        bullet.SetSourceUnit(sourceUnit);
        bullet.SetDamageAmount(damage);
        return bullet;
    }

    [SerializeField] protected UnitBase targetUnit;
    protected UnitBase sourceUnit;
    protected Vector3 lastMoveDir;
    protected Vector3 targetPosition;
    protected float timeToDie = 5f;
    protected int damageAmount = 1;
    protected BulletType bulletType;

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

        float moveSpeed = 10f;
        transform.position += moveDir * moveSpeed * Time.deltaTime;
        transform.eulerAngles = new Vector3(0, 0, UtilsClass.GetAngleFromVector(moveDir));

        timeToDie -= Time.deltaTime;
        if (timeToDie < 0)
        {
            Destroy(gameObject);
        }
    }

    protected void SetTarget(UnitBase targetUnit)
    {
        if (targetUnit == null)
        {
            return;
        }
        bulletType = BulletType.TargetUnit;
        this.targetUnit = targetUnit;
        Vector3 moveDir = (targetUnit.transform.position - transform.position).normalized;
        transform.eulerAngles = new Vector3(0, 0, UtilsClass.GetAngleFromVector(moveDir));
    }

    protected void SetTargetPosition(Vector3 targetPosition)
    {
        bulletType = BulletType.Position;
        this.targetPosition = targetPosition;
        Vector3 moveDir = (targetUnit.transform.position - transform.position).normalized;
        transform.eulerAngles = new Vector3(0, 0, UtilsClass.GetAngleFromVector(moveDir));
    }

    protected void SetSourceUnit(UnitBase sourceUnit)
    {
        if (sourceUnit != null)
        {
            this.sourceUnit = sourceUnit;
        }
    }

    protected void SetDamageAmount(int damageAmount)
    {
        this.damageAmount = damageAmount;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        UnitBase unit = collision.GetComponent<UnitBase>();

        if (unit == targetUnit && unit != null)
        {

            //if (sourceUnit != null)
            //{
            //    HitBase hit = sourceUnit.GetComponent<HitBase>();
            //    if (hit != null)
            //    {
            //        hit.DoDamage(targetUnit);
            //    }
            //    else
            //    {
            //        unit?.GetComponent<HealthSystem>()?.Damage(damageAmount, sourceUnit);
            //    }
            //}
            //else
            //{
                unit.GetComponent<HealthSystem>().Damage(damageAmount, sourceUnit);
            //}

            Destroy(gameObject);
        }
    }
}
