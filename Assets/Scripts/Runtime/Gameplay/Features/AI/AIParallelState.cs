using Assets.Scripts.Utilities.StateMachineCore;

namespace Assets.Scripts.Runtime.Gameplay.Features.AI
{
    public class AIParallelState : ParallelState<IUpdateableState>, IUpdateableState
    {
        public AIParallelState(params IUpdateableState[] states) : base(states)
        {
        }

        public void Update(float deltaTime)
        {
            foreach (var state in States)
                state.Update(deltaTime);
        }
    }
}