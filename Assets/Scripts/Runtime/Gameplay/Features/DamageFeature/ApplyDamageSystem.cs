using Assets.Scripts.Runtime.Gameplay.EntitiesCore;
using Assets.Scripts.Runtime.Gameplay.EntitiesCore.Systems;
using Assets.Scripts.Utilities.Conditions;
using Assets.Scripts.Utilities.Reactive;
using System;

namespace Assets.Scripts.Runtime.Gameplay.Features.DamageFeature
{
    public class ApplyDamageSystem : IInitializableSystem, IDisposableSystem
    {
        private ICompositeCondition _canApplyDamage;
        private ReactiveEvent<float> _damageRequest;
        private ReactiveEvent<float> _damageEvent;
        private ReactiveVariable<float> _health;

        private IDisposable _subscription;

        public void OnInit(Entity entity)
        {
            _canApplyDamage = entity.CanApplyDamage;
            _damageRequest = entity.TakeDamageRequest;
            _damageEvent = entity.TakeDamageEvent;
            _health = entity.CurrentHealth;

            _subscription = _damageRequest.Subscribe(OnDamageRequest);
        }

        public void OnDispose()
        {
            _subscription.Dispose();
        }

        private void OnDamageRequest(float value)
        {
            if (value < 0)
                throw new ArgumentOutOfRangeException("Damage cannot be below zero");

            if (_canApplyDamage.Evaluate() == false)
                return;

            _health.Value = MathF.Max(_health.Value - value, 0);
            _damageEvent.Invoke(value);
        }
    }
}