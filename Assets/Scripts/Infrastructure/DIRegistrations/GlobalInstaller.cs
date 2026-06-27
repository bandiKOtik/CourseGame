using Assets.Scripts.Configs.Meta.Wallet;
using Assets.Scripts.Infrastructure.ConfigsManagement;
using Assets.Scripts.Meta;
using Assets.Scripts.Meta.Features.LevelsProgression;
using Assets.Scripts.Meta.Features.Wallet;
using Assets.Scripts.Meta.Statistics;
using Assets.Scripts.Utilities.AssetsManagement;
using Assets.Scripts.Utilities.DataManagement;
using Assets.Scripts.Utilities.DataManagement.DataProviders;
using Assets.Scripts.Utilities.DataManagement.DataRepository;
using Assets.Scripts.Utilities.DataManagement.KeysStorage;
using Assets.Scripts.Utilities.DataManagement.Serializers;
using Assets.Scripts.Utilities.Factory.UI;
using Assets.Scripts.Utilities.LoadingScreen;
using Assets.Scripts.Utilities.Reactive;
using Assets.Scripts.Utilities.SaveScreen;
using Assets.Scripts.Utilities.SceneManagement;
using Assets.Scripts.Utilities.Timer;
using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Infrastructure.DIRegistrations
{
    public class GlobalInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Debug.Log("Global installation...");

            // Utilities
            Container.Bind<TimerServiceFactory>().AsSingle();

            // Save system
            Container.Bind<PlayerDataProvider>().AsSingle();
            Container.Bind<StatisticManageService>().AsSingle();

            string saveFolderPath = Application.isEditor ? Application.dataPath : Application.persistentDataPath;
            IDataRepository dataRepository = new LocalFileDataRepository(saveFolderPath, "json");

            Container
                .Bind<ISaveLoadService>()
                .To<SaveLoadService>()
                .AsSingle()
                .WithArguments(new JsonSerializer(), new MapDataKeyStorage(), dataRepository);

            Container
                .Bind<ILoadingScreen>()
                .To<LoadingScreenHandler>()
                .FromResource("Utilities/LoadingScreenCanvas")
                .AsSingle();

            Container
                .Bind<ISaveScreen>()
                .To<SaveScreenHandler>()
                .FromResource("Utilities/SaveScreenCanvas")
                .AsSingle();

            // Configs
            Container.Bind<ResourcesAssetsLoader>().AsSingle();
            Container.Bind<IConfigsLoader>().To<ResourcesConfigsLoader>().AsSingle();

            Container
                .Bind<ConfigsProviderService>()
                .AsSingle();

            //Scene management
            Container.Bind<SceneLoaderService>().AsSingle();
            Container.Bind<SceneSwitcherService>().AsSingle();

            // Meta
            Container
                .Bind<WalletService>()
                .FromMethod(RegisterWalletService)
                .AsSingle()
                .NonLazy();

            Container
                .Bind<PlayedGamesStatistic>()
                .FromMethod(RegisterPlayedGamesStatistic)
                .AsSingle()
                .NonLazy();

            Container.Bind<LevelsProgressionService>().AsSingle().NonLazy();

            // Factory
            Container.Bind<ProjectPresentersFactory>().AsSingle();
            Container.Bind<ViewsFactory>().AsSingle();

            Debug.Log("Global installation complete!");
        }

        private ConfigsProviderService RegisterConfigsProviderService()
        {
            var loader = Container.Resolve<ResourcesConfigsLoader>();
            var provider = new ConfigsProviderService(loader);
            var config = provider.GetConfig<GamePriceConfig>();
            Debug.LogError(config.name);

            return provider;
        }

        private WalletService RegisterWalletService()
        {
            Dictionary<CurrencyTypes, ReactiveVariable<int>> currencies = new();
            foreach (var type in Enum.GetValues(typeof(CurrencyTypes)))
                currencies.Add((CurrencyTypes)type, new ReactiveVariable<int>());

            return new(currencies, Container.Resolve<PlayerDataProvider>());
        }

        private PlayedGamesStatistic RegisterPlayedGamesStatistic()
        {
            Dictionary<GameStatType, int> statistics = new();
            foreach (var type in Enum.GetValues(typeof(GameStatType)))
                statistics.Add((GameStatType)type, default);

            return new(statistics, Container.Resolve<PlayerDataProvider>());
        }
    }
}
