using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon_DaedBow : IWeapon
{
    [Header("Projectile properties")]
    public GameObject projectilePrefab;
    /// Doesn't have a 1bit prefab
    public int projectileAmount;
    public float projectileSpeed;
    public int projectilePierce;
    public int projectileBounce;

    private Vector2 direction;
    private Transform nearestEnemy;

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
            projectileAmount += 1;
            level++;
        }
    }

    private void InstantiateProjectile()
    {
        direction = GameManager.Instance.playerMover.handToMouseDirection;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        GameObject projectile = Instantiate(projectilePrefab, transform.parent.position,
                                        Quaternion.AngleAxis(angle - 90f, Vector3.forward));

        PlayerProjectile script = projectile.GetComponent<PlayerProjectile>();
        script.SetupProjectile(this);
        script.projectileBounce = projectileBounce; script.projectilePierce = projectilePierce;

        Rigidbody2D projectileRb = projectile.GetComponent<Rigidbody2D>();
        projectileRb.AddForce(direction.normalized * projectileSpeed);
    }
}
