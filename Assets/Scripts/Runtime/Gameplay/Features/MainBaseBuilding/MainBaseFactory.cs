using Assets.Scripts.Configs.Gameplay.Entities;
using Assets.Scripts.Configs.Gameplay.Levels;
using Assets.Scripts.Infrastructure.ConfigsManagement;
using Assets.Scripts.Infrastructure.DI_Container;
using Assets.Scripts.Infrastructure.Gameplay;
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

        public Entity Create(GameplayInputArgs args, Vector3 position)
        {
            var baseConfig = _configProvider.GetConfig<DefendableBuildingConfig>();
            var levelConfig = _configProvider
                .GetConfig<LevelsListConfig>()
                .GetLevelByNumber(args.LevelNumber);

            Entity entity = _entitiesFactory.CreateMainDefendBuilding(baseConfig, levelConfig, position);

            entity
                .AddIsMainBase()
                .AddTeam(new(Teams.MainHero));

            _context.Add(entity);

            return entity;
        }
    }
}