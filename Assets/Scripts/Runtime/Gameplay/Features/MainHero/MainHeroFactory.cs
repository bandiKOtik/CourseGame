using Assets.Scripts.Configs.Gameplay.Entities;
using Assets.Scripts.Infrastructure.ConfigsManagement;
using Assets.Scripts.Infrastructure.DI_Container;
using Assets.Scripts.Runtime.Gameplay.EntitiesCore;
using Assets.Scripts.Runtime.Gameplay.EntitiesCore.Factory;
using Assets.Scripts.Runtime.Gameplay.Features.AI;
using Assets.Scripts.Runtime.Gameplay.Features.AI.States;
using Assets.Scripts.Runtime.Gameplay.Features.TeamsFeature;
using UnityEngine;

namespace Assets.Scripts.Runtime.Gameplay.Features.MainHero
{
    public class MainHeroFactory
    {
        private readonly DIContainer _container;

        private readonly EntitiesFactory _entitiesFactory;
        private readonly BrainsFactory _brainsFactory;
        private readonly ConfigsProviderService _configProvider;
        private readonly EntitiesLifeContext _context;

        public MainHeroFactory(DIContainer container)
        {
            _container = container;
            _entitiesFactory = container.Resolve<EntitiesFactory>();
            _brainsFactory = container.Resolve<BrainsFactory>();
            _configProvider = container.Resolve<ConfigsProviderService>();
            _context = container.Resolve<EntitiesLifeContext>();
        }

        public Entity Create(Vector3 position)
        {
            var config = _configProvider.GetConfig<HeroConfig>();

            Entity entity = _entitiesFactory.CreateHero(position, config);

            entity
                .AddIsMainHero()
                .AddTeam(new(Teams.MainHero));

            entity.AddCurrentTarget();

            ITargetSelector targetSelector = new NearestDamageableTargetSelector(entity);
            _brainsFactory.CreateAutoAttackWhenStandBrain(entity, targetSelector);
            // OR
            //_brainsFactory.CreateManualShooterBrain(entity);

            _context.Add(entity);

            return entity;
        }
    }
}
