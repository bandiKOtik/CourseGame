namespace Assets.Scripts.Runtime.Gameplay.Features.AI
{
    public class StateMachineBrain : IBrain
    {
        private AIStateMachine _stateMachine;
        private bool _enabled;

        public StateMachineBrain(AIStateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }

        public void Enable()
        {
            _stateMachine.Enter();
            _enabled = true;
        }

        public void Disable()
        {
            _stateMachine.Exit();
            _enabled = false;
        }

        public void Update(float deltaTime)
        {
            if (_enabled == false)
                return;

            _stateMachine.Update(deltaTime);
        }

        public void Dispose()
        {
            _stateMachine.Dispose();
            _enabled = false;
        }
    }
}