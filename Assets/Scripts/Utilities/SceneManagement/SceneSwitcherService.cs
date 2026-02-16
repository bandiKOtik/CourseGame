using Assets.Scripts.Infrastructure.ConfigsManagement.Bootstraps;
using Assets.Scripts.Infrastructure.DI_Container;
using Assets.Scripts.Utilities.LoadingScreen;
using System;
using System.Collections;
using Object = UnityEngine.Object;

namespace Assets.Scripts.Utilities.SceneManagement
{
    public class SceneSwitcherService
    {
        private readonly DIContainer _projectContainer;
        private readonly SceneLoaderService _sceneLoaderService;
        private readonly ILoadingScreen _loadingScreen;

        public SceneSwitcherService(
            DIContainer projectContainer,
            SceneLoaderService sceneLoaderService,
            ILoadingScreen loadingScreen)
        {
            _projectContainer = projectContainer;
            _sceneLoaderService = sceneLoaderService;
            _loadingScreen = loadingScreen;
        }

        public IEnumerator ProcessSwitchTo(string sceneName, IInputSceneArgs sceneArgs = null)
        {
            _loadingScreen.Show();

            yield return _sceneLoaderService.LoadAsync(Scenes.Empty);

            yield return _sceneLoaderService.LoadAsync(sceneName);

            SceneBootstrap sceneBootstrap = Object.FindObjectOfType<SceneBootstrap>();

            if (sceneBootstrap == null)
                throw new NullReferenceException(
                    nameof(sceneBootstrap) + " not found on scene");

            DIContainer sceneContainer = new(_projectContainer);

            sceneBootstrap.ProcessRegistrations(sceneContainer, sceneArgs);

<<<<<<< Updated upstream
=======
            sceneContainer.Initialize();

>>>>>>> Stashed changes
            yield return sceneBootstrap.Initialize();

            _loadingScreen.Hide();

            sceneBootstrap.Run();
        }
    }
}