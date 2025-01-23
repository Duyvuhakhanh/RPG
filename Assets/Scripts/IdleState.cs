using UnityEngine;
public class IdleState : BaseState
{
    public IdleState(Player player, StateMachine stateMachine) : base(player, stateMachine)
    {
    }
    public override void Enter()
    {
        Debug.Log("IdleState Enter");
    }
    public override void Exit()
    {
        Debug.Log("IdleState Exit");
    }
    public override void Update()
    {
        Debug.Log("IdleState Update");
    }
}
