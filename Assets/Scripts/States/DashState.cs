using UnityEngine;
public class DashState : BaseState
{
    private IState preState;
    public DashState(Player player, StateMachine stateMachine, Animator animator, string animationKey) : base(player, stateMachine, animator, animationKey)
    {
    }
    public override void Enter()
    {
        base.Enter();
        stateTimmer = player.dashTime;
        preState = stateMachine.PreState.State;
        OnDash();
        player.rb.velocity = new Vector2(player.rb.velocity.x, 0);

    }

    public override void Update()
    {
        base.Update();  
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
        stateMachine.ChangeState(preState.GetType() == typeof(JumpState) ? player.AirState : preState);
        player.rb.velocity = new Vector2(0, player.rb.velocity.y);
        base.Exit();
    }
    private void OnDash()
    {
        var dashDir = player.dashDir;
        player.rb.velocity = new Vector2(player.dashForce * dashDir, player.rb.velocity.y);
    }
}