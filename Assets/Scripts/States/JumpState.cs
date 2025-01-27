using UnityEngine;
public class JumpState : PlayerBaseState
{
    public JumpState(Player player, PlayerStateMachine playerStateMachine, Animator animator, string animationKey) : base(player, playerStateMachine, animator, animationKey)
    {
    }
    public override void Enter()
    {
        player.SetVelocity(new Vector2(player.rb.velocity.x, player.jumpForce));

        base.Enter();
    }
    public override void Exit()
    {
        base.Exit();
    }
    public override void Update()
    {
        base.Update();
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