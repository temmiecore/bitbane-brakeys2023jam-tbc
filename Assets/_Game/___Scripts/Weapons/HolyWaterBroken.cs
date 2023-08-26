using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HolyWaterBroken : MonoBehaviour
{
    private float damage;
    private float knockbackStrength;

    public void SetUpArea(HolyWater water)
    {
        damage = water.damage;
        knockbackStrength = water.knockbackStrength;
        transform.localScale = new Vector3(water.radius, water.radius, 1);

        Destroy(gameObject, water.duration);
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
