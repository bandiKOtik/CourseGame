using Assets.Scripts.Configs.Gameplay.Levels;
using Assets.Scripts.Infrastructure.ConfigsManagement;
using Assets.Scripts.Infrastructure.Gameplay;
using Assets.Scripts.Meta.Statistics;
using Assets.Scripts.Runtime.UI.Core;
using Assets.Scripts.Utilities.CoroutinesManagement;
using Assets.Scripts.Utilities.Factory.UI;
using Assets.Scripts.Utilities.SceneManagement;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Runtime.UI.MainMenu
{
    public class MainMenuScreenPresenter : IPresenter
    {
        public const int ResetProgressPrice = 100;
        private readonly MainMenuScreenView _screen;
        private readonly ProjectPresentersFactory _factory;

        private ProgressionResetService _resetService;
        private readonly ICoroutinesPerformer _performer;
        private readonly SceneSwitcherService _sceneSwitcher;
        private readonly ConfigsProviderService _configProvider;

        private readonly List<IPresenter> _childPresenters = new();

        public MainMenuScreenPresenter(
            MainMenuScreenView screen,
            ProjectPresentersFactory factory,
            ProgressionResetService resetService,
            ICoroutinesPerformer performer,
            SceneSwitcherService sceneSwitcher,
            ConfigsProviderService configProvider)
        {
            _screen = screen;
            _factory = factory;
            _resetService = resetService;
            _performer = performer;
            _sceneSwitcher = sceneSwitcher;
            _configProvider = configProvider;
        }

        public void Initialize()
        {
            _screen.LevelsButtonClicked += OnPlayButtonClicked;
            _screen.ResetProgressButtonClicked += OnResetButtonClicked;

            CreatePresenters();

            foreach (var presenter in _childPresenters)
                presenter.Initialize();
        }

        public void Dispose()
        {
            _screen.LevelsButtonClicked -= OnPlayButtonClicked;
            _screen.ResetProgressButtonClicked -= OnResetButtonClicked;

            foreach (var presenter in _childPresenters)
                presenter.Dispose();

            _childPresenters.Clear();
        }

        private void OnPlayButtonClicked()
        {
            int levelsNumber = _configProvider.GetConfig<LevelsListConfig>().Levels.Count;

            int selectedLevel = Random.Range(1, levelsNumber);
            Debug.Log("Current level: " + selectedLevel);

            _performer
                .StartPerform(_sceneSwitcher
                .SwitchAsync(Scenes.Gameplay, new GameplayInputArgs(selectedLevel)));
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