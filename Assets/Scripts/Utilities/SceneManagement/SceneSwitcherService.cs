using Assets.Scripts.Infrastructure.ConfigsManagement.Bootstraps;
using Assets.Scripts.Utilities.LoadingScreen;
using Cysharp.Threading.Tasks;
using System;
using Object = UnityEngine.Object;

namespace Assets.Scripts.Utilities.SceneManagement
{
    public class SceneSwitcherService
    {
        private readonly SceneLoaderService _sceneLoaderService;
        private readonly ILoadingScreen _loadingScreen;

        public SceneSwitcherService(
            SceneLoaderService sceneLoaderService,
            ILoadingScreen loadingScreen)
        {
            _sceneLoaderService = sceneLoaderService;
            _loadingScreen = loadingScreen;
        }

        public async UniTask SwitchAsync(string sceneName, IInputSceneArgs sceneArgs = null)
        {
            _loadingScreen.Show();

            await _sceneLoaderService.LoadAsync(Scenes.Empty);
            await _sceneLoaderService.LoadAsync(sceneName);

            SceneBootstrap sceneBootstrap = Object.FindObjectOfType<SceneBootstrap>();

            if (sceneBootstrap == null)
                throw new NullReferenceException(
                    nameof(sceneBootstrap) + " not found on scene");

            sceneBootstrap.ProcessRegistrations(sceneArgs);

            await sceneBootstrap.Initialize();

            _loadingScreen.Hide();

            sceneBootstrap.Run();
        }
    }
}