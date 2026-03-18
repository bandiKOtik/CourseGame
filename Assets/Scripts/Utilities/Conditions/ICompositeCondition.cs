namespace Assets.Scripts.Utilities.Conditions
{
    public interface ICompositeCondition : ICondition
    {
        ICompositeCondition Add(ICondition condition);

        ICompositeCondition Remove(ICondition condition);
    }
}