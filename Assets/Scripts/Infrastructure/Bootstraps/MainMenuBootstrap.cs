using Assets.Scripts.Infrastructure.ConfigsManagement;
using Assets.Scripts.Infrastructure.ConfigsManagement.Bootstraps;
using Assets.Scripts.Infrastructure.DI_Container;
using Assets.Scripts.Infrastructure.DIRegistrations;
using Assets.Scripts.Utilities.CoroutinesManagement;
using Assets.Scripts.Utilities.SceneManagement;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Infrastructure.Bootstraps
{
    public class MainMenuBootstrap : SceneBootstrap
    {
        private DIContainer _container;
        private MainMenuContextRegistrations _contextRegistrations = new();

        public override void ProcessRegistrations(DIContainer container, IInputSceneArgs sceneArgs = null)
        {
            _container = container;

            _contextRegistrations.Process(_container);
        }

        public override IEnumerator Initialize()
        {
            ICoroutinesPerformer coroutinesPerformer = _container.Resolve<ICoroutinesPerformer>();

            coroutinesPerformer.StartPerform(LoadConfigs());

            yield break;
        }

        public override void Run()
        {
            Debug.Log("Running main menu scene bootstrap");
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