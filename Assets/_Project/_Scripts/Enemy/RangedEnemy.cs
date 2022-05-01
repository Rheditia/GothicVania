using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemy : MonoBehaviour
{
    [Header("Projectile")]
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] float projectileSpeed;
    [SerializeField] float fireDelayDuration = 1f;
    [SerializeField] Vector2 firePosOffset;

    [Header("Checks")]
    [SerializeField] float playerCheckRadius = 0f;
    [SerializeField] Vector2 playerCheckOffset;
    [SerializeField] LayerMask playerLayer;

    Animator anim;

    private float fireDelay;
    private bool isFiring;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        Flip();
        fireDelay -= Time.deltaTime;
        if(CheckPlayerInRange() && (fireDelay <= 0) && !isFiring)
        {
            anim.SetBool("fire", true);
            isFiring = true;
        }
    }

    public void Fire()
    {
        anim.SetBool("fire", false);
        isFiring = false;
        fireDelay = fireDelayDuration;

        Vector3 offset = new Vector3(firePosOffset.x * transform.localScale.x, firePosOffset.y);

        GameObject projectile = Instantiate(projectilePrefab, transform.position + offset, Quaternion.identity);
        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
        Vector2 newVelocity = new Vector2(transform.localScale.x * projectileSpeed, 0f);
        rb.velocity = newVelocity;
    }

    private Collider2D CheckPlayerInRange()
    {
        Vector3 offset = new Vector3(playerCheckOffset.x * transform.localScale.x, playerCheckOffset.y);
        return Physics2D.OverlapCircle(transform.position + offset, playerCheckRadius, playerLayer);
    }

    private void Flip()
    {
        if (!CheckPlayerInRange()) { return; }
        Vector3 target = CheckPlayerInRange().transform.position - transform.position;
        transform.localScale = new Vector3(Mathf.Sign(target.x), 1, 1);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position + new Vector3(playerCheckOffset.x * transform.localScale.x, playerCheckOffset.y), playerCheckRadius);
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(transform.position + new Vector3(firePosOffset.x * transform.localScale.x, firePosOffset.y), 0.5f);
    }
}
