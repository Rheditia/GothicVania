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
    private float coyoteTimeTimer;
    public bool CoyoteTime => coyoteTimeTimer > 0;
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

    public bool CheckJumpCounter()
    {
        if (jumpCount > 0) { return true; }
        else { return false; }
    }
    #endregion

    #region Others
    public void ResetJumpCounter() => jumpCount = playerData.JumpAmount;

    public void DecreaseJumpCounter() => jumpCount--;

    public void ResetCoyoteTime() => coyoteTimeTimer = playerData.CoyoteTimeDuration;

    public void ClearCoyoteTime() => coyoteTimeTimer = 0f;

    public void CoyoteTimeCountdown()
    {
        if (coyoteTimeTimer > 0) { coyoteTimeTimer -= Time.deltaTime; }
        else { return; }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position + playerData.GroundCheckOffset, playerData.GroundCheckRadius);
    }
    #endregion
}
