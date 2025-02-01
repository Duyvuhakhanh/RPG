using UnityEngine;
namespace Player.State
{
    public class CatchSwordState : PlayerBaseState
    {
        public CatchSwordState(Player player, PlayerStateMachine playerStateMachine, Animator animator, string animationKey) : base(player, playerStateMachine, animator, animationKey)
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
            if (yInput < 0)
            {
                PlayerStateMachine.ChangeState(player.IdleState);
            }
            if (xInput != 0)
            {
                PlayerStateMachine.ChangeState(player.MoveState);
            }
        }
        public override void FixedUpdate()
        {
            base.FixedUpdate();
        }
    }
}
