using UnityEngine;
public class WallSlideState : BaseState
{
    private float frictionOnWall = 0.5f;
    public WallSlideState(Player player, StateMachine stateMachine, Animator animator, string animationKey) : base(player, stateMachine, animator, animationKey)
    {
    }
    public override void Enter()
    {
        player.rb.velocity = new Vector2(0, 0);
        
        base.Enter();
    }
    public override void Exit()
    {
        base.Exit();
    }
    public override void Update()
    {
        base.Update();
        if(xInput != 0 && xInput * player.faceDir < 0)
        {
            stateMachine.ChangeState(player.IdleState);
        }
        player.rb.velocity = new Vector2(0, yInput >= 0 ? player.rb.velocity.y * frictionOnWall : player.rb.velocity.y);
        if (player.IsOnGround())
        {
            stateMachine.ChangeState(player.IdleState);
        }
        
    }
    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }
}
