using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalArrow : MonoBehaviour
{
    Portal portal;
    SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        portal = FindObjectOfType<Portal>();
    }
    private void Update()
    {
        Vector3 direction = (portal.transform.position - GameManager.Instance.playerMover.transform.position).normalized;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        transform.position = direction * 0.6f + GameManager.Instance.playerMover.transform.position;

        if (Vector2.Distance(portal.transform.position, GameManager.Instance.playerMover.transform.position) < 1.32f)
            spriteRenderer.enabled = false;
        else
            spriteRenderer.enabled = true;
    }
}
