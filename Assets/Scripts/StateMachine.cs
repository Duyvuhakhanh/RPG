using System;
using System.Collections.Generic;
using System.Linq;
public class StateMachine
{
    public StateMachine()
    {
    }
    public StateNode CurrentState { get; private set; }
    private Dictionary<Type, StateNode> nodes = new();
    private HashSet<Transition> anyTransitions = new();
    public void Update()
    {
        var transition = GetTransition();
        if (transition != null)
        {
            ChangeState(transition.To);
        }
    }
    private void ChangeState(IState state)
    {
        if(CurrentState.State == state) return;

        var curSate = CurrentState;
        var nextState = nodes[state.GetType()].State;
        
        CurrentState = nodes[state.GetType()];
        curSate?.State.Exit();
        nextState?.Enter();
    }
    public void AddTransition(IState from, IState to, IPredicate condition)
    {
        GetOrAddNode(from).AddTransition(GetOrAddNode(to).State, condition);
    }
    public void AddAnyTransition(IState to, IPredicate condition)
    {
        anyTransitions.Add(new(to, condition));
    }
    private StateNode GetOrAddNode(IState state)
    {
        var node = nodes.GetValueOrDefault(state.GetType());
        if (node == null)
        {
            node = new StateNode(state);
            nodes[state.GetType()] = node;
        }
        return node;
    }
    public void SetState(IState state)
    {
        CurrentState = nodes[state.GetType()];
        CurrentState?.State.Enter();
    }
    private ITransition GetTransition()
    {
        foreach(Transition transition in anyTransitions.Where(transition => transition.Condition.Evaluate()))
        {
            return transition;
        }
        return CurrentState.transitions.FirstOrDefault(transition => transition.Condition.Evaluate());

    }
}