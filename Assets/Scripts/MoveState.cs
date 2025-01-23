using UnityEngine;
public class MoveState : BaseState
{


    public MoveState(Player player, Animator animator, string animationKey) : base(player, animator, animationKey)
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
        player.rb.velocity = new Vector2(player.xInput * player.speed, player.rb.velocity.y);
    }
}