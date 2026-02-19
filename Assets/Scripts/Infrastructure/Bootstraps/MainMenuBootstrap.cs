using Assets.Scripts.Infrastructure.ConfigsManagement.Bootstraps;
using Assets.Scripts.Infrastructure.DI_Container;
using Assets.Scripts.Infrastructure.DIRegistrations;
using Assets.Scripts.Runtime.InputManagement;
using Assets.Scripts.Runtime.UI.CommonViews;
using Assets.Scripts.Utilities.CoroutinesManagement;
using Assets.Scripts.Utilities.Factory.UI;
using Assets.Scripts.Utilities.SceneManagement;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Infrastructure.Bootstraps
{
    public class MainMenuBootstrap : SceneBootstrap
    {
        private DIContainer _container;
        private MainMenuContextRegistrations _contextRegistrations = new();

        private bool _initialized = false;

        private IInputHandler _inputHandler;

        public override void ProcessRegistrations(DIContainer container, IInputSceneArgs sceneArgs = null)
        {
            _container = container;

            _contextRegistrations.Process(_container);
        }

        public override IEnumerator Initialize()
        {
            ICoroutinesPerformer coroutinesPerformer = _container.Resolve<ICoroutinesPerformer>();

            _inputHandler = _container.Resolve<IInputHandler>();

            _container
                .Resolve<ProjectPresentersFactory>()
                .CreateWalletPresenter(Object.FindAnyObjectByType<IconTextListView>())
                .Initialize();

            yield break;
        }

        private void Update()
        {
            if (_initialized == false)
                return;

            _inputHandler.Update();
        }

        public override void Run() => _initialized = true;
    }
}