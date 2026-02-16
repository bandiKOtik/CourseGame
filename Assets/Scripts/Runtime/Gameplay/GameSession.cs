using Assets.Scripts.Runtime.InputManagement;
using Assets.Scripts.Runtime.Sequence;
using Assets.Scripts.Utilities.CoroutinesManagement;
using Assets.Scripts.Utilities.DataManagement.DataProviders;
using Assets.Scripts.Utilities.InputManagement;
using Assets.Scripts.Utilities.SceneManagement;
using System;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Runtime.Gameplay
{
    public class GameSession : IDisposable
    {
        public event Action Win;

        public event Action Defeat;

        private string _sequence;
        private string _input = "";

        private ICoroutinesPerformer _performer;
        private SceneSwitcherService _sceneSwitcher;
        private SequenceMatcher _matcher;
        private InputStringHandler _inputHandler;
        private PlayerDataProvider _dataProvider;

        public IEnumerator Initialize(
            ICoroutinesPerformer performer,
            ISequenceGenerator generator,
            SceneSwitcherService sceneSwitcher,
            SequenceMatcher matcher,
            IInputHandler inputHandler,
            PlayerDataProvider dataProvider)
        {
            _performer = performer;
            _sceneSwitcher = sceneSwitcher;
            _matcher = matcher;
            _dataProvider = dataProvider;

            if (inputHandler is not InputStringHandler _inputHandler)
                throw new ArgumentException(
                    nameof(inputHandler) + " is wrong handler of type " + typeof(InputStringHandler));

            _inputHandler.UserInput += OnUserInput;
            _inputHandler.ClearValue += OnClearLastValue;
            _inputHandler.CheckSequence += OnCheckSequence;
            _inputHandler.ExitToMenuRequest += ReturnToMenu;

            _sequence = generator.GenerateSequence();

            Debug.LogWarning(_sequence);

            yield break;
        }

        public void Dispose()
        {
            _inputHandler.UserInput -= OnUserInput;
            _inputHandler.ClearValue -= OnClearLastValue;
            _inputHandler.CheckSequence -= OnCheckSequence;
            _inputHandler.ExitToMenuRequest -= ReturnToMenu;
        }

        private void OnUserInput(char toAdd)
        {
            _input += toAdd;

            Debug.Log("> " + _input);
        }

        private void OnClearLastValue()
        {
            if (_input.Length > 0)
                _input = _input.Substring(0, _input.Length - 1);

            Debug.Log("You wrote: " + _input);
        }

        private void OnCheckSequence()
        {
            Debug.Log("Your answer is: " + _input);

            if (_matcher.IsMatch(_sequence, _input))
                SessionWin();
            else
                SessionDefeat();

            _input = "";
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