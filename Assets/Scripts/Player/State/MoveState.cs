using UnityEngine;
public class MoveState : GroundedState
{


    public MoveState(Player player, PlayerStateMachine playerStateMachine, Animator animator, string animationKey) : base(player, playerStateMachine, animator, animationKey)
    {
    }
    public override void Enter()
    {
        base.Enter();
    }
    public override void Exit()
    {
        //player.SetVelocity(Vector2.zero);
        base.Exit();
    }
    public override void Update()
    {
        base.Update();
        if(xInput == 0)
        {
            PlayerStateMachine.ChangeState(player.IdleState);
        }
        if (player.rb.velocity.y < 0)
        {
            PlayerStateMachine.ChangeState(player.AirState);
        }

    }
    public override void FixedUpdate()
    {
        base.FixedUpdate();

        player.SetVelocity(new Vector2(xInput * player.speed, player.rb.velocity.y));
    }
}
