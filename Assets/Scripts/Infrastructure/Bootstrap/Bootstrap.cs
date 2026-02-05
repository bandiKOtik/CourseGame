using Assets.Scripts.Infrastructure.ConfigsManagement;
using Assets.Scripts.Infrastructure.DI_Container;
using Assets.Scripts.Utilities.AssetsManagement;
using Assets.Scripts.Utilities.CoroutinesManagement;
using Assets.Scripts.Utilities.Factory;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Infrastructure.Bootstrap
{
    public class Bootstrap : MonoBehaviour
    {
        private DIContainer _container;

        private void Awake()
        {
            _container = new();

            ICoroutinesPerformer coroutinesPerformer = _container.Resolve<ICoroutinesPerformer>();

            coroutinesPerformer.StartPerform(LoadConfigs());
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