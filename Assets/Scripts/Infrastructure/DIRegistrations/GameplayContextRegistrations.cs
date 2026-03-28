using Assets.Scripts.Infrastructure.DI_Container;
using Assets.Scripts.Infrastructure.Gameplay;
using Assets.Scripts.Runtime.Gameplay;
using Assets.Scripts.Runtime.Gameplay.EntitiesCore;
using Assets.Scripts.Runtime.Gameplay.EntitiesCore.Mono;
using Assets.Scripts.Runtime.Gameplay.Features.AI;
using Assets.Scripts.Runtime.Gameplay.Features.InputManagement;
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

            container.RegisterAsSingle(c => new BrainsFactory(c));

            container.RegisterAsSingle(c => new AIBrainsContext());

            container.RegisterAsSingle<IInputService>(c => new DesktopInput());

            container.RegisterAsSingle(CreateMonoEntitiesFactory).NonLazy();

            container.RegisterAsSingle(CreateGameSession);

            container.RegisterAsSingle(GameplayUIRoot).NonLazy();

            container.RegisterAsSingle(c => new CollidersRegistryService());
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