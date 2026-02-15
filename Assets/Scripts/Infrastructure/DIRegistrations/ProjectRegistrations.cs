using Assets.Scripts.Infrastructure.ConfigsManagement;
using Assets.Scripts.Infrastructure.DI_Container;
using Assets.Scripts.Meta.Wallet;
using Assets.Scripts.Utilities.AssetsManagement;
using Assets.Scripts.Utilities.CoroutinesManagement;
using Assets.Scripts.Utilities.DataManagement;
using Assets.Scripts.Utilities.DataManagement.DataProviders;
using Assets.Scripts.Utilities.DataManagement.DataRepository;
using Assets.Scripts.Utilities.DataManagement.KeysStorage;
using Assets.Scripts.Utilities.DataManagement.Serializers;
using Assets.Scripts.Utilities.LoadingScreen;
using Assets.Scripts.Utilities.Reactive;
using Assets.Scripts.Utilities.SaveScreen;
using Assets.Scripts.Utilities.SceneManagement;
using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Assets.Scripts.Utilities.Factory
{
    public class ProjectRegistrations
    {
        public void Register(DIContainer container)
        {
            container.RegisterAsSingle<ISaveLoadService>(CreateSaveLoadService);

            container.RegisterAsSingle(CreatePlayerDataProvider);

            container.RegisterAsSingle<ICoroutinesPerformer>(CreateCoroutinesPerformer);

            container.RegisterAsSingle<ILoadingScreen>(CreateLoadingScreenHandler);

            container.RegisterAsSingle<ISaveScreen>(CreateSaveScreenHandler);

            container.RegisterAsSingle(CreateConfigsProviderService);

            container.RegisterAsSingle(c => new ResourcesAssetsLoader());

            container.RegisterAsSingle(CreateSceneLoaderService);

            container.RegisterAsSingle(CreateSceneSwitcherService);

            container.RegisterAsSingle(CreateWalletService).NonLazy();
        }

        private SaveLoadService CreateSaveLoadService(DIContainer c)
        {
            IDataSerializer dataSerializer = new JsonSerializer();
            IDataKeysStorage dataKeysStorage = new MapDataKeyStorage();

            string saveFolderPath = Application.isEditor ? Application.dataPath : Application.persistentDataPath;

            IDataRepository dataRepository = new LocalFileDataRepository(saveFolderPath, "json");

            return new(
                dataSerializer,
                dataKeysStorage,
                dataRepository,
                c.Resolve<ISaveScreen>());
        }

        private PlayerDataProvider CreatePlayerDataProvider(DIContainer c)
            => new PlayerDataProvider(c.Resolve<ISaveLoadService>(), c.Resolve<ConfigsProviderService>());

        private WalletService CreateWalletService(DIContainer c)
        {
            Dictionary<CurrencyTypes, ReactiveVariable<int>> currencies = new();

            foreach (var type in Enum.GetValues(typeof(CurrencyTypes)))
                currencies.Add((CurrencyTypes)type, new ReactiveVariable<int>());

            return new WalletService(currencies, c.Resolve<PlayerDataProvider>());
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

        private SaveScreenHandler CreateSaveScreenHandler(DIContainer c)
        {
            ResourcesAssetsLoader resourcesAssetsLoader = c.Resolve<ResourcesAssetsLoader>();

            SaveScreenHandler saveScreenHandler = resourcesAssetsLoader
                .Load<SaveScreenHandler>("Utilities/SaveScreenCanvas");

            return Object.Instantiate(saveScreenHandler);
        }

        private SceneSwitcherService CreateSceneSwitcherService(DIContainer c)
            => new SceneSwitcherService(
                c,
                c.Resolve<SceneLoaderService>(),
                c.Resolve<ILoadingScreen>());
    }
}