using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon_LunarRocket : IWeapon
{
    [Header("Projectile properties")]
    public GameObject projectilePrefab;
    public GameObject oneBitProjectilePrefab;
    public float projectileSpeed;

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
        Collider2D[] colliders = Physics2D.OverlapCircleAll(GameManager.Instance.weaponParent.position, 3f, LayerMask.GetMask("Enemy"));

        if (colliders.Length != 0)
            direction = colliders[Random.Range(0, colliders.Length)].transform.position - GameManager.Instance.weaponParent.position;

        else
            direction = Vector2.up;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        if (GameManager.Instance.objectiveController.level == 1)
        {

            projectile = Instantiate(projectilePrefab, transform.parent.position,
                                            Quaternion.AngleAxis(angle, Vector3.forward));
        }
        else
        {
            projectile = Instantiate(oneBitProjectilePrefab, transform.parent.position,
                                            Quaternion.AngleAxis(angle - 90f, Vector3.forward));
        }

        ExplosionProjectile script = projectile.GetComponent<ExplosionProjectile>();
        script.SetupProjectile(this);

        Rigidbody2D projectileRb = projectile.GetComponent<Rigidbody2D>();
        projectileRb.AddForce(direction.normalized * projectileSpeed);
    }
}
