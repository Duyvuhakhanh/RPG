using UnityEngine;
public class JumpState : BaseState
{
    public JumpState(Player player, StateMachine stateMachine, Animator animator, string animationKey) : base(player, stateMachine, animator, animationKey)
    {
    }
    public override void Enter()
    {
        player.rb.velocity = new Vector2(player.rb.velocity.x, player.jumpForce);

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
            stateMachine.ChangeState(player.AirState);
        }
    }
    public override void FixedUpdate()
    {
        base.FixedUpdate();
        player.rb.velocity = new Vector2(xInput * player.speed, player.rb.velocity.y);
    }
}