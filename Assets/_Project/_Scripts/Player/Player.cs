using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public PlayerIdleState IdleState { get; private set; }
    public PlayerMoveState MoveState { get; private set; }

    public PlayerStateMachine StateMachine { get; private set; }
    public Animator Animator { get; private set; }

    private void Awake()
    {
        StateMachine = new PlayerStateMachine();
        Animator = GetComponent<Animator>();

        IdleState = new PlayerIdleState(this, StateMachine, "idle");
        MoveState = new PlayerMoveState(this, StateMachine, "run");
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
}
