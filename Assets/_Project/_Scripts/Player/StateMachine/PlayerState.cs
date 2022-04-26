using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerState
{
    protected Player player;
    protected PlayerStateMachine stateMachine;
    protected PlayerDataSO playerData;
    protected string animationBool;

    public PlayerState(Player player, PlayerStateMachine stateMachine, PlayerDataSO playerData, string animationBool)
    {
        this.player = player;
        this.stateMachine = stateMachine;
        this.playerData = playerData;
        this.animationBool = animationBool;
    }

    public virtual void Enter()
    {
        player.Animator.SetBool(animationBool, true);
        //Debug.Log(animationBool);
    }

    public virtual void LogicUpdate()
    {

    }

    public virtual void PhysicsUpdate()
    {

    }

    public virtual void Exit()
    {
        player.Animator.SetBool(animationBool, false);
    }
}
