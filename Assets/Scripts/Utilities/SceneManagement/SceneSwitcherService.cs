using Assets.Scripts.Infrastructure.ConfigsManagement.Bootstraps;
using Assets.Scripts.Utilities.LoadingScreen;
using Cysharp.Threading.Tasks;
using System;
using UnityEngine.SceneManagement;
using Zenject;
using Object = UnityEngine.Object;

namespace Assets.Scripts.Utilities.SceneManagement
{
    public class SceneSwitcherService
    {
        private readonly ZenjectSceneLoader _sceneLoaderService;
        private readonly ILoadingScreen _loadingScreen;

        public SceneSwitcherService(
            ZenjectSceneLoader sceneLoaderService,
            ILoadingScreen loadingScreen)
        {
            _sceneLoaderService = sceneLoaderService;
            _loadingScreen = loadingScreen;
        }

        public async UniTask SwitchAsync(string sceneName, IInputSceneArgs sceneArgs = null)
        {
            _loadingScreen.Show();

            await _sceneLoaderService.LoadSceneAsync(Scenes.Empty);
            await _sceneLoaderService.LoadSceneAsync(sceneName, LoadSceneMode.Single, container =>
            {
                if (sceneArgs != null)
                    container.Bind(sceneArgs.GetType()).FromInstance(sceneArgs).AsSingle();
            });

            SceneBootstrap sceneBootstrap = Object.FindObjectOfType<SceneBootstrap>();

            if (sceneBootstrap == null)
                throw new NullReferenceException(
                    nameof(sceneBootstrap) + " not found on scene");

            await sceneBootstrap.Initialize();

            _loadingScreen.Hide();

            sceneBootstrap.Run();
        }
    }
}