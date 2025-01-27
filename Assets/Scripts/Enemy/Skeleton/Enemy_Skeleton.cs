using System;

public class Enemy_Skeleton : Enemy
{

    #region States

    public SkeletonIdleBaseState idleState;
    public SkeletonMoveState moveState;
    public SkeletonBattleState battleState;
    public SkeletonAttackState attackState;

    #endregion

    protected override void Awake()
    {
        base.Awake();
    }
    private void Start()
    {
        idleState = new(this, enemyStateMachine, animator, AnimationKeys.Idle, this);
        moveState = new(this, enemyStateMachine, animator, AnimationKeys.Move, this);
        battleState = new(this, enemyStateMachine, animator, AnimationKeys.Move, this);
        attackState = new(this, enemyStateMachine, animator, AnimationKeys.Attack, this);
        enemyStateMachine.SetState(idleState);
    }
    protected override void Update()
    {
        base.Update();
    }


}

