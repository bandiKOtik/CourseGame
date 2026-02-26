using Assets.Scripts.Configs.Meta.Wallet;
using Assets.Scripts.Infrastructure.ConfigsManagement;
using Assets.Scripts.Infrastructure.DI_Container;
using Assets.Scripts.Meta.Features.Wallet;
using Assets.Scripts.Meta.Statistics;
using Assets.Scripts.Runtime.UI.Core;
using Assets.Scripts.Runtime.UI.MainMenu;
using Assets.Scripts.Utilities.AssetsManagement;
using Assets.Scripts.Utilities.Factory.UI;
using UnityEngine;

namespace Assets.Scripts.Infrastructure.DIRegistrations
{
    public class MainMenuContextRegistrations
    {
        public void Process(DIContainer container)
        {
            container.RegisterAsSingle(CreateProjectUIRoot).NonLazy();

            container.RegisterAsSingle(c => new MainMenuPresentersFactory(c));

            container.RegisterAsSingle(CreateMainMenuScreenPresenter).NonLazy();

            container.RegisterAsSingle(CreateMainMenuPopupService);

            container.RegisterAsSingle(CreateProgressionResetService);
        }

        private MainMenuUIRoot CreateProjectUIRoot(DIContainer c)
        {
            ResourcesAssetsLoader resourcesAssetsLoader = c.Resolve<ResourcesAssetsLoader>();

            MainMenuUIRoot rootPrefab = resourcesAssetsLoader
                .Load<MainMenuUIRoot>("UI/MainMenu/MainMenuUIRoot");

            return Object.Instantiate(rootPrefab);
        }

        private MainMenuScreenPresenter CreateMainMenuScreenPresenter(DIContainer c)
        {
            MainMenuUIRoot uiRoot = c.Resolve<MainMenuUIRoot>();

            MainMenuScreenView view = c.Resolve<ViewsFactory>()
                .Create<MainMenuScreenView>(ViewIDs.MainMenuScreen, uiRoot.HUDLayer);

            MainMenuScreenPresenter presenter = c.Resolve<MainMenuPresentersFactory>()
                .CreateMainMenuScreen(view);

            return presenter;
        }

        private MainMenuPopupService CreateMainMenuPopupService(DIContainer c)
        {
            return new MainMenuPopupService(
                c.Resolve<ViewsFactory>(),
                c.Resolve<ProjectPresentersFactory>(),
                c.Resolve<MainMenuUIRoot>());
        }

        private ProgressionResetService CreateProgressionResetService(DIContainer c)
        {
            return new(
                c.Resolve<WalletService>(),
                c.Resolve<PlayedGamesStatistic>(),
                c.Resolve<ConfigsProviderService>()
                .GetConfig<GamePriceConfig>());
        }
    }
}