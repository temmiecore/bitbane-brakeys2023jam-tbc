using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShield : MonoBehaviour
{
    [HideInInspector] public Transform circlePosition;
    [HideInInspector] public float speed;
    [HideInInspector] public float damage;
    [HideInInspector] public int projectilePierce;
    [HideInInspector] public int projectileBounce;
    [HideInInspector] public float knockbackStrength;

    private Vector3 positionOffset;
    private float angle;

    private void Start()
    {
        circlePosition = GameManager.Instance.weaponParent;
    }

    private void Update()
    {
        positionOffset.Set(Mathf.Cos(angle) * 0.5f, Mathf.Sin(angle) * 0.5f, 0);

        transform.position = circlePosition.position + positionOffset;
        angle += Time.deltaTime * speed;
    }

    public void SetupShield(IWeapon weapon)
    {
        damage = weapon.damage;
        knockbackStrength = weapon.knockbackStrength;
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
