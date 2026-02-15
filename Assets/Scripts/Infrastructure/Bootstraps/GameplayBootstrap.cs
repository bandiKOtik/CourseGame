using Assets.Scripts.Infrastructure.DI_Container;
using Assets.Scripts.Infrastructure.DIRegistrations;
using Assets.Scripts.Infrastructure.Gameplay;
using Assets.Scripts.Runtime.Gameplay;
using Assets.Scripts.Runtime.InputManagement;
using Assets.Scripts.Runtime.Sequence;
using Assets.Scripts.Utilities.CoroutinesManagement;
using Assets.Scripts.Utilities.DataManagement;
using Assets.Scripts.Utilities.DataManagement.DataProviders;
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

            Debug.Log("<color=blue>Gamemode</color>: " + args.CurrentGamemode);

            _contextRegistrations.Process(_container, args);
        }

        public override IEnumerator Initialize()
        {
            _coroutinesPerformer = _container.Resolve<ICoroutinesPerformer>();

            _session = new();

            yield return _session.Initialize(
                _coroutinesPerformer,
                _container.Resolve<ISequenceGenerator>(),
                _container.Resolve<SceneSwitcherService>(),
                _container.Resolve<SequenceMatcher>(),
                _inputHandler = _container.Resolve<IInputHandler>(),
                _container.Resolve<PlayerDataProvider>());
        }

        private void Update()
        {
            if (_initialized)
                _inputHandler.Update();
        }

        public override void Run() => _initialized = true;
    }
}