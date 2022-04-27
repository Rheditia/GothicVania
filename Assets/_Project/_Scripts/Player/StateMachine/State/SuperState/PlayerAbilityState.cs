using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbilityState : PlayerState
{
    protected PlayerInputHandler inputHandler;
    protected PlayerLocomotion locomotion;

    protected bool isAbilityDone;
    
    public PlayerAbilityState(Player player, PlayerStateMachine stateMachine, PlayerDataSO playerData, string animationBool) : base(player, stateMachine, playerData, animationBool)
    {
    }

    public override void Enter()
    {
        base.Enter();
        inputHandler = player.InputHandler;
        locomotion = player.Locomotion;

        isAbilityDone = false;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (isAbilityDone)
        {
            if (player.CheckIfGrounded())
            {
                if (Mathf.Abs(inputHandler.MoveInput.x) > Mathf.Epsilon) { stateMachine.ChangeState(player.MoveState); }
                else if (Mathf.Abs(inputHandler.MoveInput.x) < Mathf.Epsilon) { stateMachine.ChangeState(player.IdleState); }
            }
            else if (!player.CheckIfGrounded())
            {
                stateMachine.ChangeState(player.InAirState);
            }
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
