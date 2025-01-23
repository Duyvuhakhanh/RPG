using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
public class BaseState : IState
{
    protected readonly Player player;
    protected readonly Animator animator;
    protected readonly int hasKey;
    protected readonly Dictionary<IState, IPredicate> allTransitions = new Dictionary<IState, IPredicate>();

    public BaseState(Player player, Animator animator, string animationKey,
        params (IState state, IPredicate condition)[] transitions)
    {
        this.player = player;
        this.animator = animator;
        hasKey = Animator.StringToHash(animationKey);
        foreach(var tran in transitions)
        {
            if (!allTransitions.ContainsKey(tran.state)) allTransitions.Add(tran.state, tran.condition);
        }
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
    }
}
