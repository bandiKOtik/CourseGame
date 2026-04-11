using Assets.Scripts.Runtime.Gameplay.EntitiesCore;
using Assets.Scripts.Runtime.Gameplay.EntitiesCore.Factory;
using Assets.Scripts.Runtime.Gameplay.EntitiesCore.Systems;
using Assets.Scripts.Utilities.Reactive;
using System;
using UnityEngine;

namespace Assets.Scripts.Runtime.Gameplay.Features.Attack.AreaAttack
{
    public class AreaAttackSystem : IInitializableSystem, IDisposableSystem
    {
        private EntitiesFactory _factory;

        private Entity _excludedEntity;
        private ReactiveVariable<float> _attackRadius;
        private ReactiveVariable<float> _damage;
        private ReactiveEvent _startAttackEvent;
        private Transform _transform;

        private IDisposable _subscription;

        public AreaAttackSystem(EntitiesFactory factory)
        {
            _factory = factory;
        }

        public void OnInit(Entity entity)
        {
            _excludedEntity = entity;
            _attackRadius = entity.AreaAttackRadius;
            _damage = entity.InstantAttackDamage;
            _startAttackEvent = entity.StartAttackEvent;
            _transform = entity.Transform;

            _subscription = _startAttackEvent.Subscribe(OnStartAttackEvent);
        }

        public void OnDispose()
        {
            _subscription.Dispose();
        }

        private void OnStartAttackEvent()
        {
            _factory.CreateExplosion(_excludedEntity, _transform.position, _damage.Value, _attackRadius.Value);
        }
    }
}