using System.Collections;
using System.Reflection;
using System.Collections.Generic;
using UnityEngine;

public class Item : ICollectable
{
    [Header("General properties")]
    public ModifiedParameterName modifiedParameterName;
    public float value;

    public List<float> levelAdditions; /// How much value is increased by leveling up the item. First item in List will be 0.

    private void Start()
    {
        ModifyParameter();
    }

    public void ModifyParameter()
    {
        /// Actual sin
        switch (modifiedParameterName)
        {
            case ModifiedParameterName.movementSpeed: { GameManager.Instance.playerParameters.movementSpeed += value; break; }
            case ModifiedParameterName.maxHP: { GameManager.Instance.playerParameters.maxHP += (int)value; break; }
            case ModifiedParameterName.regenTime: { GameManager.Instance.playerParameters.regenTime += value; break; }
            case ModifiedParameterName.additionalDamage: { GameManager.Instance.playerParameters.additionalDamage += value; break; }
            case ModifiedParameterName.additionalProjectileAmmount: { GameManager.Instance.playerParameters.additionalProjectileAmmount += (int)value; break; }
            case ModifiedParameterName.weaponCooldownReduction: { GameManager.Instance.playerParameters.weaponCooldownReduction += value; break; }
            case ModifiedParameterName.luck: { GameManager.Instance.playerParameters.luck += value; break; }
            case ModifiedParameterName.experienceGrowth: { GameManager.Instance.playerParameters.experienceGrowth += value; break; }
            case ModifiedParameterName.pickupRange: { GameManager.Instance.playerParameters.pickupRange += value; break; }
        }
    }
    public void RemoveModifier()
    {
        /// Actual sin
        switch (modifiedParameterName)
        {
            case ModifiedParameterName.movementSpeed: { GameManager.Instance.playerParameters.movementSpeed -= value; break; }
            case ModifiedParameterName.maxHP: { GameManager.Instance.playerParameters.maxHP -= (int)value; break; }
            case ModifiedParameterName.regenTime: { GameManager.Instance.playerParameters.regenTime -= value; break; }
            case ModifiedParameterName.additionalDamage: { GameManager.Instance.playerParameters.additionalDamage -= value; break; }
            case ModifiedParameterName.additionalProjectileAmmount: { GameManager.Instance.playerParameters.additionalProjectileAmmount -= (int)value; break; }
            case ModifiedParameterName.weaponCooldownReduction: { GameManager.Instance.playerParameters.weaponCooldownReduction -= value; break; }
            case ModifiedParameterName.luck: { GameManager.Instance.playerParameters.luck -= value; break; }
            case ModifiedParameterName.experienceGrowth: { GameManager.Instance.playerParameters.experienceGrowth -= value; break; }
            case ModifiedParameterName.pickupRange: { GameManager.Instance.playerParameters.pickupRange -= value; break; }
        }
    }

    public override void LevelUp()
    {
        if (level < maxLevel)
        {
            level++;
            RemoveModifier();
            value += levelAdditions[level];
            ModifyParameter();
        }
    }
}

public enum ModifiedParameterName
{
    movementSpeed,
    maxHP,
    regenTime,
    additionalDamage,
    additionalProjectileAmmount,
    weaponCooldownReduction,
    luck,
    experienceGrowth,
    pickupRange,
}