using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    #region StateMachine
    public PlayerStateMachine StateMachine { get; private set; }
    // Grounded
    public PlayerIdleState IdleState { get; private set; }
    public PlayerMoveState MoveState { get; private set; }
    // Ability
    public PlayerJumpState JumpState { get; private set; }
    public PlayerWallSlideState WallSlideState { get; private set; }
    public PlayerDashState DashState { get; private set; }
    // Miscellaneous
    public PlayerInAirState InAirState { get; private set; }
    #endregion

    #region Component
    [SerializeField] PlayerDataSO playerData;
    public Animator Animator { get; private set; }
    public PlayerInputHandler InputHandler { get; private set; }
    public PlayerLocomotion Locomotion { get; private set; }
    #endregion

    #region Variables
    private int jumpCount;
    public bool isFirstJump;

    private int dashCount;
    private float dashTimeTimer;
    public bool DashTime => dashTimeTimer > 0f;
    private float dashDelayTimer;
    public bool DashDelay => dashDelayTimer > 0f;

    private float coyoteTimeTimer;
    public bool CoyoteTime => coyoteTimeTimer > 0f;
    
    private float wallJumpDelayTimer;
    public bool WallJumpDelay => wallJumpDelayTimer > 0f;
    #endregion

    #region UnityCallbacks
    private void Awake()
    {
        StateMachine = new PlayerStateMachine();
        Animator = GetComponent<Animator>();
        InputHandler = GetComponent<PlayerInputHandler>();
        Locomotion = GetComponent<PlayerLocomotion>();

        IdleState = new PlayerIdleState(this, StateMachine, playerData, "idle");
        MoveState = new PlayerMoveState(this, StateMachine, playerData, "run");
        JumpState = new PlayerJumpState(this, StateMachine, playerData, "inAir");
        WallSlideState = new PlayerWallSlideState(this, StateMachine, playerData, "wall");
        DashState = new PlayerDashState(this, StateMachine, playerData, "dash");
        InAirState = new PlayerInAirState(this, StateMachine, playerData, "inAir");
    }

    private void Start()
    {
        StateMachine.InitializeState(IdleState);
    }

    private void Update()
    {
        StateMachine.CurrentState.LogicUpdate();
    }

    private void FixedUpdate()
    {
        StateMachine.CurrentState.PhysicsUpdate();
    }
    #endregion

    #region Condition Checks
    public bool CheckIfGrounded()
    {
        return Physics2D.OverlapCircle(transform.position + playerData.GroundCheckOffset, playerData.GroundCheckRadius, playerData.PlatformLayer);
    }

    public bool CheckJumpCounter() => jumpCount > 0;
    public bool CheckDashCounter() => dashCount > 0;


    public bool CheckIfTouchingWall()
    {
        return Physics2D.OverlapCircle(transform.position + (playerData.WallCheckOffset * transform.localScale.x), playerData.WallCheckRadius, playerData.PlatformLayer);
    }
    #endregion

    #region Others
    // Jump Count
    public void ResetJumpCounter() => jumpCount = playerData.JumpAmount;
    public void DecreaseJumpCounter() => jumpCount--;

    // Dash
    public void ResetDashCounter() => dashCount = playerData.DashAmount;
    public void DecreaseDashCounter() => dashCount--;
    public void ResetDashTime() => dashTimeTimer = playerData.DashDuration;
    public void ClearDashTime() => dashTimeTimer = 0;
    public void DashDurationCountdown()
    {
        if(dashTimeTimer > 0) { dashTimeTimer -= Time.deltaTime; }
    }
    public void ResetDashDelay() => dashDelayTimer = playerData.DelayBetweenDash;
    public void ClearDashDelay() => dashDelayTimer = 0;
    public void DashDelayCountdown()
    {
        if (dashDelayTimer > 0) { dashDelayTimer -= Time.deltaTime; }
    }

    // Coyote Time
    public void ResetCoyoteTime() => coyoteTimeTimer = playerData.CoyoteTimeDuration;
    public void ClearCoyoteTime() => coyoteTimeTimer = 0f;
    public void CoyoteTimeCountdown()
    {
        if (coyoteTimeTimer > 0) { coyoteTimeTimer -= Time.deltaTime; }
        else { return; }
    }

    //Wall Jump
    public void ResetWallJumpDelay() => wallJumpDelayTimer = playerData.WallJumpDelayDuration;
    public void ClearWallJumpDelay() => wallJumpDelayTimer = 0f;
    public void WallJumpDelayCountdown()
    {
        if (wallJumpDelayTimer > 0) { wallJumpDelayTimer -= Time.deltaTime; }
        else { return; }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position + playerData.GroundCheckOffset, playerData.GroundCheckRadius);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position + (playerData.WallCheckOffset * transform.localScale.x), playerData.WallCheckRadius);
    }
    #endregion
}
