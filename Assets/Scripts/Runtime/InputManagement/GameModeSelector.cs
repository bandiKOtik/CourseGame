using Assets.Scripts.Configs.Meta.GameModeConfigs;
using Assets.Scripts.Infrastructure.Gameplay;
using Assets.Scripts.Runtime.Gameplay;
using Assets.Scripts.Utilities.AssetsManagement;
using Assets.Scripts.Utilities.CoroutinesManagement;
using Assets.Scripts.Utilities.DataManagement.DataProviders;
using Assets.Scripts.Utilities.Factory;
using Assets.Scripts.Utilities.SceneManagement;
using UnityEngine;

namespace Assets.Scripts.Runtime.InputManagement
{
    public class GameModeSelector : IInputHandler
    {
        private ICoroutinesPerformer _performer;
        private SceneSwitcherService _sceneSwitcher;
        private PlayerDataProvider _playerDataProvider;

        private GameModeConfig _gameModeConfig;

        private GameMode _currentGamemode;
        private bool _requested = false;

        public GameModeSelector(
            ICoroutinesPerformer performer,
            SceneSwitcherService sceneSwitcher,
            PlayerDataProvider playerDataProvider,
            GameModeConfig gameModeConfig)
        {
            _performer = performer;
            _sceneSwitcher = sceneSwitcher;
            _playerDataProvider = playerDataProvider;
            _gameModeConfig = gameModeConfig;
        }

        public void Update() => ListenUserInput();

        private void ListenUserInput()
        {
            if (Input.GetKeyDown(KeyCode.R))
                _playerDataProvider.Reset();

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

                GameplayInputArgs args = new(_currentGamemode, _gameModeConfig);

                _performer
                    .StartPerform(_sceneSwitcher
                    .ProcessSwitchTo(Scenes.Gameplay, args));
            }
        }
    }
}