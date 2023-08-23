using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon_Shield : IWeapon
{
    public float shieldSpeed;
    public bool isSpawned;

    public PlayerShield shieldPrefab;

    public override void Attack()
    {
        shieldPrefab.speed = shieldSpeed;

        if (isSpawned)
            return;

        shieldPrefab = Instantiate(shieldPrefab, transform.parent.position,
                                        Quaternion.identity);
        shieldPrefab.SetupShield(this);

        isSpawned = true;
    }

    public override void LevelUp()
    {
        if (level < maxLevel)
        {
            shieldSpeed += 0.1f;
            damage += 2f;
            level++;
        }
    }
}
