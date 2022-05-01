using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDieState : PlayerState
{
    PlayerLocomotion locomotion;
    public PlayerDieState(Player player, PlayerStateMachine stateMachine, PlayerDataSO playerData, string animationBool) : base(player, stateMachine, playerData, animationBool)
    {
    }

    public override void Enter()
    {
        base.Enter();
        locomotion = player.Locomotion;

        locomotion.SetDeathKnockback(player.DeathImpact);

        player.Die();
    }

    public override void LogicUpdate() { }
}
