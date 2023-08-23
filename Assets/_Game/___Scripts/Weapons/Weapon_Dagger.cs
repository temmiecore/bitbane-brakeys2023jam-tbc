using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon_Dagger : IWeapon
{
    public override void Attack()
    {
        InstantiateProjectile();
    }

    public override void LevelUp()
    {
        if (level < maxLevel)
        {
            damage += 1f;
            cooldownTime -= 0.1f;
            level++;
        }
    }

    protected void InstantiateProjectile()
    {
        if (GameManager.Instance.playerMover.targetVelocity == Vector2.zero)
            direction = Vector2.right;
        else
            direction = GameManager.Instance.playerMover.targetVelocity.normalized;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        GameObject projectile = Instantiate(projectilePrefab, transform.parent.position,
                                        Quaternion.AngleAxis(angle - 90f, Vector3.forward));

        PlayerProjectile script = projectile.GetComponent<PlayerProjectile>();
        script.SetupProjectile(this);

        Rigidbody2D projectileRb = projectile.GetComponent<Rigidbody2D>();
        projectileRb.AddForce(direction.normalized * projectileSpeed);
    }
}
