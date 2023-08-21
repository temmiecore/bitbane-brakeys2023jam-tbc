using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Abstract weapon class.
/// </summary>
public abstract class IWeapon : MonoBehaviour
{
    [Header("General properties")]
    public string weaponName;
    public float damage;
    public float cooldownTime;

    [Header("Level")]
    public int level;
    public int maxLevel;
    public List<string> levelDescriptions;

    [Header("Icon sprite")]
    public Sprite icon;

    [Header("Projectile properties")]
    public GameObject projectilePrefab;
    public float projectileSpeed;
    public int projectilePierce;
    public int projectileBounce;
    protected Vector2 direction;

    [HideInInspector] public PlayerMover playerReference;

    private void Start()
    { playerReference = FindObjectOfType<PlayerMover>(); StartCoroutine(AttackCoroutine()); }

    protected IEnumerator AttackCoroutine()
    {
        yield return new WaitForSeconds(cooldownTime);
        Debug.Log("Attack");
        Attack();
        StartCoroutine(AttackCoroutine());
    }

    public abstract void Attack();
    public abstract void LevelUp();

    protected void InstantiateProjectile()
    {
        if (playerReference.targetVelocity == Vector2.zero)
            direction = Vector2.right;
        else
            direction = playerReference.targetVelocity.normalized;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        GameObject projectile = Instantiate(projectilePrefab, playerReference.transform.position,
                                        Quaternion.AngleAxis(angle - 90f, Vector3.forward));

        PlayerProjectile script = projectile.GetComponent<PlayerProjectile>();
        script.SetupProjectile(this);

        Rigidbody2D projectileRb = projectile.GetComponent<Rigidbody2D>();
        projectileRb.AddForce(direction.normalized * projectileSpeed);
    }
}
