using UnityEngine;
public class SkeletonBaseState : EnemyBaseState
{
    protected Enemy_Skeleton skeleton;
    public SkeletonBaseState(Enemy enemy, EnemyStateMachine stateMachine, Animator animator, string animationKey, Enemy_Skeleton skeleton) : base(enemy, stateMachine, animator, animationKey)
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
    }
    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }
}
