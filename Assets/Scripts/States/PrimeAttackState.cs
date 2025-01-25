using UnityEngine;
public class PrimeAttackState : BaseState
{
    private static readonly int ComboCounter = Animator.StringToHash("ComboCounter");
    private static readonly int Attack = Animator.StringToHash("Attack");
    private int comboCounter;
    private float lastTimeAttack;
    private float comboWindow = 2;
    public PrimeAttackState(Player player, StateMachine stateMachine, Animator animator, string animationKey) : base(player, stateMachine, animator, animationKey)
    {
    }
    public override void Enter()
    {
        base.Enter();
        if (comboCounter > 2 || Time.time - lastTimeAttack > comboWindow)
        {
            comboCounter = 0;
        }
        animator.SetInteger(ComboCounter, comboCounter);
        player.SetVelocity(new(player.attackMovement[comboCounter].x * player.faceDir, player.attackMovement[comboCounter].y));
        //stateTimmer = comboCounter == 0 ? 0.1f : 0f;
        stateTimmer = 0.1f;
        player.LockAcitivity(0.15f);
        
    }
    public override void Update()
    {
        base.Update();
        if (stateTimmer < 0)
        {
            player.SetVelocity(Vector2.zero);
        }
        if (triggerCalled)
        {
            stateMachine.ChangeState(player.IdleState);
        }
    }
    public override void Exit()
    {
        comboCounter++;
        lastTimeAttack = Time.time;
        player.LockAcitivity(0.15f);
        base.Exit();
    }
}
