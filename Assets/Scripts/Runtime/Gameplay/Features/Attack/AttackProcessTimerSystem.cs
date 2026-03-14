using Assets.Scripts.Runtime.Gameplay.EntitiesCore;
using Assets.Scripts.Runtime.Gameplay.EntitiesCore.Systems;
using Assets.Scripts.Utilities.Reactive;
using System;

namespace Assets.Scripts.Runtime.Gameplay.Features.Attack
{
    public class AttackProcessTimerSystem : IInitializableSystem, IUpdateableSystem, IDisposableSystem
    {
        private ReactiveVariable<float> _currentTime;
        private ReactiveVariable<bool> _inProcess;
        private ReactiveEvent _startAttackEvent;

        private IDisposable _subscription;

        public void OnInit(Entity entity)
        {
            _currentTime = entity.AttackProcessCurrentTime;
            _inProcess = entity.InAttackProcess;
            _startAttackEvent = entity.StartAttackEvent;

            _subscription = _startAttackEvent.Subscribe(OnStartAttackProcess);
        }

        public void OnDispose()
        {
            _subscription.Dispose();
        }

        public void OnUpdate(float deltaTime)
        {
            if (_inProcess.Value == false)
                return;

            _currentTime.Value += deltaTime;
        }

        private void OnStartAttackProcess()
        {
            _currentTime.Value = 0;
        }
    }
}
