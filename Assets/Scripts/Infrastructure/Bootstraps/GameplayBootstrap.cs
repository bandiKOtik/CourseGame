using Assets.Scripts.Infrastructure.DI_Container;
using Assets.Scripts.Infrastructure.DIRegistrations;
using Assets.Scripts.Infrastructure.Gameplay;
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
        private GameplayInputArgs _inputArgs;
        private GameplayContextRegistrations _contextRegistrations = new();

        public override void ProcessRegistrations(DIContainer container, IInputSceneArgs sceneArgs = null)
        {
            _container = container;

            if (sceneArgs is not GameplayInputArgs args)
                throw new ArgumentException(
                    nameof(sceneArgs) + " is not match with " + typeof(GameplayInputArgs));

            _inputArgs = args;

            _contextRegistrations.Process(_container, _inputArgs);
        }

        public override IEnumerator Initialize()
        {
            Debug.Log("Current level: " + _inputArgs.LevelNumber);

            ICoroutinesPerformer coroutinesPerformer = _container.Resolve<ICoroutinesPerformer>();

            coroutinesPerformer.StartPerform(LoadConfigs());

            yield break;
        }

        public override void Run()
        {
            Debug.Log("Running gameplay scene bootstrap");
        }

        private IEnumerator LoadConfigs()
        {
            ConfigsProviderService configsProvider = _container.Resolve<ConfigsProviderService>();

            Debug.Log("Configs loading performed");

            yield return configsProvider.LoadAsync();

            Debug.Log("Configs loaded");
        }
    }
}
