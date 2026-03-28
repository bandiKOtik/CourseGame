using Assets.Scripts.Runtime.Gameplay.EntitiesCore;
using Assets.Scripts.Runtime.Gameplay.EntitiesCore.Systems;
using Assets.Scripts.Utilities.Reactive;
using System;

namespace Assets.Scripts.Runtime.Gameplay.Features.Attack
{
    public class AttackCooldownTimerSystem : IInitializableSystem, IDisposableSystem, IUpdateableSystem
    {
        private ReactiveVariable<float> _currentTime;
        private ReactiveVariable<float> _initialTime;
        private ReactiveVariable<bool> _inCooldown;
        private ReactiveEvent _endAttackEvent;

        private IDisposable _subscription;

        public void OnInit(Entity entity)
        {
            _currentTime = entity.AttackCooldownCurrentTime;
            _initialTime = entity.AttackCooldownInitialTime;
            _inCooldown = entity.InAttackCooldown;
            _endAttackEvent = entity.EndAttackEvent;
            _subscription = _endAttackEvent.Subscribe(OnEndAttack);
        }

        public void OnDispose()
        {
            _subscription.Dispose();
        }

        public void OnUpdate(float deltaTime)
        {
            if (_inCooldown.Value == false)
                return;

            _currentTime.Value -= deltaTime;

            if (CooldownIsOver())
            {
                _inCooldown.Value = false;
            }
        }

        private void OnEndAttack()
        {
            _currentTime.Value = _initialTime.Value;
            _inCooldown.Value = true;
        }

        private bool CooldownIsOver() => _currentTime.Value <= 0;
    }
}