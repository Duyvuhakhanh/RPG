using UnityEngine;
namespace Player.State
{
    public class IdleState : GroundedState
    {
        public IdleState(Player player, PlayerStateMachine playerStateMachine, Animator animator, string animationKey) : base(player, playerStateMachine, animator, animationKey)
        {
        }
        public override void Enter()
        {
            base.Enter();
            player.SetVelocity(Vector2.zero);

        }
        public override void Update()
        {
            base.Update();
            if(xInput != 0 && !player.isBusy)
            {
                PlayerStateMachine.ChangeState(player.MoveState);
            }
        }

    }
}
