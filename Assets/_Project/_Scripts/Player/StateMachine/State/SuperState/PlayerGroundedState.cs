using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerGroundedState : PlayerState
{
    protected PlayerLocomotion locomotion;
    protected PlayerInputHandler inputHandler;

    protected PlayerGroundedState(Player player, PlayerStateMachine stateMachine, PlayerDataSO playerData, string animationBool) : base(player, stateMachine, playerData, animationBool)
    {
    }

    public override void Enter()
    {
        base.Enter();
        player.Animator.SetFloat("yVelocity", 0f);

        locomotion = player.Locomotion;
        inputHandler = player.InputHandler;

        player.isFirstJump = true;
        player.ResetJumpCounter();
        player.ResetDashCounter();
        player.ResetCoyoteTime();
        player.ClearWallJumpDelay();
        inputHandler.ClearDashBuffer();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        player.DashDelayCountdown();

        if (inputHandler.JumpInput && player.CheckJumpCounter())
        {
            stateMachine.ChangeState(player.JumpState);
        }
        else if (!player.CheckIfGrounded())
        {
            stateMachine.ChangeState(player.InAirState);
        }
        else if (inputHandler.DashInput && !player.DashDelay)
        {
            stateMachine.ChangeState(player.DashState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
