using Assets.Scripts.Runtime.Gameplay.EntitiesCore;
using Assets.Scripts.Runtime.Gameplay.EntitiesCore.Systems;
using Assets.Scripts.Utilities.Reactive;
using System;

namespace Assets.Scripts.Runtime.Gameplay.Features.Attack
{
    public class SelfDestroyAfterAttackSystem : IInitializableSystem, IDisposableSystem
    {
        private ReactiveEvent _attackRequest;
        private ReactiveVariable<bool> _mustDestroy;

        private IDisposable _subscription;

        public void OnInit(Entity entity)
        {
            _attackRequest = entity.StartAttackRequest;
            _mustDestroy = entity.MustSelfDestroy;

            _subscription = _attackRequest.Subscribe(OnAttackRequested);
        }

        public void OnDispose() => _subscription?.Dispose();

        private void OnAttackRequested() => _mustDestroy.Value = true;
    }
}