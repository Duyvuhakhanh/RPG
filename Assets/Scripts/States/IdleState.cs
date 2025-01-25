using UnityEngine;
public class IdleState : GroundedState
{
    public IdleState(Player player, StateMachine stateMachine, Animator animator, string animationKey) : base(player, stateMachine, animator, animationKey)
    {
    }
    public override void Enter()
    {
        base.Enter();

    }
    public override void Update()
    {
        base.Update();
        if(xInput != 0)
        {
            stateMachine.ChangeState(player.MoveState);
        }
    }


}
