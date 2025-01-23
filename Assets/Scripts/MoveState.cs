using UnityEngine;
public class MoveState : BaseState
{
    public MoveState(Player player, StateMachine stateMachine) : base(player, stateMachine)
    {
    }
    public override void Enter()
    {
        Debug.Log("MoveState Enter");
    }
    public override void Exit()
    {
        Debug.Log("MoveState Exit");
    }
    public override void Update()
    {
        Debug.Log("MoveState Update");
    }
}
