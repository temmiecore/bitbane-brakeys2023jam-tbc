using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon_Dagger : IWeapon
{
    [Header("Projectile properties")]
    public GameObject projectilePrefab;
    public GameObject oneBitProjectilePrefab;
    public float projectileSpeed;
    public int projectilePierce;
    public int projectileBounce;

    private Vector2 direction;
    private GameObject projectile;

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

        if (GameManager.Instance.objectiveController.level == 1)
        {
            projectile = Instantiate(projectilePrefab, transform.parent.position,
                                            Quaternion.AngleAxis(angle - 90f, Vector3.forward));
        }
        else
        {
            projectile = Instantiate(oneBitProjectilePrefab, transform.parent.position,
                                            Quaternion.AngleAxis(angle - 90f, Vector3.forward));
        }

        PlayerProjectile script = projectile.GetComponent<PlayerProjectile>();
        script.SetupProjectile(this);
        script.projectileBounce = projectileBounce; script.projectilePierce = projectilePierce;

        Rigidbody2D projectileRb = projectile.GetComponent<Rigidbody2D>();
        projectileRb.AddForce(direction.normalized * projectileSpeed);
    }
}
