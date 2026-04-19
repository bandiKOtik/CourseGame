using System;

namespace Assets.Scripts.Runtime.Gameplay.States
{
    public class GameplayStatesContext : IDisposable
    {
        private GameplayStateMachine _stateMachine;

        private bool _isRunning;

        public GameplayStatesContext(GameplayStateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }

        public void Run()
        {
            _stateMachine.Enter();
            _isRunning = true;
        }

        public void Update(float deltaTime)
        {
            if (_isRunning == false)
                return;

            _stateMachine.Update(deltaTime);
        }

        public void Dispose()
        {
            _isRunning = false;
            _stateMachine.Dispose();
        }
    }
}
