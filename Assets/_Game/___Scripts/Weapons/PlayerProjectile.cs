using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectile : MonoBehaviour
{
    [HideInInspector] public float damage;
    [HideInInspector] public int projectilePierce;
    [HideInInspector] public int projectileBounce;

    public void SetupProjectile(IWeapon weapon)
    {
        damage = weapon.damage;
        projectileBounce = weapon.projectileBounce;
        projectilePierce = weapon.projectilePierce;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy" || collision.tag == "Destructable")
        { /*Deal damage to an enemy*/  }

        else if (collision.tag == "TerrainCol")
            Destroy(gameObject);
    }
}
