using Assets.Scripts.Infrastructure.DI_Container;
using Assets.Scripts.Infrastructure.DIRegistrations;
using Assets.Scripts.Infrastructure.Gameplay;
using Assets.Scripts.Runtime.Infrastructure;
using Assets.Scripts.Runtime.InputManagement;
using Assets.Scripts.Utilities.CoroutinesManagement;
using Assets.Scripts.Utilities.SceneManagement;
using System;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Infrastructure.ConfigsManagement.Bootstraps
{
    public class GameplayBootstrap : SceneBootstrap
    {
        private DIContainer _container;
        private IInputHandler _inputHandler;
        private ICoroutinesPerformer _coroutinesPerformer;
        private GameSession _session;
        private GameplayContextRegistrations _contextRegistrations = new();

        private bool _initialized = false;

        public override void ProcessRegistrations(DIContainer container, IInputSceneArgs sceneArgs = null)
        {
            _container = container;

            if (sceneArgs is not GameplayInputArgs args)
                throw new ArgumentException(
                    nameof(sceneArgs) + " is not match with " + typeof(GameplayInputArgs));

            Debug.Log("Gamemode: " + args.GameConfig.Current);

            _contextRegistrations.Process(_container, args);
        }

        public override IEnumerator Initialize()
        {
            _coroutinesPerformer = _container.Resolve<ICoroutinesPerformer>();

            _inputHandler = _container.Resolve<IInputHandler>();

            _coroutinesPerformer.StartPerform(LoadConfigs());

            _session = new();

            yield return _session.Initialize(_container);
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