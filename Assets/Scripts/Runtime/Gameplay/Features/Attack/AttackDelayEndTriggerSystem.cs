using Assets.Scripts.Runtime.Gameplay.EntitiesCore;
using Assets.Scripts.Runtime.Gameplay.EntitiesCore.Systems;
using Assets.Scripts.Utilities.Reactive;
using System;

namespace Assets.Scripts.Runtime.Gameplay.Features.Attack
{
    public class AttackDelayEndTriggerSystem : IInitializableSystem, IDisposableSystem
    {
        private ReactiveEvent _attackDelayEndEvent;
        private ReactiveEvent _startAttack;
        private ReactiveVariable<float> _delay;
        private ReactiveVariable<float> _currentTime;
        private bool _attacked;

        private IDisposable _attackTimerSub;
        private IDisposable _startAttackSub;

        public void OnInit(Entity entity)
        {
            _attackDelayEndEvent = entity.AttackDelayEndEvent;
            _startAttack = entity.StartAttackEvent;
            _delay = entity.AttackDelayTime;
            _currentTime = entity.AttackProcessCurrentTime;

            _attackTimerSub = _currentTime.Subscribe(OnAttackTimerChanged);
            _startAttackSub = _startAttack.Subscribe(OnStartAttack);
        }

        public void OnDispose()
        {
            _attackTimerSub.Dispose();
            _startAttackSub.Dispose();
        }

        private void OnAttackTimerChanged(float oldValue, float newValue)
        {
            if (_attacked)
                return;

            if (newValue >= _delay.Value)
            {
                UnityEngine.Debug.Log("Delay end");
                _attackDelayEndEvent.Invoke();
                _attacked = true;
            }
        }

        private void OnStartAttack() => _attacked = false;
    }
}
