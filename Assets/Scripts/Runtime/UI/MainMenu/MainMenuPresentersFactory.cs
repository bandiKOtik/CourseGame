using Assets.Scripts.Infrastructure.DI_Container;
using Assets.Scripts.Meta.Statistics;
using Assets.Scripts.Utilities.Factory.UI;

namespace Assets.Scripts.Runtime.UI.MainMenu
{
    public class MainMenuPresentersFactory
    {
        private readonly DIContainer _container;

        public MainMenuPresentersFactory(DIContainer container)
        {
            _container = container;
        }

        public MainMenuScreenPresenter CreateMainMenuScreen(MainMenuScreenView view)
            => new(view,
                _container.Resolve<ProjectPresentersFactory>(),
                _container.Resolve<MainMenuPopupService>(),
                _container.Resolve<ProgressionResetService>());
    }
}