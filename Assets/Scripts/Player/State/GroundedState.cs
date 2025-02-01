using UnityEngine;
namespace Player.State
{
    public class GroundedState : PlayerBaseState
    {
        public GroundedState(Player player, PlayerStateMachine playerStateMachine, Animator animator, string animationKey) : base(player, playerStateMachine, animator, animationKey)
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
            if (yInput > 0)
            {
                PlayerStateMachine.ChangeState(player.JumpState);
            }
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                PlayerStateMachine.ChangeState(player.PrimeAttackState);
            }
            if (Input.GetKeyDown(KeyCode.Q))
            {
                PlayerStateMachine.ChangeState(player.CounterAttackState);
            }
            if (Input.GetKeyDown(KeyCode.Mouse1))
            {
                PlayerStateMachine.ChangeState(player.AimSwordState);
            }
        }
    }
}