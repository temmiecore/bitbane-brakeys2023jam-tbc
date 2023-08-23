using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Abstract weapon class.
/// </summary>
public abstract class IWeapon : ICollectable
{
    [Header("General properties")]
    public float damage;
    public float cooldownTime;
    public float knockbackStrength;

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
}
