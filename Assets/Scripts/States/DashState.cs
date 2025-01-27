using UnityEngine;
public class DashState : PlayerBaseState
{
    private PlayerBaseState preState;
    public DashState(Player player, PlayerStateMachine playerStateMachine, Animator animator, string animationKey) : base(player, playerStateMachine, animator, animationKey)
    {
    }
    public override void Enter()
    {
        base.Enter();
        stateTimmer = player.dashTime;
        preState = PlayerStateMachine.PreState;
        OnDash();
        player.rb.velocity = new Vector2(player.rb.velocity.x, 0);

    }

    public override void Update()
    {
        base.Update();
        if (!player.IsOnGround() && player.IsWallDetected())
        {
            PlayerStateMachine.ChangeState(player.WallSlideState);
        }
        OnDash();
        if(stateTimmer <= 0)
        {
            Exit();
        }

    }
    public override void FixedUpdate()
    {
        base.FixedUpdate();
        
    }
    public override void Exit()
    {
        PlayerStateMachine.ChangeState(preState.GetType() == typeof(JumpState) || preState.GetType() == typeof(JumpWallState)? player.AirState : preState);
        player.rb.velocity = new Vector2(0, player.rb.velocity.y);
        base.Exit();
    }
    private void OnDash()
    {
        var dashDir = player.dashDir;
        player.rb.velocity = new Vector2(player.dashForce * dashDir, player.rb.velocity.y);
    }
}
