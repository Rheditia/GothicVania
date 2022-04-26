using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : PlayerGroundedState
{
    public PlayerIdleState(Player player, PlayerStateMachine stateMachine, PlayerDataSO playerData, string animationBool) : base(player, stateMachine, playerData, animationBool)
    {
    }

    public override void Enter()
    {
        base.Enter();
        locomotion.SetHorizontalVelocity(0f, inputHandler.MoveInput.x);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (Mathf.Abs(inputHandler.MoveInput.x) > Mathf.Epsilon)
        {
            stateMachine.ChangeState(player.MoveState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
