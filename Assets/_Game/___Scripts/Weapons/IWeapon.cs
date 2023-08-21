using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Abstract weapon class.
/// </summary>
public abstract class IWeapon : ICollectable
{
    [Header("General properties")]
    public string weaponName;
    public float damage;
    public float cooldownTime;
    public float knockbackStrength;

    [Header("Projectile properties")]
    public GameObject projectilePrefab;
    public float projectileSpeed;
    public int projectilePierce;
    public int projectileBounce;
    protected Vector2 direction;

    private void Start()
    { StartCoroutine(AttackCoroutine()); }

    protected IEnumerator AttackCoroutine()
    {
        yield return new WaitForSeconds(cooldownTime * (1 - GameManager.Instance.playerParameters.weaponCooldownReduction));
        Attack();
        StartCoroutine(AttackCoroutine());
    }

    public abstract void Attack();
    public override void LevelUp() { }

    protected virtual void InstantiateProjectile()
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
