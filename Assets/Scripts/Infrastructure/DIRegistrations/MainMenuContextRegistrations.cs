using Assets.Scripts.Configs.Meta.GameModeConfigs;
using Assets.Scripts.Configs.Meta.Wallet;
using Assets.Scripts.Infrastructure.ConfigsManagement;
using Assets.Scripts.Infrastructure.DI_Container;
using Assets.Scripts.Meta.Statistics;
using Assets.Scripts.Meta.Wallet;
using Assets.Scripts.Runtime.InputManagement;
using Assets.Scripts.Runtime.UI.CommonViews;
using Assets.Scripts.Runtime.UI.Wallet;
using Assets.Scripts.Utilities.AssetsManagement;
using Assets.Scripts.Utilities.CoroutinesManagement;
using Assets.Scripts.Utilities.DataManagement.DataProviders;
using Assets.Scripts.Utilities.Factory.UI;
using Assets.Scripts.Utilities.SceneManagement;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Infrastructure.DIRegistrations
{
    public class MainMenuContextRegistrations
    {
        private readonly string _gameModeConfigPath = "Configs/GameModeConfig";

        public void Process(DIContainer container)
        {
            container.RegisterAsSingle<IInputHandler>(CreateGameModeSelector);

            container.RegisterAsSingle(CreateWalletPresenter).NonLazy();
        }

        private WalletPresenter CreateWalletPresenter(DIContainer c)
        {
            var view = Object.FindObjectOfType<IconTextListView>();

            var presenter = c.Resolve<ProjectPresentersFactory>().CreateWalletPresenter(view);

            return presenter;
        }

        private GameModeSelector CreateGameModeSelector(DIContainer c)
        {
            IReadOnlyDictionary<CurrencyTypes, int> prices = c
                .Resolve<ConfigsProviderService>()
                        .GetConfig<GamePriceConfig>()
                        .GetEnterPrices();

            return new(
                        c.Resolve<ICoroutinesPerformer>(),
                        c.Resolve<SceneSwitcherService>(),
                        c.Resolve<PlayerDataProvider>(),
                        c.Resolve<ResourcesAssetsLoader>()
                        .Load<GameModeConfig>(_gameModeConfigPath),
                        c.Resolve<WalletService>(),
                        c.Resolve<PlayedGamesStatistic>(),
                        prices);
        }
    }
}