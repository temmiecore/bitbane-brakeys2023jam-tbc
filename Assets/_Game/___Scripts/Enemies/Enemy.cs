using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float hp;
    public float damage;
    public float movementSpeed;
    public float xpDropChange;
    public GameObject xpDropPrefab;
    public float weight; /// How much knockback is decreased

    private Rigidbody2D rb;
    private Vector2 direction;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        direction = GameManager.Instance.playerMover.transform.position - transform.position;
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + direction.normalized * movementSpeed * Time.deltaTime);
    }

    public void RecieveDamage(float damage)
    {
        hp -= damage;
        GameManager.Instance.InstantiateFloatingText("-" + damage, Color.white, 0.45f, Random.Range(1, 4), transform);

        if (hp <= 0)
            Death();
    }

    public void Death()
    {
        Destroy(gameObject);   
    }

    public void Knockback(float weaponKnockbackStrength)
    {
        StartCoroutine(KnockbackCoroutine(weaponKnockbackStrength));
    }

    private IEnumerator KnockbackCoroutine(float weaponKnockbackStrength)
    {
        float speedBuff = movementSpeed;
        movementSpeed = -movementSpeed * weaponKnockbackStrength / weight;
        yield return new WaitForSeconds(0.1f);
        movementSpeed = speedBuff;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
            GameManager.Instance.playerParameters.RecieveDamage(damage);
    }
}

