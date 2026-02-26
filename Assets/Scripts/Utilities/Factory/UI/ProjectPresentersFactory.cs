using Assets.Scripts.Configs.Meta.Wallet;
using Assets.Scripts.Infrastructure.ConfigsManagement;
using Assets.Scripts.Infrastructure.DI_Container;
using Assets.Scripts.Meta.Features.LevelsProgression;
using Assets.Scripts.Meta.Features.Wallet;
using Assets.Scripts.Meta.Statistics;
using Assets.Scripts.Runtime.Gameplay;
using Assets.Scripts.Runtime.UI;
using Assets.Scripts.Runtime.UI.CommonViews;
using Assets.Scripts.Runtime.UI.LevelsMenuPopup;
using Assets.Scripts.Runtime.UI.StatisticsUI;
using Assets.Scripts.Runtime.UI.Wallet;
using Assets.Scripts.Utilities.CoroutinesManagement;
using Assets.Scripts.Utilities.Reactive;
using Assets.Scripts.Utilities.SceneManagement;

namespace Assets.Scripts.Utilities.Factory.UI
{
    public class ProjectPresentersFactory
    {
        private readonly DIContainer _container;

        public ProjectPresentersFactory(DIContainer container)
        {
            _container = container;
        }

        public CurrencyPresenter CreateCurrencyPresenter(
            IconTextView view,
            IReadOnlyVariable<int> currency,
            CurrencyTypes currencyType)
        {
            var config = _container
                .Resolve<ConfigsProviderService>()
                .GetConfig<CurrencyIconsConfig>();

            return new(currency, currencyType, config, view);
        }

        public WalletPresenter CreateWalletPresenter(IconTextListView view)
        {
            var walletService = _container.Resolve<WalletService>();
            var viewsFactory = _container.Resolve<ViewsFactory>();

            return new(walletService, this, viewsFactory, view);
        }

        public StatPresenter CreateStatisticPresenter(
            TextView view,
            int statValue,
            GameStatType type)
        {
            return new(statValue, type, view);
        }

        public StatisticsWindowPresenter CreateStatisticsElementsPresenter(TextListView view)
        {
            var statistics = _container.Resolve<PlayedGamesStatistic>();
            var viewsFactory = _container.Resolve<ViewsFactory>();

            return new(statistics, this, viewsFactory, view);
        }

        public LevelTilePresenter CreateLevelTilePresenter(LevelTileView view, GameMode gameMode)
        {
            return new(
                _container.Resolve<LevelsProgressionService>(),
                _container.Resolve<SceneSwitcherService>(),
                _container.Resolve<ICoroutinesPerformer>(),
                gameMode,
                view);
        }

        public LevelsMenuPopupPresenter CreateLevelMenuPopupPresenter(LevelsMenuPopupView view)
        {
            return new(
                _container.Resolve<ICoroutinesPerformer>(),
                _container.Resolve<ConfigsProviderService>(),
                this,
                _container.Resolve<ViewsFactory>(),
                view);
        }
    }
}