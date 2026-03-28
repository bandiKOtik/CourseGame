namespace Assets.Scripts.Utilities.StateMachineCore
{
    public interface IUpdateableState : IState
    {
        void Update(float deltaTime);
    }
}