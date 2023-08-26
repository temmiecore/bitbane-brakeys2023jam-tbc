using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectile : MonoBehaviour
{
    [HideInInspector] public float damage;
    [HideInInspector] public int projectilePierce;
    [HideInInspector] public int projectileBounce;
    [HideInInspector] public float knockbackStrength;

    public void SetupProjectile(IWeapon weapon)
    {
        damage = weapon.damage;
        knockbackStrength = weapon.knockbackStrength;

        Destroy(gameObject, 20);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        { 
            collision.GetComponent<Enemy>().RecieveDamage(damage + GameManager.Instance.playerParameters.additionalDamage);
            collision.GetComponent<Enemy>().Knockback(knockbackStrength);

            projectilePierce--;
            if (projectilePierce <= 0)
                Destroy(gameObject);
        }

        if (collision.tag == "Destructable")
        { /*Destroy destructable*/ }

        // Bounce off if has bounce
        if (collision.tag == "TerrainCol")
            Destroy(gameObject);
    }
}
