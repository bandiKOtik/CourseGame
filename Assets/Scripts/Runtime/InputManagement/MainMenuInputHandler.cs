using Assets.Scripts.Infrastructure.DI_Container;
using Assets.Scripts.Infrastructure.Gameplay;
using Assets.Scripts.Runtime.Gameplay;
using Assets.Scripts.Utilities.AssetsManagement;
using Assets.Scripts.Utilities.CoroutinesManagement;
using Assets.Scripts.Utilities.Factory;
using Assets.Scripts.Utilities.SceneManagement;
using UnityEngine;

namespace Assets.Scripts.Runtime.InputManagement
{
    public class MainMenuInputHandler : IInputHandler
    {
        private DIContainer _container;
        private SceneSwitcherService _sceneSwitcher;
        private ResourcesAssetsLoader _resourcesLoader;
        private GameModeConfigFactory _gamemodeFactory = new();

        private GameMode _currentGamemode;
        private bool _requested = false;

        public MainMenuInputHandler(DIContainer container)
        {
            _container = container;

            _sceneSwitcher = _container.Resolve<SceneSwitcherService>();

            _resourcesLoader = _container.Resolve<ResourcesAssetsLoader>();
        }

        public void Update() => ListenUserInput();

        private void ListenUserInput()
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                _currentGamemode = GameMode.Numbers;
                _requested = true;
            }

            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                _currentGamemode = GameMode.Letters;
                _requested = true;
            }

            if (_requested)
            {
                _requested = false;

                GameplayInputArgs args = new(_gamemodeFactory.Create(
                    _container,
                    _currentGamemode));

                _container
                    .Resolve<ICoroutinesPerformer>()
                    .StartPerform(_sceneSwitcher
                    .ProcessSwitchTo(Scenes.Gameplay, args));
            }
        }
    }
}