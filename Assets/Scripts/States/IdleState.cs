using UnityEngine;
public class IdleState : GroundedState
{
    public IdleState(Player player, StateMachine stateMachine, Animator animator, string animationKey) : base(player, stateMachine, animator, animationKey)
    {
    }
    public override void Enter()
    {
        base.Enter();
        player.SetVelocity(Vector2.zero);

    }
    public override void Update()
    {
        base.Update();
        if(xInput != 0 && !player.isBusy)
        {
            stateMachine.ChangeState(player.MoveState);
        }
    }

}
