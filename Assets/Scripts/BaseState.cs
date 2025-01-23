using System;
using JetBrains.Annotations;
using UnityEngine;
public class BaseState : IState
{
    private Player player;
    private StateMachine stateMachine;
    public BaseState([NotNull] Player player, [NotNull] StateMachine stateMachine)
    {
        this.player = player ?? throw new ArgumentNullException(nameof(player));
        this.stateMachine = stateMachine ?? throw new ArgumentNullException(nameof(stateMachine));
    }
    public virtual void Enter()
    {
        Debug.Log("BaseState Enter");
    }
    public virtual void Exit()
    {
        Debug.Log("BaseState Exit");
    }
    public virtual void Update()
    {
        Debug.Log("BaseState Update");
    }
}
