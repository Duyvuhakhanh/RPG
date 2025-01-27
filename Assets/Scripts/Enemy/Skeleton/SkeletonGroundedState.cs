using UnityEngine;
public class SkeletonGroundedState : SkeletonBaseState
{
    public SkeletonGroundedState(Enemy enemy, EnemyStateMachine stateMachine, Animator animator, string animationKey, Enemy_Skeleton skeleton) : base(enemy, stateMachine, animator, animationKey, skeleton)
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
        if (enemy.IsPlayerInSight())
        {
            enemy.enemyStateMachine.ChangeState(skeleton.battleState);
        }
    }
    public override void FixedUpdate()
    {
        base.FixedUpdate();
        enemy.rb.velocity = new Vector2(enemy.moveSpeed * enemy.faceDir, enemy.rb.velocity.y);
    }
}