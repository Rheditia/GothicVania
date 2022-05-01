using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLocomotion : MonoBehaviour
{
    Rigidbody2D myRigidbody;

    [Header("Locomotion")]
    [SerializeField] float moveSpeed = 0f;

    [Header("Checks")]
    [SerializeField] Vector2 wallCheckOffset;
    [SerializeField] float wallCheckLenght = 0f;
    [SerializeField] Vector2 groundCheckOffset;
    [SerializeField] float groundCheckLenght = 0f;
    [SerializeField] LayerMask platformLayer;

    private void Awake()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (!CheckForGround() || CheckForWall())
        {
            transform.localScale = new Vector3(-transform.localScale.x, 1, 1);
        }
    }

    private void FixedUpdate()
    {
        Patrol();
    }

    private void Patrol()
    {
        Vector2 newVelocity = new Vector2(moveSpeed * transform.localScale.x, myRigidbody.velocity.y);
        myRigidbody.velocity = newVelocity;
    }

    private bool CheckForGround()
    {
        Vector3 offset = new Vector3(groundCheckOffset.x * transform.localScale.x, groundCheckOffset.y);
        return Physics2D.Raycast(transform.position + offset, Vector2.down, groundCheckLenght, platformLayer);
    }

    private bool CheckForWall()
    {
        Vector3 offset = new Vector3(wallCheckOffset.x * transform.localScale.x, wallCheckOffset.y);
        return Physics2D.Raycast(transform.position + offset, Vector2.right * transform.localScale.x, wallCheckLenght, platformLayer);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position + new Vector3(groundCheckOffset.x * transform.localScale.x, groundCheckOffset.y),
                        transform.position + new Vector3(groundCheckOffset.x * transform.localScale.x, groundCheckOffset.y) + (Vector3)(Vector2.down * groundCheckLenght));
        Gizmos.color = Color.white;
        Gizmos.DrawLine(transform.position + new Vector3(wallCheckOffset.x * transform.localScale.x, wallCheckOffset.y),
                        transform.position + new Vector3(wallCheckOffset.x * transform.localScale.x, wallCheckOffset.y) + (Vector3)(Vector2.right * transform.localScale.x * wallCheckLenght));
    }
}
