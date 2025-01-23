using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class PlayerState
{
    private Player player;
    private StateMachine stateMachine;
    public PlayerState([NotNull] Player player, [NotNull] StateMachine stateMachine)
    {
        this.player = player ?? throw new ArgumentNullException(nameof(player));
        this.stateMachine = stateMachine ?? throw new ArgumentNullException(nameof(stateMachine));
    }
    public virtual void Enter()
    {
        Debug.Log("PlayerState Enter");
    }
    public virtual void Exit()
    {
        Debug.Log("PlayerState Exit");
    }
    public virtual void Update()
    {
        Debug.Log("PlayerState Update");
    }
}