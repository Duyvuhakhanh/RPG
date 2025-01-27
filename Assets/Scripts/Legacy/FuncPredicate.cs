public class FuncPredicate : IPredicate
{
    private readonly System.Func<bool> predicate;
    public FuncPredicate(System.Func<bool> predicate)
    {
        this.predicate = predicate;
    }
    public bool Evaluate()
    {
        return predicate();
    }
}
