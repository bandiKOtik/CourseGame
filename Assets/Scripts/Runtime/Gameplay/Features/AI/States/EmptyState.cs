using Assets.Scripts.Utilities.StateMachineCore;

namespace Assets.Scripts.Runtime.Gameplay.Features.AI.States
{
    public class EmptyState : State, IUpdateableState
    {
        public void Update(float deltaTime)
        { }
    }
}