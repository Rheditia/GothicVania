using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLocomotion : MonoBehaviour
{
    Rigidbody2D myRigidbody;

    PlayerInputHandler inputHandler;
    [SerializeField] float groundCheckRadius = 0f;
    [SerializeField] Vector3 groundCheckOffset;
    [SerializeField] LayerMask platformLayer;
    [SerializeField] float jumpSpeed = 0f;

    private void Awake()
    {
        myRigidbody = GetComponent<Rigidbody2D>();

        inputHandler = GetComponent<PlayerInputHandler>();
    }

    private void FixedUpdate()
    {
        if (inputHandler.JumpInput) { Jump(); }
    }

    public void SetHorizontalVelocity(float velocity, float horizontalInput)
    {
        Vector2 newVelocity = new Vector2(velocity * Mathf.Round(horizontalInput), myRigidbody.velocity.y);
        myRigidbody.velocity = newVelocity;
        Flip(horizontalInput);
    }

    private void Flip(float input)
    {
        if (Mathf.Abs(input) <= Mathf.Epsilon) { return; }
        transform.localScale = new Vector3(Mathf.Sign(myRigidbody.velocity.x), 1, 1);
    }

    private void Jump()
    {
        if (!CheckIfGrounded()) { return; }
        Vector2 newVelocity = new Vector2(myRigidbody.velocity.x, jumpSpeed);
        myRigidbody.velocity = newVelocity;
    }

    private bool CheckIfGrounded()
    {
        return Physics2D.OverlapCircle(transform.position + groundCheckOffset, groundCheckRadius, platformLayer);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position + groundCheckOffset, groundCheckRadius);
    }
}
