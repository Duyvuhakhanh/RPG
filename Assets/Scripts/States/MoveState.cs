using UnityEngine;
public class MoveState : GroundedState
{


    public MoveState(Player player, StateMachine stateMachine, Animator animator, string animationKey) : base(player, stateMachine, animator, animationKey)
    {
    }
    public override void Enter()
    {
        base.Enter();
    }
    public override void Exit()
    {
        player.rb.velocity = Vector2.zero;
    }
    public override void Update()
    {
        base.Update();
        if(xInput == 0)
        {
            stateMachine.ChangeState(player.IdleState);
        }
        if (player.rb.velocity.y < 0)
        {
            stateMachine.ChangeState(player.AirState);
        }
    }
    public override void FixedUpdate()
    {
        base.FixedUpdate();
        player.rb.velocity = new Vector2(xInput * player.speed, player.rb.velocity.y);
    }
}
