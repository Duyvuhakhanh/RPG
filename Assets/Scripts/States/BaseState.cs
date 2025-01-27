using UnityEngine;
public class BaseState : IState
{
    protected Animator animator;
    protected int hashKey;
    protected PlayerStateMachine PlayerStateMachine;
    protected float stateTimmer;
    protected bool triggerCalled;
    public virtual void Enter()
    {
        animator.SetBool(hashKey, true);
        triggerCalled = false;
    }
    public virtual void Exit()
    {
        animator.SetBool(hashKey, false);
    }
    public virtual void Update()
    {
        stateTimmer -= Time.deltaTime;

    }
    public virtual void FixedUpdate()
    {
    }
    public virtual void AnimationTrigger()
    {
        triggerCalled = true;
    }
}
