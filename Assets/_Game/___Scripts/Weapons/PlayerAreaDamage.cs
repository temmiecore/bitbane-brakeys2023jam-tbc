using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAreaDamage : MonoBehaviour
{
    [HideInInspector] public Transform position;
    [HideInInspector] public float damage;
    [HideInInspector] public int projectilePierce;
    [HideInInspector] public int projectileBounce;
    [HideInInspector] public float knockbackStrength;

    private void Update()
    {
        transform.position = position.position;
    }

    public void SetupCircle(IWeapon weapon, Transform position)
    {
        damage = weapon.damage;
        projectileBounce = weapon.projectileBounce;
        projectilePierce = weapon.projectilePierce;
        knockbackStrength = weapon.knockbackStrength;
        this.position = position;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            collision.GetComponent<Enemy>().RecieveDamage(damage);
            collision.GetComponent<Enemy>().Knockback(knockbackStrength);
        }

        if (collision.tag == "Destructable")
        { /*Destroy destructable*/ }
    }
}
