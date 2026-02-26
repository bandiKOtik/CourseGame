using Assets.Scripts.Infrastructure.DI_Container;
using Assets.Scripts.Infrastructure.Gameplay;
using Assets.Scripts.Runtime.Gameplay;
using Assets.Scripts.Runtime.InputManagement;
using Assets.Scripts.Runtime.SequenceGeneration;
using Assets.Scripts.Runtime.UI.Core;
using Assets.Scripts.Runtime.UI.Gameplay;
using Assets.Scripts.Runtime.UI.MainMenu;
using Assets.Scripts.Utilities.AssetsManagement;
using Assets.Scripts.Utilities.CoroutinesManagement;
using Assets.Scripts.Utilities.DataManagement.DataProviders;
using Assets.Scripts.Utilities.Factory;
using Assets.Scripts.Utilities.Factory.UI;
using Assets.Scripts.Utilities.InputManagement;
using Assets.Scripts.Utilities.SceneManagement;
using Assets.Scripts.Utilities.Sequence;
using UnityEngine;

namespace Assets.Scripts.Infrastructure.DIRegistrations
{
    public class GameplayContextRegistrations
    {
        private GameplayInputArgs _args;

        public void Process(DIContainer container, GameplayInputArgs args)
        {
            _args = args;

            container.RegisterAsSingle(CreateSequenceGeneratorFactory);

            container.RegisterAsSingle<ISequenceGenerator>(CreateSequenceGenerator);

            container.RegisterAsSingle<IInputHandler>(CreateGameplayInputHandler);

            container.RegisterAsSingle(CreateSequenceMatcher);

            container.RegisterAsSingle(c => new GameplayPresentersFactory(c));

            container.RegisterAsSingle(CreateGameSession);

            container.RegisterAsSingle(GameplayUIRoot).NonLazy();

            container.RegisterAsSingle(CreateGameplayScreenPresenter).NonLazy();
        }

        private SequenceGeneratorFactory CreateSequenceGeneratorFactory(DIContainer c) => new(c);

        private SequenceGenerator CreateSequenceGenerator(DIContainer c)
        {
            return c
                .Resolve<SequenceGeneratorFactory>()
                .CreateSequenceGenerator(_args.CurrentGamemode);
        }

        private InputStringHandler CreateGameplayInputHandler(DIContainer c) => new();

        private SequenceMatcher CreateSequenceMatcher(DIContainer c) => new();

        private GameSession CreateGameSession(DIContainer c)
        {
            return new(
                c.Resolve<ICoroutinesPerformer>(),
                c.Resolve<ISequenceGenerator>(),
                c.Resolve<SceneSwitcherService>(),
                c.Resolve<SequenceMatcher>(),
                c.Resolve<IInputHandler>(),
                c.Resolve<PlayerDataProvider>());
        }

        private GameplayUIRoot GameplayUIRoot(DIContainer c)
        {
            ResourcesAssetsLoader resourcesAssetsLoader = c.Resolve<ResourcesAssetsLoader>();

            GameplayUIRoot rootPrefab = resourcesAssetsLoader
                .Load<GameplayUIRoot>("UI/Gameplay/GameplayUIRoot");

            return Object.Instantiate(rootPrefab);
        }

        private GameplayScreenPresenter CreateGameplayScreenPresenter(DIContainer c)
        {
            GameplayUIRoot uiRoot = c.Resolve<GameplayUIRoot>();

            GameplayScreenView view = c.Resolve<ViewsFactory>()
                .Create<GameplayScreenView>(ViewIDs.GameplayScreen, uiRoot.HUDLayer);

            GameplayScreenPresenter presenter = c.Resolve<GameplayPresentersFactory>()
                .CreateGameplayScreenPresenter(view);

            return presenter;
        }
    }
}