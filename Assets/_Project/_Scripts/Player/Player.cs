using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    #region StateMachine
    public PlayerStateMachine StateMachine { get; private set; }
    public PlayerIdleState IdleState { get; private set; }
    public PlayerMoveState MoveState { get; private set; }
    #endregion

    #region Component
    [SerializeField] PlayerDataSO playerData;
    public Animator Animator { get; private set; }
    public PlayerInputHandler InputHandler { get; private set; }
    public PlayerLocomotion Locomotion { get; private set; }
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
}
