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
    [Range(0f, 5f)]
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
    public int experienceGrowth;

    public int lives;
    public float dodgeChance;

    public float instakill;

    public float pickupRange;

    [Header("Leveling parameters")]
    public int xp; /// Each level either requires +N more XP, or I can make a fancy formula for that
    public int requiredXP;
    public int level;

    private void Start()
    {
        StartCoroutine(RegenCoroutine());
        requiredXP = 5;
        GameManager.Instance.inGameUIController.UpdateIcons();
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
        GameManager.Instance.inGameUIController.CalculateHPBar();

        if (hp <= 0)
            Death();
    }

    private void Regenerate()
    {
        hp += regenAmount; 
        GameManager.Instance.inGameUIController.CalculateHPBar();

        if (hp >= maxHP)
            hp = maxHP;
    }

    public void Death()
    {

    }

    public void GetXP(int xp)
    {
        this.xp += xp + experienceGrowth;

        if (this.xp >= requiredXP)
        {
            LevelUp();
            this.xp -= requiredXP;
            requiredXP += 5;
        }

        GameManager.Instance.inGameUIController.CalculateXPBar();
    }

    public void LevelUp()
    {
        GameManager.Instance.levelupWindowController.OpenWindow();
    }
}