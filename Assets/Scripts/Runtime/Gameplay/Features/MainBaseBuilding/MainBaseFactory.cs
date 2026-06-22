using Assets.Scripts.Configs.Gameplay;
using Assets.Scripts.Configs.Gameplay.Abilities;
using Assets.Scripts.Configs.Gameplay.Entities;
using Assets.Scripts.Configs.Gameplay.Levels;
using Assets.Scripts.Infrastructure.ConfigsManagement;
using Assets.Scripts.Infrastructure.DI_Container;
using Assets.Scripts.Infrastructure.Gameplay;
using Assets.Scripts.Runtime.Gameplay.EntitiesCore;
using Assets.Scripts.Runtime.Gameplay.EntitiesCore.Factory;
using Assets.Scripts.Runtime.Gameplay.Features.AbilitiesFeature;
using Assets.Scripts.Runtime.Gameplay.Features.LevelUpFeature;
using Assets.Scripts.Runtime.Gameplay.Features.TeamsFeature;
using UnityEngine;

namespace Assets.Scripts.Runtime.Gameplay.Features.MainBaseBuilding
{
    public class MainBaseFactory
    {
        private readonly DIContainer _container;
        private readonly EntitiesFactory _entitiesFactory;
        private readonly ConfigsProviderService _configProvider;
        private readonly EntitiesLifeContext _context;

        public MainBaseFactory(DIContainer container)
        {
            _container = container;
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

            AbilitiesFactory factory = _container.Resolve<AbilitiesFactory>();
            AbilitiesList list = new();

            entity
                .AddAbilities(list)
                .AddSystem(new AbilityOnAddActivationSystem());

            entity
                .AddLevel(new(1))
                .AddExperience(new(0))
                .AddSystem(new LevelUpSystem(_configProvider
                .GetConfig<ExperienceForUpgradeLevelConfig>()));

            list.Add(factory
                .CreateAbilityFor(entity, _configProvider
                .GetConfig<AbilitiesConfigsContainer>().AbilityConfigs[0]));

            list.Add(factory
                .CreateAbilityFor(entity, _configProvider
                .GetConfig<AbilitiesConfigsContainer>().AbilityConfigs[1]));

            _context.Add(entity);

            return entity;
        }
    }
}