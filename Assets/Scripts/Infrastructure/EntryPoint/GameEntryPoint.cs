using Assets.Scripts.Infrastructure.ConfigsManagement;
using Assets.Scripts.Infrastructure.DI_Container;
using Assets.Scripts.Utilities.CoroutinesManagement;
using Assets.Scripts.Utilities.Factory;
using Assets.Scripts.Utilities.LoadingScreen;
using Assets.Scripts.Utilities.SceneManagement;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Infrastructure.EntryPoint
{
    internal class GameEntryPoint : MonoBehaviour
    {
        private ProjectRegistrations _projectRegistrations = new();

        private void Awake()
        {
            SetupAppSettings();

            DIContainer projectContainer = new();

            _projectRegistrations.Register(projectContainer);

            projectContainer
                .Resolve<ICoroutinesPerformer>()
                .StartPerform(Initialize(projectContainer));
        }

        public IEnumerator Initialize(DIContainer container)
        {
            ILoadingScreen loadScreen = container.Resolve<ILoadingScreen>();
            SceneSwitcherService sceneSwitcher = container.Resolve<SceneSwitcherService>();
            loadScreen.Show();

            yield return container
                .Resolve<ConfigsProviderService>().LoadAsync();

            loadScreen.Hide();

            yield return sceneSwitcher.ProcessSwitchTo(Scenes.MainMenu);
        }

        private void SetupAppSettings()
        {
            QualitySettings.vSyncCount = 0;
            Application.targetFrameRate = 60;
        }
    }
}