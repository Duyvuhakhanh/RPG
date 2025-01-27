using UnityEngine;
public class SkeletonMoveState : SkeletonGroundedState
{

    public SkeletonMoveState(Enemy enemy, EnemyStateMachine stateMachine, Animator animator, string animationKey, Enemy_Skeleton skeleton) : base(enemy, stateMachine, animator, animationKey, skeleton)
    {
    }
    public override void Update()
    {
        base.Update();
        skeleton.SetVelocity(new Vector2(skeleton.moveSpeed * skeleton.faceDir, enemy.rb.velocity.y));
        if (skeleton.IsWallDetected() || !enemy.IsOnGround())
        {
            stateMachine.ChangeState(skeleton.idleState);
            skeleton.Flip();
        }
    }
    
}