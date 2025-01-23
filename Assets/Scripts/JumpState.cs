using UnityEngine;
public class JumpState : BaseState
{
    public JumpState(Player player, Animator animator, string animationKey) : base(player, animator, animationKey)
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
        player.rb.velocity = new Vector2(player.xInput * player.speed, player.rb.velocity.y);
        base.Update();
    }
}
