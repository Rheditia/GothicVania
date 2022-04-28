using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInAirState : PlayerState
{
    private PlayerInputHandler inputHandler;
    private PlayerLocomotion locomotion;

    public PlayerInAirState(Player player, PlayerStateMachine stateMachine, PlayerDataSO playerData, string animationBool) : base(player, stateMachine, playerData, animationBool)
    {
    }

    public override void Enter()
    {
        base.Enter();
        inputHandler = player.InputHandler;
        locomotion = player.Locomotion;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        player.Animator.SetFloat("yVelocity", Mathf.Clamp(player.Locomotion.VerticalVelocity, -1f, 1f));
        
        player.CoyoteTimeCountdown();
        player.WallJumpDelayCountdown();

        // decrease the jump counter if the player miss the coyote time and registered it as if already jump once
        if (!player.CoyoteTime && player.isFirstJump)
        {
            player.DecreaseJumpCounter();
            player.isFirstJump = false;
        }

        if (inputHandler.JumpInput && player.CheckJumpCounter())
        {            
            stateMachine.ChangeState(player.JumpState);
        }
        else if (player.CheckIfGrounded())
        {
            if (Mathf.Abs(inputHandler.MoveInput.x) > Mathf.Epsilon) { stateMachine.ChangeState(player.MoveState); }
            else if (Mathf.Abs(inputHandler.MoveInput.x) < Mathf.Epsilon) { stateMachine.ChangeState(player.IdleState); }
        }
        else if(player.CheckIfTouchingWall() && (Mathf.Round(inputHandler.MoveInput.x) == player.transform.localScale.x) && locomotion.VerticalVelocity <= 0)
        {
            stateMachine.ChangeState(player.WallSlideState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        if (player.WallJumpDelay) { return; }
        locomotion.SetHorizontalVelocity(playerData.MoveSpeed, inputHandler.MoveInput.x);
    }
}
