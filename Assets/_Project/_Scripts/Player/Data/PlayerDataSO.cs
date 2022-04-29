using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newPlayerData", menuName = "Data/Player Data/Base Data")]
public class PlayerDataSO : ScriptableObject
{
    [Header("Locomotion")]
    [SerializeField] float moveSpeed = 0f;
    [SerializeField] float jumpSpeed = 0f;
    [SerializeField] int jumpAmount = 1;
    [SerializeField] float coyoteTimeDuration = 0.2f;
    [SerializeField] float wallSlideYVelocity = 0f;
    [SerializeField] float wallJumpXVelocity = 0f;
    [SerializeField] float wallJumpDelayDuration = 0f;
    [SerializeField] float dashXVelocity = 0f;
    [SerializeField] float dashDuration = 0.5f;
    [SerializeField] int dashAmount = 1;
    [SerializeField] float delayBetweenDash = 0.5f;

    [Header("Checks")]
    [SerializeField] float groundCheckRadius = 0f;
    [SerializeField] Vector3 groundCheckOffset;
    [SerializeField] LayerMask platformLayer;
    [SerializeField] float wallCheckRadius = 0f;
    [SerializeField] Vector3 wallCheckOffset;

    public float MoveSpeed => moveSpeed;
    public float JumpSpeed => jumpSpeed;
    public int JumpAmount => jumpAmount;
    public float CoyoteTimeDuration => coyoteTimeDuration;
    public float WallSlideYVelocity => wallSlideYVelocity;
    public float WallJumpXVelocity => wallJumpXVelocity;
    public float WallJumpDelayDuration => wallJumpDelayDuration;
    public float DashXVelocity => dashXVelocity;
    public float DashDuration => dashDuration;
    public int DashAmount => dashAmount;
    public float DelayBetweenDash => delayBetweenDash;


    public float GroundCheckRadius => groundCheckRadius;
    public Vector3 GroundCheckOffset => groundCheckOffset;
    public LayerMask PlatformLayer => platformLayer;
    public float WallCheckRadius => wallCheckRadius;
    public Vector3 WallCheckOffset => wallCheckOffset;
}
