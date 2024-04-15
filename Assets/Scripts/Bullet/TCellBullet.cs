using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TCellBullet : Bullet
{
    public static TCellBullet Create(Vector3 position, UnitBase targetUnit, int damage, UnitBase sourceUnit = null)
    {
        Transform pfBullet = Resources.Load<Transform>("pfTCellBullet");
        Transform bulletTransform = Instantiate(pfBullet, position, Quaternion.identity);

        TCellBullet bullet = bulletTransform.GetComponent<TCellBullet>();
        bullet.SetTarget(targetUnit);
        bullet.SetSourceUnit(sourceUnit);
        bullet.SetDamageAmount(damage);
        return bullet;
    }

    public static TCellBullet Create(Vector3 position, Vector3 targetPosition, int damage, UnitBase sourceUnit = null)
    {
        Transform pfBullet = Resources.Load<Transform>("pfTCellBullet");
        Transform bulletTransform = Instantiate(pfBullet, position, Quaternion.identity);

        TCellBullet bullet = bulletTransform.GetComponent<TCellBullet>();
        bullet.SetTargetPosition(targetPosition);
        bullet.SetSourceUnit(sourceUnit);
        bullet.SetDamageAmount(damage);
        return bullet;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        UnitBase unit = collision.GetComponent<UnitBase>();

        if (unit == targetUnit && unit != null)
        {
            BuffManager.Instance.AddBuff<TCellBuff>(unit.gameObject, sourceUnit.gameObject.name);
            unit.GetComponent<HealthSystem>().Damage(damageAmount, sourceUnit);
            Destroy(gameObject);
        }
    }
}
