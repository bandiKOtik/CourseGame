using Assets.Scripts.Infrastructure.DI_Container;
using Assets.Scripts.Utilities.Factory;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Infrastructure.EntryPoint
{
    internal class GameEntryPoint : MonoBehaviour
    {
        private ProjectRegistrations _projectRegistrations;

        private void Awake()
        {
            SetupAppSettings();

            DIContainer container = new DIContainer();

            _projectRegistrations = new ProjectRegistrations();

            _projectRegistrations.Register(container);
        }

        public IEnumerator Initialize(DIContainer container)
        {
            //x ILoadingScreen here
            //? ILoadingScreen here

            yield return null;
        }

        private void SetupAppSettings()
        {
            QualitySettings.vSyncCount = 0;
            Application.targetFrameRate = 60;
        }
    }
}
