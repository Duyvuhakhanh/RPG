using UnityEngine;
namespace Enemy.Skeleton
{
    public class SkeletonStunState : SkeletonBaseState
    {
        public SkeletonStunState(Enemy enemy, EnemyStateMachine stateMachine, Animator animator, string animationKey, Enemy_Skeleton skeleton) : base(enemy, stateMachine, animator, animationKey, skeleton)
        {
        }
        public override void Enter()
        {
            base.Enter();
            stateTimmer = skeleton.stunTime;
            skeleton.enityFx.BlinkRed(0.1f);
            skeleton.rb.velocity = new Vector2(- skeleton.faceDir * skeleton.stunKnockBackDirection.x,skeleton.stunKnockBackDirection.y);
        }
        public override void Exit()
        {
            base.Exit();
            skeleton.enityFx.CancelBlink();
        }
        public override void Update()
        {
            base.Update();
            if (stateTimmer < 0)
            {
                stateMachine.ChangeState(skeleton.idleState);
            }

        }
        public override void FixedUpdate()
        {
            base.FixedUpdate();
        }
    }
}