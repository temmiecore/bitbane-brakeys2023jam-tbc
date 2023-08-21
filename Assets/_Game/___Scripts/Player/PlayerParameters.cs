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
    public float regenTime;

    public float immunityTime; 

    [Header("Stat modifiers")]
    public float additionalDamage; /// Percentage
    public int additionalProjectileAmmount;
    public float weaponCooldownReduction; /// Percentage

    public float luck;
    public float experienceGrowth;

    public float pickupRange;
}