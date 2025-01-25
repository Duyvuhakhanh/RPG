using System.Collections.Generic;
public class StateNode 
{
    public IState State { get; }
    public HashSet<ITransition> transitions { get; }
    public StateNode(IState state)
    {
        this.State = state;
        transitions = new HashSet<ITransition>();
    }
    public void AddTransition(IState to, IPredicate condition)
    {
        transitions.Add(new Transition(to, condition));
    }

}
