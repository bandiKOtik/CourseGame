using Assets.Scripts.Utilities.DataManagement.DataProviders;
using Assets.Scripts.Utilities.LoadingScreen;
using Assets.Scripts.Utilities.SceneManagement;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Infrastructure.Bootstraps
{
    public class GameEntryPoint : MonoBehaviour
    {
        private ILoadingScreen _loadScreen;
        private SceneSwitcherService _sceneSwitcher;
        private PlayerDataProvider _playerDataProvider;

        [Inject]
        private void Construct(
            ILoadingScreen loadScreen,
            SceneSwitcherService sceneSwitcher,
            PlayerDataProvider playerDataProvider)
        {
            _loadScreen = loadScreen;
            _sceneSwitcher = sceneSwitcher;
            _playerDataProvider = playerDataProvider;
        }

        private void Start()
        {
            SetupAppSettings();

            Initialize().Forget();
        }

        public async UniTask Initialize()
        {
            await UniTask.CompletedTask;

            _loadScreen.Show();

            bool isPlayerDataSaveExists = false;

            isPlayerDataSaveExists = await _playerDataProvider.ExistsAsync();

            if (isPlayerDataSaveExists)
                await _playerDataProvider.LoadAsync();
            else
                _playerDataProvider.Reset();

            await _playerDataProvider.SaveAsync();

            _loadScreen.Hide();

            await _sceneSwitcher.SwitchAsync(Scenes.MainMenu);
        }

        private void SetupAppSettings()
        {
            QualitySettings.vSyncCount = 0;
            Application.targetFrameRate = 60;
        }
    }
}