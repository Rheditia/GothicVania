using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpState : PlayerAbilityState
{
    public PlayerJumpState(Player player, PlayerStateMachine stateMachine, PlayerDataSO playerData, string animationBool) : base(player, stateMachine, playerData, animationBool)
    {
    }

    public override void Enter()
    {
        base.Enter();

        inputHandler.ClearJumpBuffer();
        player.DecreaseJumpCounter();
        player.ClearCoyoteTime();
        player.isFirstJump = false;

        locomotion.SetVerticalVelocity(playerData.JumpSpeed);
        isAbilityDone = true;
    }
}
