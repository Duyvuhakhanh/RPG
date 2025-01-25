using UnityEngine;
public class GroundedState : BaseState
{
    public GroundedState(Player player, StateMachine stateMachine, Animator animator, string animationKey) : base(player, stateMachine, animator, animationKey)
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
            stateMachine.ChangeState(player.JumpState);
        }
        if (Input.GetMouseButton(0))
        {
            stateMachine.ChangeState(player.PrimeAttackState);
        }
    }
}