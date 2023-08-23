using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon_HolyWater : IWeapon
{
    [Header("Projectile properties")]
    public GameObject projectilePrefab;
    public float areaRadius;
    public float areaDuration;

    private Vector2 direction;

    public override void Attack()
    {
        InstantiateProjectile();
    }

    public override void LevelUp()
    {
        if (level < maxLevel)
        {
            areaDuration += 1f;
            areaRadius += 0.05f;
            level++;
        }
    }

    private void InstantiateProjectile()
    {
        direction = Random.insideUnitCircle.normalized * Random.Range(0.5f, 2f);

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        GameObject projectile = Instantiate(projectilePrefab, transform.parent.position,
                                        Quaternion.AngleAxis(angle - 90f, Vector3.forward));

        HolyWater script = projectile.GetComponent<HolyWater>();
        script.SetupProjectile(this);

        script.BreakBottle();

        Rigidbody2D projectileRb = projectile.GetComponent<Rigidbody2D>();
        projectileRb.AddForce(direction.normalized * 35f);
    }
}
