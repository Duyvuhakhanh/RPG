using UnityEngine;
public class JumpWallState : BaseState
{

    public JumpWallState(Player player, StateMachine stateMachine, Animator animator, string animationKey) : base(player, stateMachine, animator, animationKey)
    {
    }
    public override void Enter()
    {
        base.Enter();
        OnJump();
    }

    public override void Update()
    {
        base.Update();


    }
    public override void FixedUpdate()
    {
        base.FixedUpdate();
        player.rb.velocity = new Vector2(xInput * player.speed, player.rb.velocity.y);

    }
    public override void Exit()
    {
        base.Exit();
        
    }
    private void OnJump()
    {
        player.rb.velocity = new Vector2( - player.faceDir * player.jumpWallForce, player.jumpForce);
    }
}
