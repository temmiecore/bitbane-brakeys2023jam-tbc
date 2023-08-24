using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XpDrop : MonoBehaviour
{
    public int xp;

    void FixedUpdate()
    {
        if (Vector2.Distance(transform.position, GameManager.Instance.weaponParent.position) < GameManager.Instance.playerParameters.pickupRange)
        {
            Vector3 direction = GameManager.Instance.weaponParent.position - transform.position;
            transform.position = transform.position + direction.normalized * 6f * Time.deltaTime;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            GameManager.Instance.playerParameters.GetXP(xp);
            Destroy(gameObject);
        }
    }
}
