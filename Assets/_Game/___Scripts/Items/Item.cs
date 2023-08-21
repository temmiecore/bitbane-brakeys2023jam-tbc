using System.Collections;
using System.Reflection;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [Header("General properties")]
    public string itemName;
    public ModifiedParameterName modifiedParameterName;
    public float value;

    [Header("Level")]
    public int level;
    public int maxLevel;
    public List<string> levelDescriptions; /// First item in List will be description of the item.
    public List<float> levelAdditions; /// How much value is increased by leveling up the item. First item in List will be 0.

    [Header("Icon sprite")]
    public Sprite icon;

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

    public void LevelUp()
    {
        if (level < maxLevel)
        {
            level++;
            value += levelAdditions[level];
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