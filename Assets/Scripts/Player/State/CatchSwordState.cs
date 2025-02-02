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

            UpdateDirection();
        }
        private void UpdateDirection()
        {
            if(player.faceDir * (player.sword.transform.position.x - player.transform.position.x) < 0)
            {
                player.Flip();
            }
        }
        public override void Update()
        {
            base.Update();
            if(triggerCalled)
            {
                PlayerStateMachine.ChangeState(player.IdleState);
            }
        }
    }
}
