using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLocomotion : MonoBehaviour
{
    Rigidbody2D myRigidbody;

    private void Awake()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
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
}
