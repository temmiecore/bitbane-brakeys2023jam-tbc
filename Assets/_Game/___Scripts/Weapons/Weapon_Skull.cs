using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon_Skull : IWeapon
{
    public float circleRadius;
    public bool isSpawned;
    public PlayerAreaDamage circlePrefab;

    public PlayerAreaDamage oneBitCirclePrefab;

    public override void Attack()
    {
        circlePrefab.transform.localScale = new Vector2(circleRadius, circleRadius);

        if (isSpawned)
            return;

        if (GameManager.Instance.objectiveController.level == 1)
        {

            circlePrefab = Instantiate(circlePrefab, transform.parent.position,
                                            Quaternion.identity);
            circlePrefab.SetupCircle(this, GameManager.Instance.weaponParent);
        }
        else
        {
            oneBitCirclePrefab = Instantiate(oneBitCirclePrefab, transform.parent.position,
                                            Quaternion.identity);
            oneBitCirclePrefab.SetupCircle(this, GameManager.Instance.weaponParent);
        }

        isSpawned = true;
    }

    public override void LevelUp()
    {
        if (level < maxLevel)
        {
            knockbackStrength += 0.5f;
            damage += 1f;
            circleRadius += 0.05f;
            level++;
        }
    }
}
