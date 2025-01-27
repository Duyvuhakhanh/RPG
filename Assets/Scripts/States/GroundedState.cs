using UnityEngine;
public class GroundedState : PlayerBaseState
{
    public GroundedState(Player player, PlayerStateMachine playerStateMachine, Animator animator, string animationKey) : base(player, playerStateMachine, animator, animationKey)
    {
    }
    public override void Enter()
    {
        base.Enter();
    }
    public override void Exit()
    {
        base.Exit();
    }
    public override void Update()
    {
        base.Update();
        if (yInput > 0)
        {
            PlayerStateMachine.ChangeState(player.JumpState);
        }
        if (Input.GetMouseButton(0))
        {
            PlayerStateMachine.ChangeState(player.PrimeAttackState);
        }
    }
}