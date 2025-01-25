using System;
using System.Collections.Generic;
using System.Linq;
public class StateMachine
{
    public StateMachine()
    {
    }
    public BaseState CurrentState { get; private set; }
    public BaseState PreState { get; private set; }
    //private Dictionary<Type, StateNode> nodes = new();
    //private HashSet<Transition> anyTransitions = new();
    public void Update()
    {
        // var transition = GetTransition();
        // if (transition != null)
        // {
        //     ChangeState(transition.To);
        // }
        CurrentState?.Update();
    }
    public void ChangeState(BaseState state)
    {
        
        if(CurrentState == state) return;
        
        var curSate = CurrentState;
        var nextState = state;
        
        PreState = curSate;
        CurrentState = state;
        //CurrentState = nodes[state.GetType()];
        curSate?.Exit();
        nextState?.Enter();
    }
    // public void InjectTransition(IState target, IState source, IPredicate condition)
    // {
    //     GetOrAddNode(target).AddTransition(GetOrAddNode(source).State, condition);
    // }
    // public void AddAnyTransition(IState to, IPredicate condition)
    // {
    //     anyTransitions.Add(new(GetOrAddNode(to).State, condition));
    // }
    // private StateNode GetOrAddNode(IState state)
    // {
    //     var node = nodes.GetValueOrDefault(state.GetType());
    //     if (node == null)
    //     {
    //         node = new StateNode(state);
    //         nodes[state.GetType()] = node;
    //     }
    //     return node;
    // }
    public void SetState(BaseState state)
    {
        //var stateNode = GetOrAddNode(state);
        CurrentState = state;
        CurrentState?.Enter();
    }
    // private ITransition GetTransition()
    // {
    //     foreach(Transition transition in anyTransitions.Where(transition => transition.Condition.Evaluate() && transition.To != CurrentState.State))
    //     {
    //         return transition;
    //     }
    //     return CurrentState.transitions.FirstOrDefault(transition => transition.Condition.Evaluate());
    //
    // }
    public void FixedUpdate()
    {
        CurrentState?.FixedUpdate();
    }
}