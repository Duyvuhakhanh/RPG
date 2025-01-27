using UnityEngine;
public class SkeletonBattleState : EnemyBaseState
{
    protected Enemy_Skeleton skeleton;

    public SkeletonBattleState(Enemy enemy, EnemyStateMachine stateMachine, Animator animator, string animationKey, Enemy_Skeleton skeleton) : base(enemy, stateMachine, animator, animationKey)
    {
        this.skeleton = skeleton;
    }
    public override void Update()
    {
        base.Update();
        if (skeleton.IsPlayerDetected())
        {
            stateMachine.ChangeState(skeleton.moveState);
        }
    }
}
