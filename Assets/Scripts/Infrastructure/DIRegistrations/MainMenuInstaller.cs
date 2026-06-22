using Assets.Scripts.Meta.Statistics;
using Assets.Scripts.Runtime.UI.Core;
using Assets.Scripts.Runtime.UI.MainMenu;
using Assets.Scripts.Utilities.Factory.UI;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Infrastructure.DIRegistrations
{
    public class MainMenuInstaller : MonoInstaller
    {
        ViewsFactory _viewsFactory;
        MainMenuPresentersFactory _presentersFactory;

        [Inject]
        private void Construct(ViewsFactory viewsFactory)
        {
            _viewsFactory = viewsFactory;
        }

        public override void InstallBindings()
        {
            Debug.Log("Main menu installation...");

            Container
                .Bind<MainMenuUIRoot>()
                .FromResource("UI/MainMenu/MainMenuUIRoot")
                .AsSingle()
                .NonLazy();

            Container.Bind<MainMenuPresentersFactory>().AsSingle().NonLazy();

            Container.Bind<ProgressionResetService>().AsSingle();

            MainMenuScreenView view = _viewsFactory.Create<MainMenuScreenView>(
                ViewIDs.MainMenuScreen,
                Container.Resolve<MainMenuUIRoot>().HUDLayer);

            _presentersFactory = Container.Resolve<MainMenuPresentersFactory>();

            MainMenuScreenPresenter presenter = _presentersFactory.CreateMainMenuScreen(view);

            Container
                .Bind<MainMenuScreenPresenter>()
                .FromInstance(presenter)
                .AsSingle()
                .NonLazy();

            Container.Bind<MainMenuPopupService>().AsSingle();

            Debug.Log("Main menu installation completed!");
        }
    }
}
