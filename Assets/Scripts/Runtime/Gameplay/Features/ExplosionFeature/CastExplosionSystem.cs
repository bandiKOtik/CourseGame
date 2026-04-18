using Assets.Scripts.Runtime.Gameplay.EntitiesCore;
using Assets.Scripts.Runtime.Gameplay.EntitiesCore.Factory;
using Assets.Scripts.Runtime.Gameplay.EntitiesCore.Systems;
using Assets.Scripts.Runtime.Gameplay.Features.TeamsFeature;
using Assets.Scripts.Utilities.Reactive;
using System;
using UnityEngine;

namespace Assets.Scripts.Runtime.Gameplay.Features.ExplosionFeature
{
    public class CastExplosionSystem : IInitializableSystem, IDisposableSystem
    {
        private readonly EntitiesFactory _entitiesFactory;

        private ReactiveVariable<Teams> _sourceTeam;
        private ReactiveEvent _request;
        private ReactiveVariable<Vector3> _position;

        private IDisposable _subscription;

        public CastExplosionSystem(EntitiesFactory entitiesFactory)
        {
            _entitiesFactory = entitiesFactory;
        }

        public void OnInit(Entity entity)
        {
            _sourceTeam = entity.Team;
            _request = entity.StartAttackRequest;
            _position = entity.ExplosionPosition;

            _subscription = _request.Subscribe(OnAttackRequest);
        }

        public void OnDispose() => _subscription.Dispose();

        private void OnAttackRequest()
        {
            _entitiesFactory.CreateExplosion(_sourceTeam, _position.Value, 1, 2);
        }
    }
}
