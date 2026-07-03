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

            Container.Bind<MainMenuPresentersFactory>().AsSingle();

            Container
                .Bind<MainMenuUIRoot>()
                .FromComponentInNewPrefabResource("UI/MainMenu/MainMenuUIRoot")
                .AsSingle()
                .NonLazy();

            Container.Bind<ProgressionResetService>().AsSingle();

            Container
                .BindInterfacesAndSelfTo<MainMenuScreenPresenter>()
                .FromMethod(RegisterMainMenuScreenPresenter)
                .AsSingle()
                .NonLazy();

            Container.Bind<MainMenuPopupService>().AsSingle();

            Debug.Log("Main menu installation completed!");
        }

        private MainMenuScreenPresenter RegisterMainMenuScreenPresenter(InjectContext arg)
        {
            ViewsFactory viewsFactory = Container.Resolve<ViewsFactory>();

            Transform hudLayer = Container.Resolve<MainMenuUIRoot>().HUDLayer;

            MainMenuScreenView view = viewsFactory.Create<MainMenuScreenView>(
                ViewIDs.MainMenuScreen,
                hudLayer);

            var presentersFactory = Container.Resolve<MainMenuPresentersFactory>();

            return presentersFactory.CreateMainMenuScreen(view);
        }
    }
}