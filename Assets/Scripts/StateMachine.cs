using System;
using System.Collections.Generic;
using System.Linq;
public class StateMachine
{
    public StateMachine()
    {
    }
    public StateNode CurrentState { get; private set; }
    public StateNode PreState { get; private set; }
    private Dictionary<Type, StateNode> nodes = new();
    private HashSet<Transition> anyTransitions = new();
    public void Update()
    {
        // var transition = GetTransition();
        // if (transition != null)
        // {
        //     ChangeState(transition.To);
        // }
        CurrentState?.State.Update();
    }
    public void ChangeState(IState state)
    {
        if(CurrentState.State == state) return;
        
        var curSate = CurrentState;
        var nextState = GetOrAddNode(state).State;
        
        PreState = curSate;
        CurrentState = nodes[state.GetType()];
        curSate?.State.Exit();
        nextState?.Enter();
    }
    public void InjectTransition(IState target, IState source, IPredicate condition)
    {
        GetOrAddNode(target).AddTransition(GetOrAddNode(source).State, condition);
    }
    public void AddAnyTransition(IState to, IPredicate condition)
    {
        anyTransitions.Add(new(GetOrAddNode(to).State, condition));
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
        var stateNode = GetOrAddNode(state);
        CurrentState = stateNode;
        CurrentState?.State.Enter();
    }
    private ITransition GetTransition()
    {
        foreach(Transition transition in anyTransitions.Where(transition => transition.Condition.Evaluate() && transition.To != CurrentState.State))
        {
            return transition;
        }
        return CurrentState.transitions.FirstOrDefault(transition => transition.Condition.Evaluate());

    }
    public void FixedUpdate()
    {
        CurrentState?.State.FixedUpdate();
    }
}