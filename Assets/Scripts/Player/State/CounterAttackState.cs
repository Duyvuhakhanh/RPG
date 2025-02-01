using UnityEngine;
public class CounterAttackState : PlayerBaseState
{

    public CounterAttackState(Player player, PlayerStateMachine playerStateMachine, Animator animator, string animationKey) : base(player, playerStateMachine, animator, animationKey)
    {
    }
    public override void Enter()
    {
        base.Enter();
        stateTimmer = player.counterAttackDuration;
        animator.SetBool(AnimationKeys.SuccessfulCounterAttack, false);

    }
    public override void Update()
    {
        base.Update();
        Collider2D[] hitTargets = Physics2D.OverlapCircleAll(player.attackCheck.position, player.attackCheckRadius, player.whatIsEnemy);
        foreach(var target in hitTargets)
        {
            var targetInfo = target.GetComponent<Enemy>();
            if(targetInfo != null && targetInfo.CanBeStunned())
            {
                stateTimmer = 10;
                animator.SetBool(AnimationKeys.SuccessfulCounterAttack, true);

            }
        }
        if (stateTimmer < 0 || triggerCalled)           
        {
            animator.SetBool(AnimationKeys.SuccessfulCounterAttack, false);
            PlayerStateMachine.ChangeState((player.IdleState));
        }
    }
    public override void Exit()
    {
        base.Exit();
    }
}
