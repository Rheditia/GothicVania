using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLocomotion : MonoBehaviour
{
    PlayerInputHandler inputHandler;
    Rigidbody2D myRigidbody;

    [SerializeField] float moveSpeed = 0f;

    private void Awake()
    {
        inputHandler = GetComponent<PlayerInputHandler>();
        myRigidbody = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        Run();
    }

    private void Run()
    {
        Vector2 newVelocity = new Vector2(moveSpeed * Mathf.Round(inputHandler.MoveInput.x), myRigidbody.velocity.y);
        myRigidbody.velocity = newVelocity;
        Flip();
    }

    private void Flip()
    {
        if (Mathf.Abs(inputHandler.MoveInput.x) <= Mathf.Epsilon) { return; }
        transform.localScale = new Vector3(Mathf.Sign(myRigidbody.velocity.x), 1, 1);
    }
}
