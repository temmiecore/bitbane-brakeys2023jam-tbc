using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon_Dagger : IWeapon
{
    public override void Attack()
    {
        InstantiateProjectile();
    }

    public override void LevelUp()
    {
        if (level < maxLevel)
        {
            cooldownTime -= 0.1f;
            level++;
        }
    }
}
