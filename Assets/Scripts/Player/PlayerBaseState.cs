using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
public class PlayerBaseState : BaseState
{
    protected Player player;
    protected float xInput;
    protected float yInput;

    public PlayerBaseState(Player player, PlayerStateMachine playerStateMachine, Animator animator, string animationKey
    )
    {
        this.PlayerStateMachine = playerStateMachine;
        this.player = player;
        this.animator = animator;
        hashKey = Animator.StringToHash(animationKey);

    }

    public override void Update()
    {
        base.Update();
        xInput = Input.GetAxis("Horizontal");
        yInput = Input.GetAxis("Vertical");


    }
 

}