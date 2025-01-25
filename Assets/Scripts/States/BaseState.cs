using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
public class BaseState : IState
{
    protected readonly Player player;
    protected readonly Animator animator;
    protected readonly int hasKey;
    protected readonly StateMachine stateMachine;
    protected float stateTimmer;
    protected float xInput { get; private set; }
    protected float yInput { get; private set; } 
    protected readonly Dictionary<IState, IPredicate> allTransitions = new Dictionary<IState, IPredicate>();

    public BaseState(Player player, StateMachine stateMachine, Animator animator, string animationKey
    )
    {
        this.stateMachine = stateMachine;
        this.player = player;
        this.animator = animator;
        hasKey = Animator.StringToHash(animationKey);

    }

    public virtual void Enter()
    {
        animator.Play(hasKey);
    }
    public virtual void Exit()
    {
    }
    public virtual void Update()
    {
        stateTimmer -= Time.deltaTime;
        xInput = Input.GetAxis("Horizontal");
        yInput = Input.GetAxis("Vertical");

    }
    public virtual void FixedUpdate()
    {
    }
}