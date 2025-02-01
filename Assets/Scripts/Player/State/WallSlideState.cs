using UnityEngine;
public class WallSlideState : PlayerBaseState
{
    private float frictionOnWall = 0.5f;
    public WallSlideState(Player player, PlayerStateMachine playerStateMachine, Animator animator, string animationKey) : base(player, playerStateMachine, animator, animationKey)
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
        
        if(yInput > 0)
        {
            PlayerStateMachine.ChangeState(player.JumpWallState);
            return;
        }
        if(xInput != 0 && xInput * player.faceDir < 0)
        {
            PlayerStateMachine.ChangeState(player.IdleState);
        }
        player.rb.velocity = new Vector2(0, yInput >= 0 ? player.rb.velocity.y * frictionOnWall : player.rb.velocity.y);
        if (player.IsOnGround())
        {
            PlayerStateMachine.ChangeState(player.IdleState);
        }

        
    }
    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }
}