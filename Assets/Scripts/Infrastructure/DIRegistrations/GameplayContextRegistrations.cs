using Assets.Scripts.Configs.Gameplay.Levels;
using Assets.Scripts.Infrastructure.ConfigsManagement;
using Assets.Scripts.Infrastructure.DI_Container;
using Assets.Scripts.Infrastructure.Gameplay;
using Assets.Scripts.Runtime.Gameplay;
using Assets.Scripts.Runtime.Gameplay.EntitiesCore;
using Assets.Scripts.Runtime.Gameplay.EntitiesCore.Factory;
using Assets.Scripts.Runtime.Gameplay.EntitiesCore.Mono;
using Assets.Scripts.Runtime.Gameplay.Features.AI;
using Assets.Scripts.Runtime.Gameplay.Features.Enemies;
using Assets.Scripts.Runtime.Gameplay.Features.InputManagement;
using Assets.Scripts.Runtime.Gameplay.Features.MainBaseBuilding;
using Assets.Scripts.Runtime.Gameplay.Features.MainHero;
using Assets.Scripts.Runtime.Gameplay.Features.StagesFeature;
using Assets.Scripts.Runtime.Gameplay.States;
using Assets.Scripts.Runtime.UI.Gameplay;
using Assets.Scripts.Utilities.AssetsManagement;
using Assets.Scripts.Utilities.CoroutinesManagement;
using Assets.Scripts.Utilities.DataManagement.DataProviders;
using Assets.Scripts.Utilities.SceneManagement;
using UnityEngine;

namespace Assets.Scripts.Infrastructure.DIRegistrations
{
    public class GameplayContextRegistrations
    {
        private GameplayInputArgs _args;

        public void Process(DIContainer container, GameplayInputArgs args)
        {
            _args = args;

            container.RegisterAsSingle(c => new EntitiesFactory(c));

            container.RegisterAsSingle(c => new EntitiesLifeContext());

            container.RegisterAsSingle(c => new MainHeroFactory(c));

            container.RegisterAsSingle(c => new MainBaseFactory(c));

            container.RegisterAsSingle(CreateMainHeroHolderService).NonLazy();

            container.RegisterAsSingle(c => new EnemiesFactory(c));

            container.RegisterAsSingle(c => new BrainsFactory(c));

            container.RegisterAsSingle(c => new StagesFactory(c));

            container.RegisterAsSingle(c => new GameplayStatesFactory(c));

            container.RegisterAsSingle(CreateGameplayStatesContext);

            container.RegisterAsSingle(CreatePreperationTriggerService);

            container.RegisterAsSingle(CreateStageProviderService);

            container.RegisterAsSingle(c => new AIBrainsContext());

            container.RegisterAsSingle<IInputService>(c => new DesktopInput());

            container.RegisterAsSingle(CreateMonoEntitiesFactory).NonLazy();

            container.RegisterAsSingle(CreateGameSession);

            container.RegisterAsSingle(GameplayUIRoot).NonLazy();

            container.RegisterAsSingle(c => new CollidersRegistryService());
        }

        private MainHeroHolderService CreateMainHeroHolderService(DIContainer c)
        {
            return new(c.Resolve<EntitiesLifeContext>());
        }

        private GameplayStatesContext CreateGameplayStatesContext(DIContainer c)
        {
            return new(c
                .Resolve<GameplayStatesFactory>()
                .CreateGameplayStateMachine(_args));
        }

        private PreperationTriggerService CreatePreperationTriggerService(DIContainer c)
        {
            return new(
                c.Resolve<EntitiesFactory>(),
                c.Resolve<EntitiesLifeContext>());
        }

        private StageProviderService CreateStageProviderService(DIContainer c)
        {
            return new(
                c.Resolve<StagesFactory>(),
                c.Resolve<ConfigsProviderService>()
                    .GetConfig<LevelsListConfig>()
                    .GetLevelByNumber(_args.LevelNumber));
        }

        private MonoEntitiesFactory CreateMonoEntitiesFactory(DIContainer c)
        {
            return new(
                c.Resolve<CollidersRegistryService>(),
                c.Resolve<ResourcesAssetsLoader>(),
                c.Resolve<EntitiesLifeContext>());
        }

        private GameSession CreateGameSession(DIContainer c)
        {
            return new(
                c.Resolve<ICoroutinesPerformer>(),
                c.Resolve<SceneSwitcherService>(),
                c.Resolve<PlayerDataProvider>());
        }

        private GameplayUIRoot GameplayUIRoot(DIContainer c)
        {
            ResourcesAssetsLoader resourcesAssetsLoader = c.Resolve<ResourcesAssetsLoader>();

            GameplayUIRoot rootPrefab = resourcesAssetsLoader
                .Load<GameplayUIRoot>("UI/Gameplay/GameplayUIRoot");

            return Object.Instantiate(rootPrefab);
        }
    }
}