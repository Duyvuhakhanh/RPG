using UnityEngine;
public class JumpWallState : BaseState
{

    public JumpWallState(Player player, StateMachine stateMachine, Animator animator, string animationKey) : base(player, stateMachine, animator, animationKey)
    {
    }
    public override void Enter()
    {
        base.Enter();
        player.SetVelocity( new Vector2(- player.jumpWallForce * player.faceDir, player.jumpForce));
        stateTimmer = player.jumpWallTime;
    }

    public override void Update()
    {
        base.Update();

        if (stateTimmer < 0)
        {
            stateMachine.ChangeState(player.AirState);
        }

    }
    public override void FixedUpdate()
    {
        base.FixedUpdate();
        player.rb.velocity = new Vector2(xInput !=0 ? xInput* player.rb.velocity.x : player.rb.velocity.x, player.rb.velocity.y);

    }
    public override void Exit()
    {
        base.Exit();
        
    }
    private void OnJump()
    {
        player.SetVelocity(new Vector2(-player.faceDir * player.jumpWallForce, player.jumpForce));
    }
}
