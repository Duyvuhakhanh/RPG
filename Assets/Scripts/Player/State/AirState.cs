using UnityEngine;
namespace Player.State
{
    public class AirState : PlayerBaseState
    {
        private float speedXOnAir = 0.8f;
        public AirState(Player player, PlayerStateMachine playerStateMachine, Animator animator, string animationKey) : base(player, playerStateMachine, animator, animationKey)
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
                PlayerStateMachine.ChangeState(player.WallSlideState);
            }
            if (player.IsOnGround())
            {
                PlayerStateMachine.ChangeState(player.IdleState);
            }

        }
        public override void FixedUpdate()
        {
            base.FixedUpdate();
            player.SetVelocity(new Vector2(xInput * player.speed * speedXOnAir, player.rb.velocity.y)) ;

        }
    }
}