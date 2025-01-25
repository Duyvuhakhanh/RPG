using UnityEngine;
public class AirState : BaseState
{
    private float speedXOnAir = 0.8f;
    public AirState(Player player, StateMachine stateMachine, Animator animator, string animationKey) : base(player, stateMachine, animator, animationKey)
    {
    }
    public override void Enter()
    {
        base.Enter();
    }
    public override void Exit()
    {
        base.Exit();
    }
    public override void Update()
    {
        base.Update();
        if (player.IsWallDetected() && xInput * player.faceDir > 0)
        {
            stateMachine.ChangeState(player.WallSlideState);
        }
        if (player.IsOnGround())
        {
            stateMachine.ChangeState(player.IdleState);
        }

    }
    public override void FixedUpdate()
    {
        base.FixedUpdate();
        player.SetVelocity(new Vector2(xInput * player.speed * speedXOnAir, player.rb.velocity.y)) ;

    }
}
