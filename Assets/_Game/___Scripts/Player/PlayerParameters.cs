using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

/// <summary>
/// Data storage for all player-related parameters.
/// </summary>
public class PlayerParameters: MonoBehaviour
{
    [Range(0f, 1f)]
    public float movementSpeed;

    [Header("Health component")]
    public float hp;
    public int maxHP;
    public float regenAmount;
    public float regenTime;

    [Header("Stat modifiers")]
    public float additionalDamage; /// Percentage
    public int additionalProjectileAmmount;
    public float weaponCooldownReduction; /// Percentage

    public float luck;
    public float experienceGrowth;

    public float pickupRange;

    [Header("Leveling parameters")]
    public int xp; /// Each level either requires +N more XP, or I can make a fancy formula for that
    public int level;

    private void Start()
    {
        StartCoroutine(RegenCoroutine());
    }

    private IEnumerator RegenCoroutine()
    {
        Regenerate();
        yield return new WaitForSeconds(regenTime);
        StartCoroutine(RegenCoroutine());
    }

    public void RecieveDamage(float damage)
    {
        hp -= damage;

        if (hp <= 0)
            Death();
    }

    private void Regenerate()
    {
        hp += regenAmount;

        if (hp >= maxHP)
            hp = maxHP;
    }

    public void Death()
    {

    }

}