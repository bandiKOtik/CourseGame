using Assets.Scripts.Infrastructure.ConfigsManagement.Bootstraps;
using Assets.Scripts.Infrastructure.DI_Container;
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

        //private DIContainer _currentSceneContainer;

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

            //_currentSceneContainer?.Dispose();

            await _sceneLoaderService.LoadAsync(Scenes.Empty);
            await _sceneLoaderService.LoadAsync(sceneName);

            SceneBootstrap sceneBootstrap = Object.FindObjectOfType<SceneBootstrap>();

            if (sceneBootstrap == null)
                throw new NullReferenceException(
                    nameof(sceneBootstrap) + " not found on scene");

            //sceneBootstrap.ProcessRegistrations(_currentSceneContainer, sceneArgs);

            //_currentSceneContainer.Initialize();

            await sceneBootstrap.Initialize();

            _loadingScreen.Hide();

            sceneBootstrap.Run();
        }
    }
}