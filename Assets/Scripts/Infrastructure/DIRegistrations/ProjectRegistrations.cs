using Assets.Scripts.Infrastructure.ConfigsManagement;
using Assets.Scripts.Infrastructure.DI_Container;
using Assets.Scripts.Meta.Wallet;
using Assets.Scripts.Utilities.AssetsManagement;
using Assets.Scripts.Utilities.CoroutinesManagement;
using Assets.Scripts.Utilities.LoadingScreen;
using Assets.Scripts.Utilities.Reactive;
using Assets.Scripts.Utilities.SceneManagement;
using System;
using System.Collections.Generic;
using UnityEngine.Windows.Speech;
using Object = UnityEngine.Object;

namespace Assets.Scripts.Utilities.Factory
{
    public class ProjectRegistrations
    {
        public void Register(DIContainer container)
        {
            container.RegisterAsSingle<ICoroutinesPerformer>(CreateCoroutinesPerformer);

            container.RegisterAsSingle<ILoadingScreen>(CreateLoadingScreenHandler);

            container.RegisterAsSingle(CreateConfigsProviderService);

            container.RegisterAsSingle(c => new ResourcesAssetsLoader());

            container.RegisterAsSingle(CreateSceneLoaderService);

            container.RegisterAsSingle(CreateSceneSwitcherService);

            container.RegisterAsSingle(CreateWalletService);
        }

        private WalletService CreateWalletService(DIContainer c)
        {
            Dictionary<CurrencyTypes, ReactiveVariable<int>> currencies = new();

            foreach (var type in Enum.GetValues(typeof(CurrencyTypes)))
                currencies.Add((CurrencyTypes)type, new ReactiveVariable<int>());

            return new WalletService(currencies);
        }

        private SceneLoaderService CreateSceneLoaderService(DIContainer c)
            => new SceneLoaderService();

        private ConfigsProviderService CreateConfigsProviderService(DIContainer c)
        {
            ResourcesAssetsLoader resourcesAssetsLoader = c.Resolve<ResourcesAssetsLoader>();

            ResourcesConfigsLoader resourcesConfigsLoader = new ResourcesConfigsLoader(resourcesAssetsLoader);

            return new ConfigsProviderService(resourcesConfigsLoader);
        }

        private CoroutinesPerformer CreateCoroutinesPerformer(DIContainer c)
        {
            ResourcesAssetsLoader resourcesAssetsLoader = c.Resolve<ResourcesAssetsLoader>();

            CoroutinesPerformer coroutinesPerformer = resourcesAssetsLoader
                .Load<CoroutinesPerformer>("Utilities/CoroutinePerformer");

            return Object.Instantiate(coroutinesPerformer);
        }

        private LoadingScreenHandler CreateLoadingScreenHandler(DIContainer c)
        {
            ResourcesAssetsLoader resourcesAssetsLoader = c.Resolve<ResourcesAssetsLoader>();

            LoadingScreenHandler loadingScreenHandler = resourcesAssetsLoader
                .Load<LoadingScreenHandler>("Utilities/LoadingScreenCanvas");

            return Object.Instantiate(loadingScreenHandler);
        }

        private SceneSwitcherService CreateSceneSwitcherService(DIContainer c)
            => new SceneSwitcherService(
                c,
                c.Resolve<SceneLoaderService>(),
                c.Resolve<ILoadingScreen>());
    }
}