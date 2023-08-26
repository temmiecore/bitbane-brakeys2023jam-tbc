using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAreaDamage : MonoBehaviour
{
    private Transform position;
    private float damage;
    private float knockbackStrength;

    private void Update()
    {
        transform.position = position.position;
    }

    public void SetupCircle(IWeapon weapon, Transform position)
    {
        damage = weapon.damage;
        knockbackStrength = weapon.knockbackStrength;
        this.position = position;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            collision.GetComponent<Enemy>().RecieveDamage(damage + GameManager.Instance.playerParameters.additionalDamage);
            collision.GetComponent<Enemy>().Knockback(knockbackStrength);
        }

        if (collision.tag == "Destructable")
        { /*Destroy destructable*/ }
    }
}
