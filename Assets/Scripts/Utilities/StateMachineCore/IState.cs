using Assets.Scripts.Utilities.Reactive;

namespace Assets.Scripts.Utilities.StateMachineCore
{
    public interface IState
    {
        IReadOnlyEvent Entered { get; }
        IReadOnlyEvent Exited { get; }

        void Enter();

        void Exit();
    }
}