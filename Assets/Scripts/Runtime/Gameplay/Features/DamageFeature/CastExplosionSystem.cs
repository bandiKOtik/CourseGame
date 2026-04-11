using Assets.Scripts.Runtime.Gameplay.EntitiesCore;
using Assets.Scripts.Runtime.Gameplay.EntitiesCore.Factory;
using Assets.Scripts.Runtime.Gameplay.EntitiesCore.Systems;
using Assets.Scripts.Utilities.Reactive;
using System;

namespace Assets.Scripts.Runtime.Gameplay.Features.DamageFeature
{
    public class CastExplosionSystem : IInitializableSystem, IUpdateableSystem
    {
        private readonly EntitiesFactory _entitiesFactory;

        private Entity _source;
        private ReactiveEvent _request;

        public CastExplosionSystem(EntitiesFactory entitiesFactory)
        {
            _entitiesFactory = entitiesFactory;
        }

        public void OnInit(Entity entity)
        {
            _source = entity;
            _request = entity.StartAttackRequest;
        }

        public void OnUpdate(float deltaTime)
        {

        }
    }
}
