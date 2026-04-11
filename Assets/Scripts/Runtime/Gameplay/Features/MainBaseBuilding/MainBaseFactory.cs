using Assets.Scripts.Configs.Gameplay.Entities;
using Assets.Scripts.Infrastructure.ConfigsManagement;
using Assets.Scripts.Infrastructure.DI_Container;
using Assets.Scripts.Runtime.Gameplay.EntitiesCore;
using Assets.Scripts.Runtime.Gameplay.EntitiesCore.Factory;
using Assets.Scripts.Runtime.Gameplay.Features.TeamsFeature;
using UnityEngine;

namespace Assets.Scripts.Runtime.Gameplay.Features.MainBaseBuilding
{
    public class MainBaseFactory
    {
        private readonly EntitiesFactory _entitiesFactory;
        private readonly ConfigsProviderService _configProvider;
        private readonly EntitiesLifeContext _context;

        public MainBaseFactory(DIContainer container)
        {
            _entitiesFactory = container.Resolve<EntitiesFactory>();
            _configProvider = container.Resolve<ConfigsProviderService>();
            _context = container.Resolve<EntitiesLifeContext>();
        }

        public Entity Create(Vector3 position)
        {
            var config = _configProvider.GetConfig<DefendableBuildingConfig>();

            Entity entity = _entitiesFactory.CreateMainDefendBuilding(config, position);

            entity
                .AddIsMainBase()
                .AddTeam(new(Teams.MainHero));

            _context.Add(entity);

            return entity;
        }
    }
}
