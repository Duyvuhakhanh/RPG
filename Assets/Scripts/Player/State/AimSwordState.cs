using UnityEngine;
namespace Player.State
{
    public class AimSwordState : PlayerBaseState
    {
        public AimSwordState(Player player, PlayerStateMachine playerStateMachine, Animator animator, string animationKey) : base(player, playerStateMachine, animator, animationKey)
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
            if(Input.GetKeyUp(KeyCode.Mouse1))
            {
                PlayerStateMachine.ChangeState(player.IdleState);
            }
        }
        public override void FixedUpdate()
        {
            base.FixedUpdate();
        }
    }
}