using UnityEngine;
public class SkeletonIdleBaseState : SkeletonGroundedState
{

    public SkeletonIdleBaseState(Enemy enemy, EnemyStateMachine stateMachine, Animator animator, string animationKey, Enemy_Skeleton skeleton) : base(enemy, stateMachine, animator, animationKey, skeleton)
    {
    }
    public override void Enter()
    {
        base.Enter();
        skeleton.SetVelocity(Vector2.zero);
        stateTimmer = 1f;
    }
    public override void Exit()
    {
        base.Exit();
    }
    public override void Update()
    {
        base.Update();
        if (stateTimmer < 0)
        {
            stateMachine.ChangeState(skeleton.moveState);
        }

    }
    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }
}