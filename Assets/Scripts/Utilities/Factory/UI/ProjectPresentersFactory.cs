using Assets.Scripts.Configs.Meta.Wallet;
using Assets.Scripts.Infrastructure.ConfigsManagement;
using Assets.Scripts.Infrastructure.DI_Container;
using Assets.Scripts.Meta.Features.LevelsProgression;
using Assets.Scripts.Meta.Features.Wallet;
using Assets.Scripts.Meta.Statistics;
using Assets.Scripts.Runtime.UI.CommonViews;
using Assets.Scripts.Runtime.UI.LevelsMenuPopup;
using Assets.Scripts.Runtime.UI.Statistics;
using Assets.Scripts.Runtime.UI.Wallet;
using Assets.Scripts.Utilities.Reactive;
using Assets.Scripts.Utilities.SceneManagement;

namespace Assets.Scripts.Utilities.Factory.UI
{
    public class ProjectPresentersFactory
    {
        private readonly ConfigsProviderService _configsProviderService;
        private readonly SceneSwitcherService _sceneSwitcherService;
        private readonly LevelsProgressionService _levelsProgressionService;
        private readonly WalletService _walletService;
        private readonly ViewsFactory _viewsFactory;
        private readonly PlayedGamesStatistic _playedGamesStatistic;

        public ProjectPresentersFactory(
            ConfigsProviderService configsProviderService,
            SceneSwitcherService sceneSwitcherService,
            LevelsProgressionService levelsProgressionService,
            WalletService walletService,
            ViewsFactory viewsFactory,
            PlayedGamesStatistic playedGamesStatistic)
        {
            _configsProviderService = configsProviderService;
            _sceneSwitcherService = sceneSwitcherService;
            _levelsProgressionService = levelsProgressionService;
            _walletService = walletService;
            _viewsFactory = viewsFactory;
            _playedGamesStatistic = playedGamesStatistic;
        }

        public CurrencyPresenter CreateCurrencyPresenter(
            IconTextView view,
            IReadOnlyVariable<int> currency,
            CurrencyTypes currencyType)
        {
            var config = _configsProviderService
                .GetConfig<CurrencyIconsConfig>();

            return new(currency, currencyType, config, view);
        }

        public WalletPresenter CreateWalletPresenter(IconTextListView view)
        {
            return new(_walletService, this, _viewsFactory, view);
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
            return new(_playedGamesStatistic, this, _viewsFactory, view);
        }

        public LevelTilePresenter CreateLevelTilePresenter(LevelTileView view, int levelNumber)
        {
            return new(
                _levelsProgressionService,
                _sceneSwitcherService,
                levelNumber,
                view);
        }

        public LevelsMenuPopupPresenter CreateLevelMenuPopupPresenter(LevelsMenuPopupView view)
        {
            return new(
                _configsProviderService,
                this,
                _viewsFactory,
                view);
        }
    }
}