using UnityEngine;
namespace Enemy.Skeleton
{
    public class SkeletonAttackState : SkeletonBaseState
    {
        public SkeletonAttackState(Enemy enemy, EnemyStateMachine stateMachine, Animator animator, string animationKey, Enemy_Skeleton skeleton) : base(enemy, stateMachine, animator, animationKey, skeleton)
        {
        }
        public override void Enter()
        {
            base.Enter();
            skeleton.SetVelocity(Vector2.zero);
        }
        public override void Exit()
        {
            base.Exit();
        }
        public override void Update()
        {
            base.Update();
            if(triggerCalled)
            {
                stateMachine.ChangeState(skeleton.battleState);
            }

        }
        public override void FixedUpdate()
        {
            base.FixedUpdate();
        }
    }
}
