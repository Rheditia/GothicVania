using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDashState : PlayerAbilityState
{
    private bool dashStartPos;
    private bool dashEndPos;
    public PlayerDashState(Player player, PlayerStateMachine stateMachine, PlayerDataSO playerData, string animationBool) : base(player, stateMachine, playerData, animationBool)
    {
    }

    public override void Enter()
    {
        base.Enter();
        player.DecreaseDashCounter();
        player.ResetDashTime();

        // prevent coyote time from decreasing jump counter
        player.isFirstJump = false;

        if (player.CheckIfTouchingWall()) // wall to air
        {
            dashStartPos = player.CheckIfTouchingWall();
            locomotion.SetHorizontalVelocity(playerData.DashXVelocity, -player.transform.localScale.x);
        }
        else // ground to air / ground to ground / air to air
        {
            dashStartPos = player.CheckIfGrounded();
            locomotion.SetHorizontalVelocity(playerData.DashXVelocity, player.transform.localScale.x);
        }
    }

    public override void Exit()
    {
        base.Exit();
        dashEndPos = player.CheckIfGrounded();
        if(dashStartPos != dashEndPos) { player.DecreaseJumpCounter(); }

        player.ClearDashTime();
        player.ResetDashDelay();
    }

    public override void LogicUpdate()
    {
        player.DashDurationCountdown();
        if (!player.DashTime) { isAbilityDone = true; }
        base.LogicUpdate();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        locomotion.SetVerticalVelocity(0f);
    }
}
