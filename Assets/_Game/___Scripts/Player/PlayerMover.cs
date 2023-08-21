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

    ///[HideInInspector] public Vector2 handToMouseDirection;
    ///[HideInInspector] public float handToMouseRotation;

    [HideInInspector] public Vector2 targetVelocity;

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        targetVelocity = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

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
        rb.MovePosition(rb.position + targetVelocity * GameManager.Instance.playerParameters.movementSpeed * Time.deltaTime);
    }
}
