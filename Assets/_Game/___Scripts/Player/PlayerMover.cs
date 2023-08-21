using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script is used for player movement, animation triggering, 
/// sprite rotation and hand object rotation, which are linked to cursor position. 
/// </summary>
[RequireComponent(typeof(PlayerParameters))]
public class PlayerMover : MonoBehaviour
{
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private Animator animator;

    private PlayerParameters playerParams;

    ///[HideInInspector] public Vector2 handToMouseDirection;
    ///[HideInInspector] public float handToMouseRotation;

    [HideInInspector] public Vector2 targetVelocity;

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        playerParams = GetComponent<PlayerParameters>();
    }

    void Update()
    {
        targetVelocity = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        /*handToMouseDirection = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;

        handToMouseRotation = Mathf.Atan2(handToMouseDirection.y, handToMouseDirection.x) * Mathf.Rad2Deg;


        float angle = Mathf.Atan2(handToMouseDirection.y, handToMouseDirection.x) * Mathf.Rad2Deg;
        Quaternion handRotation = Quaternion.AngleAxis(angle, Vector3.forward);*/

        if (targetVelocity.magnitude > 0)
            animator.SetBool("IsWalking", true);
        else
            animator.SetBool("IsWalking", false);

        if (targetVelocity.x < 0)
            spriteRenderer.flipX = true;
        else
            spriteRenderer.flipX = false;
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + targetVelocity * playerParams.movementSpeed * Time.deltaTime);
    }
}
