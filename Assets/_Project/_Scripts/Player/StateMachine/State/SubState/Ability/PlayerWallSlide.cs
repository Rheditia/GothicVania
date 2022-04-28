using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallSlide : PlayerAbilityState
{
    public PlayerWallSlide(Player player, PlayerStateMachine stateMachine, PlayerDataSO playerData, string animationBool) : base(player, stateMachine, playerData, animationBool)
    {
    }

    public override void Enter()
    {
        base.Enter();
        player.ResetJumpCounter();
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

        base.LogicUpdate();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        locomotion.SetVerticalVelocity(playerData.WallSlideYVelocity);
        locomotion.SetHorizontalVelocity(playerData.MoveSpeed, inputHandler.MoveInput.x);
    }
}
