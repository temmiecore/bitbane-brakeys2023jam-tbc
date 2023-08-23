using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon_AcraneBolt : IWeapon
{
    [Header("Projectile properties")]
    public GameObject projectilePrefab;
    public int projectileAmount;
    public float projectileSpeed;
    public int projectilePierce;
    public int projectileBounce;

    private Vector2 direction;
    private Transform nearestEnemy;

    public override void Attack()
    {
        StartCoroutine(InstantiateProjectile());
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

    private IEnumerator InstantiateProjectile()
    {
        for (int i = 0; i < projectileAmount; i++)
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(GameManager.Instance.weaponParent.position, 2f, LayerMask.GetMask("Enemy"));

            if (colliders.Length != 0)
            {
                float minDistance = int.MaxValue;
                nearestEnemy = null;

                foreach (Collider2D collider in colliders)
                {
                    if (Vector2.Distance(collider.transform.position, transform.position) < minDistance)
                    {
                        minDistance = Vector2.Distance(collider.transform.position, transform.position);
                        nearestEnemy = collider.transform;
                    }
                }

                direction = (nearestEnemy.position - GameManager.Instance.weaponParent.position);
            }
            else
            {
                direction = Vector2.up;
            }

            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            GameObject projectile = Instantiate(projectilePrefab, transform.parent.position,
                                            Quaternion.AngleAxis(angle - 90f, Vector3.forward));

            PlayerProjectile script = projectile.GetComponent<PlayerProjectile>();
            script.SetupProjectile(this);
            script.projectileBounce = projectileBounce; script.projectilePierce = projectilePierce;

            Rigidbody2D projectileRb = projectile.GetComponent<Rigidbody2D>();
            projectileRb.AddForce(direction.normalized * projectileSpeed);

            yield return new WaitForSeconds(0.1f);
        }
    }
}
