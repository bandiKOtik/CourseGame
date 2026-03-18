using Assets.Scripts.Runtime.InputManagement;
using Assets.Scripts.Utilities.CoroutinesManagement;
using Assets.Scripts.Utilities.DataManagement.DataProviders;
using Assets.Scripts.Utilities.InputManagement;
using Assets.Scripts.Utilities.SceneManagement;
using System;
using UnityEngine;

namespace Assets.Scripts.Runtime.Gameplay
{
    public class GameSession : IDisposable
    {
        public event Action Win;

        public event Action Defeat;

        private ICoroutinesPerformer _performer;
        private SceneSwitcherService _sceneSwitcher;
        private InputStringHandler _inputHandler;
        private PlayerDataProvider _dataProvider;

        public GameSession(
            ICoroutinesPerformer performer,
            SceneSwitcherService sceneSwitcher,
            IInputHandler inputHandler,
            PlayerDataProvider dataProvider)
        {
            _performer = performer;
            _sceneSwitcher = sceneSwitcher;
            _dataProvider = dataProvider;

            if (inputHandler is not InputStringHandler _inputHandler)
                throw new ArgumentException(
                    nameof(inputHandler) + " is wrong handler of type " + typeof(InputStringHandler));

            _inputHandler.CheckSequence += SessionWin;
            _inputHandler.ClearValue += SessionDefeat;
            _inputHandler.ExitToMenuRequest += ReturnToMenu;
        }

        public void Dispose()
        {
            if (_inputHandler != null)
            {
                _inputHandler.CheckSequence -= SessionWin;
                _inputHandler.ClearValue -= SessionDefeat;
                _inputHandler.ExitToMenuRequest -= ReturnToMenu;
            }
        }

        private void SessionWin()
        {
            Debug.Log("And it's <color=green>CORRECT</color>!");
            Win?.Invoke();
            ReturnToMenu();
        }

        private void SessionDefeat()
        {
            Debug.Log("And it's <color=red>WRONG</color>.");
            Defeat?.Invoke();
            ReturnToMenu();
        }

        private void ReturnToMenu()
        {
            _performer.StartPerform(_dataProvider.SaveAsync());

            _performer
                .StartPerform(_sceneSwitcher
                .ProcessSwitchTo(Scenes.MainMenu));
        }
    }
}