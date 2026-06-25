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
        public override void InstallBindings()
        {
            Debug.Log("Main menu installation...");

            Container
                .Bind<MainMenuUIRoot>()
                .FromComponentInNewPrefabResource("UI/MainMenu/MainMenuUIRoot")
                .AsSingle()
                .NonLazy();

            Container.Bind<MainMenuPresentersFactory>().AsSingle();

            Container.Bind<ProgressionResetService>().AsSingle();

            Container
                .Bind<MainMenuScreenPresenter>()
                .FromMethod(RegisterMainMenuScreenPresenter)
                .AsSingle()
                .NonLazy();

            Container.Bind<MainMenuPopupService>().AsSingle();

            Debug.Log("Main menu installation completed!");
        }

        private MainMenuScreenPresenter RegisterMainMenuScreenPresenter()
        {
            ViewsFactory viewsFactory = Container.Resolve<ViewsFactory>();

            MainMenuScreenView view = viewsFactory.Create<MainMenuScreenView>(
                ViewIDs.MainMenuScreen,
                Container.Resolve<MainMenuUIRoot>().HUDLayer);

            var presentersFactory = Container.Resolve<MainMenuPresentersFactory>();

            return presentersFactory.CreateMainMenuScreen(view);
        }
    }
}