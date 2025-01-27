using UnityEngine;
public class EnenmyGroundedState : EnemyBaseState
{
    protected Enemy_Skeleton skeleton;
    public EnenmyGroundedState(Enemy enemy, EnemyStateMachine stateMachine, Animator animator, string animationKey, Enemy_Skeleton skeleton) :  base(enemy, stateMachine, animator, animationKey)
    {
        this.skeleton = skeleton;
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
        if (!enemy.IsPlayerDetected())
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