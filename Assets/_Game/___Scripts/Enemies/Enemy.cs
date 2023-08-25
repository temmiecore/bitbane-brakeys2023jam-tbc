using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float hp;
    public float damage;
    public float movementSpeed;
    public float xpDropChance;
    public GameObject xpDropPrefab;
    public float weight; /// How much knockback is decreased

    public bool isInfected;

    private Rigidbody2D rb;
    private Vector2 direction;

    private float immunityTime;
    private float immunityCooldown;

    private bool alreadyKnockbacked;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        immunityTime = 0.1f;
    }

    private void Update()
    {
        direction = GameManager.Instance.playerMover.transform.position - transform.position;
        immunityCooldown += Time.deltaTime;

        if (direction.x < 0)
            GetComponent<SpriteRenderer>().flipX = true;
        else
            GetComponent<SpriteRenderer>().flipX = false;
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + direction.normalized * movementSpeed * Time.deltaTime);
    }

    public void RecieveDamage(float damage)
    {
        if (immunityCooldown < immunityTime)
            return;

        hp -= damage;
        GameManager.Instance.InstantiateFloatingText("-" + damage, Color.white, 0.45f, Random.Range(1, 4), transform);

        immunityCooldown = 0f;

        if (hp <= 0)
            Death();
    }

    public void Death()
    {
        if (Random.Range(0, 100) > xpDropChance)
            Instantiate(xpDropPrefab, transform.position, transform.rotation);

        if (GameManager.Instance.objectiveController.level == 1)
            GameManager.Instance.objectiveController.AddToObjectiveCount();

        if (GameManager.Instance.objectiveController.level == 2 && isInfected)
            GameManager.Instance.objectiveController.AddToObjectiveCount();

        Destroy(gameObject);   
    }

    public void Knockback(float weaponKnockbackStrength)
    {
        if (alreadyKnockbacked)
            return;

        StartCoroutine(KnockbackCoroutine(weaponKnockbackStrength));
    }

    private IEnumerator KnockbackCoroutine(float weaponKnockbackStrength)
    {
        alreadyKnockbacked = true;
        float speedBuff = movementSpeed;
        if (weaponKnockbackStrength != 0)
        {
            GetComponent<Rigidbody2D>().mass += 5f;
            movementSpeed = -movementSpeed * weaponKnockbackStrength / weight;
            yield return new WaitForSeconds(0.1f);
            GetComponent<Rigidbody2D>().mass -= 5f;
            movementSpeed = speedBuff;
        }
        alreadyKnockbacked = false;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (Random.Range(0, 100) > GameManager.Instance.playerParameters.dodgeChance)
                GameManager.Instance.playerParameters.RecieveDamage(damage);
        }
    }
}

