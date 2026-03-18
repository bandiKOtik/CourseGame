using Assets.Scripts.Meta.Statistics;
using Assets.Scripts.Runtime.UI.Core;
using Assets.Scripts.Utilities.Factory.UI;
using System.Collections.Generic;

namespace Assets.Scripts.Runtime.UI.MainMenu
{
    public class MainMenuScreenPresenter : IPresenter
    {
        public const int ResetProgressPrice = 100;
        private readonly MainMenuScreenView _screen;
        private readonly ProjectPresentersFactory _factory;

        private readonly MainMenuPopupService _popupService;
        private ProgressionResetService _resetService;

        private readonly List<IPresenter> _childPresenters = new();

        public MainMenuScreenPresenter(
            MainMenuScreenView screen,
            ProjectPresentersFactory factory,
            MainMenuPopupService popupService,
            ProgressionResetService resetService)
        {
            _screen = screen;
            _factory = factory;
            _popupService = popupService;
            _resetService = resetService;
        }

        public void Initialize()
        {
            _screen.LevelsButtonClicked += OnLevelsMenuButtonClicked;
            _screen.ResetProgressButtonClicked += OnResetButtonClicked;

            CreatePresenters();

            foreach (var presenter in _childPresenters)
                presenter.Initialize();
        }

        public void Dispose()
        {
            _screen.LevelsButtonClicked -= OnLevelsMenuButtonClicked;
            _screen.ResetProgressButtonClicked -= OnResetButtonClicked;

            foreach (var presenter in _childPresenters)
                presenter.Dispose();

            _childPresenters.Clear();
        }

        private void OnLevelsMenuButtonClicked()
        {
            _popupService.OpenLevelsMenuPopup();
        }

        private void OnResetButtonClicked()
        {
            _resetService.Reset();
        }

        private void CreatePresenters()
        {
            var walletPresenter = _factory.CreateWalletPresenter(_screen.WalletView);
            var statisticsPresenter = _factory.CreateStatisticsElementsPresenter(_screen.StatisticsView);

            _childPresenters.Add(walletPresenter);
            _childPresenters.Add(statisticsPresenter);
        }
    }
}