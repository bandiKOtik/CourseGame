using Assets.Scripts.Runtime.Gameplay.EntitiesCore;
using Assets.Scripts.Runtime.Gameplay.EntitiesCore.Factory;
using Assets.Scripts.Runtime.Gameplay.EntitiesCore.Systems;
using Assets.Scripts.Utilities.Reactive;
using System;
using UnityEngine;
using static UnityEngine.UI.GridLayoutGroup;

namespace Assets.Scripts.Runtime.Gameplay.Features.Attack.Shoot
{
    public class InstantShootSystem : IInitializableSystem, IDisposableSystem
    {
        private readonly EntitiesFactory _factory;

        private Entity _owner;
        private ReactiveEvent _attackDelayEndEvent;
        private ReactiveVariable<float> _damage;
        private Transform _shootPoint;

        private IDisposable _subscription;

        public InstantShootSystem(EntitiesFactory factory)
        {
            _factory = factory;
        }

        public void OnInit(Entity entity)
        {
            _owner = entity;
            _attackDelayEndEvent = entity.AttackDelayEndEvent;
            _damage = entity.InstantAttackDamage;
            _shootPoint = entity.ShootPoint;
            _subscription = _attackDelayEndEvent.Subscribe(OnDelayEnd);
        }

        public void OnDispose()
        {
            _subscription.Dispose();
        }

        private void OnDelayEnd()
        {
            _factory.CreateBullet(_owner, _shootPoint.position, _shootPoint.forward, _damage.Value);
        }
    }
}