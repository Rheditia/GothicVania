using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallSlideState : PlayerAbilityState
{
    public PlayerWallSlideState(Player player, PlayerStateMachine stateMachine, PlayerDataSO playerData, string animationBool) : base(player, stateMachine, playerData, animationBool)
    {
    }

    public override void Enter()
    {
        base.Enter();
        player.ResetJumpCounter();
        player.ResetDashCounter();
        player.ClearWallJumpDelay();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        if (!player.CheckIfTouchingWall() || player.CheckIfGrounded())
        {
            player.DecreaseJumpCounter();
            isAbilityDone = true;
        }

        if (inputHandler.JumpInput && player.CheckJumpCounter())
        {
            player.ResetWallJumpDelay();
            player.isFirstJump = true;
            locomotion.SetHorizontalVelocity(playerData.WallJumpXVelocity, -player.transform.localScale.x);
            stateMachine.ChangeState(player.JumpState);
        }
        else if (inputHandler.DashInput && player.CheckDashCounter())
        {
            //locomotion.SetHorizontalVelocity(playerData.DashXVelocity, -player.transform.localScale.x);
            stateMachine.ChangeState(player.DashState);
        }

        base.LogicUpdate();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        locomotion.SetVerticalVelocity(playerData.WallSlideYVelocity);
        locomotion.SetHorizontalVelocity(playerData.MoveSpeed, inputHandler.MoveInput.x);
    }
}
