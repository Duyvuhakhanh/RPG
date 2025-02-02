using UnityEngine;

namespace Player.State
{
    public class AimSwordState : PlayerBaseState
    {
        private Camera _camera;
        public AimSwordState(Player player, PlayerStateMachine playerStateMachine, Animator animator, string animationKey) :
            base(player, playerStateMachine, animator, animationKey)
        {
        }
        public override void Enter()
        {
            base.Enter();
            player.SetVelocity(0, 0);
            _camera = Camera.main;
            player.playerVisualizeSwordAbility.OnEnterAimState();
        }
        public override void Exit()
        {
            base.Exit();
            player.playerVisualizeSwordAbility.OnExitAimState();
        }
        public override void Update()
        {
            base.Update();
            if (Input.GetKeyUp(KeyCode.Mouse1))
            {
                PlayerStateMachine.ChangeState(player.IdleState);
            }
            Vector2 mousePos = _camera.ScreenToWorldPoint(Input.mousePosition);

            if (player.faceDir * (mousePos.x - player.transform.position.x) < 0)
            {
                player.Flip();
            }
        }

        public override void FixedUpdate()
        {
            base.FixedUpdate();
        }
    }
}
