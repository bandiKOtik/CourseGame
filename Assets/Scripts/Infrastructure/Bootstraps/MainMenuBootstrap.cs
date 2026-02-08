using Assets.Scripts.Infrastructure.Configs;
using Assets.Scripts.Infrastructure.ConfigsManagement;
using Assets.Scripts.Infrastructure.ConfigsManagement.Bootstraps;
using Assets.Scripts.Infrastructure.DI_Container;
using Assets.Scripts.Infrastructure.DIRegistrations;
using Assets.Scripts.Runtime.InputManagement;
using Assets.Scripts.Utilities.CoroutinesManagement;
using Assets.Scripts.Utilities.SceneManagement;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Infrastructure.Bootstraps
{
    public class MainMenuBootstrap : SceneBootstrap
    {
        [SerializeField] private GameModeConfig[] _gameModes;

        private DIContainer _container;
        private IInputHandler _inputHandler;
        private MainMenuContextRegistrations _contextRegistrations = new();

        private bool _initialized = false;

        public override void ProcessRegistrations(DIContainer container, IInputSceneArgs sceneArgs = null)
        {
            _container = container;

            _contextRegistrations.Process(_container);
        }

        public override IEnumerator Initialize()
        {
            ICoroutinesPerformer coroutinesPerformer = _container.Resolve<ICoroutinesPerformer>();

            _inputHandler = _container.Resolve<IInputHandler>();

            coroutinesPerformer.StartPerform(LoadConfigs());

            yield break;
        }

        private void Update()
        {
            if (_initialized)
            {
                _inputHandler.Update();
            }
        }

        public override void Run() => _initialized = true;

        private IEnumerator LoadConfigs()
        {
            ConfigsProviderService configsProvider = _container.Resolve<ConfigsProviderService>();

            yield return configsProvider.LoadAsync();
        }
    }
}