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
        player.ResetCoyoteTime();
        player.ClearWallJumpDelay();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (inputHandler.JumpInput && player.CheckJumpCounter())
        {
            stateMachine.ChangeState(player.JumpState);
        }
        else if (!player.CheckIfGrounded())
        {
            //player.DecreaseJumpCounter();
            stateMachine.ChangeState(player.InAirState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
