using Assets.Scripts.Infrastructure.ConfigsManagement;
using Assets.Scripts.Infrastructure.DI_Container;
using Assets.Scripts.Utilities.CoroutinesManagement;
using Assets.Scripts.Utilities.DataManagement.DataProviders;
using Assets.Scripts.Utilities.Factory;
using Assets.Scripts.Utilities.LoadingScreen;
using Assets.Scripts.Utilities.SceneManagement;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Infrastructure.Bootstraps
{
    internal class GameEntryPoint : MonoBehaviour
    {
        private ProjectContextRegistrations _projectRegistrations = new();
        private ICoroutinesPerformer _coroutinesPerformer;

        private void Awake()
        {
            SetupAppSettings();

            DIContainer projectContainer = new();

            _projectRegistrations.Register(projectContainer);

            projectContainer.Initialize();

            _coroutinesPerformer = projectContainer
                .Resolve<ICoroutinesPerformer>();

            _coroutinesPerformer
                .StartPerform(Initialize(projectContainer));
        }

        public IEnumerator Initialize(DIContainer container)
        {
            ILoadingScreen loadScreen = container.Resolve<ILoadingScreen>();
            SceneSwitcherService sceneSwitcher = container.Resolve<SceneSwitcherService>();
            PlayerDataProvider playerDataProvider = container.Resolve<PlayerDataProvider>();

            loadScreen.Show();

            yield return container
                .Resolve<ConfigsProviderService>().LoadAsync();

            bool isPlayerDataSaveExists = false;

            yield return playerDataProvider.Exists(result => isPlayerDataSaveExists = result);

            if (isPlayerDataSaveExists)
                yield return _coroutinesPerformer
                    .StartPerform(playerDataProvider.LoadAsync());
            else
                playerDataProvider.Reset();

            _coroutinesPerformer
                .StartPerform(playerDataProvider.SaveAsync());

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