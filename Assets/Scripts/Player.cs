using UnityEngine;
public class Player : MonoBehaviour
{
    private StateMachine stateMachine;
    private float timmer;
    private void Start()
    {
        stateMachine = new StateMachine();
        var idleState = new IdleState(this, stateMachine);
        var moveState = new MoveState(this, stateMachine);
        stateMachine.AddTransition(idleState, moveState, new FuncPredicate( ()=> timmer > 3));
        stateMachine.AddAnyTransition(idleState, new FuncPredicate( () => false));
        stateMachine.SetState(idleState);
    }
    private void Update()
    {
        timmer += Time.deltaTime;
        stateMachine.Update();
    }
    
}