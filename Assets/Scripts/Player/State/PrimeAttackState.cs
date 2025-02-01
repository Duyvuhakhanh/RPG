using UnityEngine;
namespace Player.State
{
    public class PrimeAttackState : PlayerBaseState
    {
        private static readonly int ComboCounter = Animator.StringToHash("ComboCounter");
        private static readonly int Attack = Animator.StringToHash("Attack");
        private int comboCounter;
        private float lastTimeAttack;
        private float comboWindow = 2;
        public PrimeAttackState(Player player, PlayerStateMachine playerStateMachine, Animator animator, string animationKey) : base(player, playerStateMachine, animator, animationKey)
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
            stateTimmer = 0.1f;
            float attackDirection = player.faceDir;
            xInput = Input.GetAxisRaw("Horizontal");
            if(xInput != 0)
            {
                attackDirection = xInput;
            }
            player.SetVelocity(new(player.attackMovement[comboCounter].x * attackDirection, player.attackMovement[comboCounter].y));
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
                PlayerStateMachine.ChangeState(player.IdleState);
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
}
