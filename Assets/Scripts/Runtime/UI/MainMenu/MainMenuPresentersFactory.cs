using Assets.Scripts.Infrastructure.ConfigsManagement;
using Assets.Scripts.Meta.Statistics;
using Assets.Scripts.Utilities.Factory.UI;
using Assets.Scripts.Utilities.SceneManagement;

namespace Assets.Scripts.Runtime.UI.MainMenu
{
    public class MainMenuPresentersFactory
    {
        private readonly ProjectPresentersFactory _projectPresentersFactory;
        private readonly ProgressionResetService _progressionResetService;
        private readonly SceneSwitcherService _sceneSwitcherService;
        private readonly ConfigsProviderService _configsProviderService;

        public MainMenuPresentersFactory(
            ProjectPresentersFactory projectPresentersFactory,
            ProgressionResetService progressionResetService,
            SceneSwitcherService sceneSwitcherService,
            ConfigsProviderService configsProviderService)
        {
            _projectPresentersFactory = projectPresentersFactory;
            _progressionResetService = progressionResetService;
            _sceneSwitcherService = sceneSwitcherService;
            _configsProviderService = configsProviderService;
        }

        public MainMenuScreenPresenter CreateMainMenuScreen(MainMenuScreenView view)
            => new(view,
                _projectPresentersFactory,
                _progressionResetService,
                _sceneSwitcherService,
                _configsProviderService);
    }
}