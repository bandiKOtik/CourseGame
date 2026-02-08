using Assets.Scripts.Infrastructure.DI_Container;
using Assets.Scripts.Runtime.InputManagement;
using Assets.Scripts.Utilities.CoroutinesManagement;
using Assets.Scripts.Utilities.InputManagement;
using Assets.Scripts.Utilities.SceneManagement;
using Assets.Scripts.Utilities.Sequence;
using System;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Runtime.Infrastructure
{
    public class GameSession : IDisposable
    {
        public event Action Win;

        private string _sequence;

        private DIContainer _container;
        private SequenceMatcher _matcher;
        private GameplayInputHandler _inputHandler;

        public IEnumerator Initialize(DIContainer container)
        {
            _container = container;

            _matcher = _container.Resolve<SequenceMatcher>();

            var generator = _container.Resolve<ISequenceGenerator>();

            var inputHandler = _container.Resolve<IInputHandler>();

            if (inputHandler is not GameplayInputHandler _inputHandler)
                throw new ArgumentException(
                    nameof(inputHandler) + " is wrong handler for " + typeof(GameplayInputHandler));

            _inputHandler.UserAnswer += CheckAnswer;

            _sequence = generator.GenerateSequence();

            Debug.LogWarning(_sequence);

            yield break;
        }

        public void Dispose() => _inputHandler.UserAnswer -= CheckAnswer;

        private void CheckAnswer(string input)
        {
            if (_matcher.IsMatch(_sequence, input))
            {
                Debug.Log("And it's correct!");
                ProcessEndGame();
            }
            else
            {
                Debug.Log("And you was wrong.");
            }
        }

        private void ProcessEndGame()
        {
            Win?.Invoke();
            _container
                .Resolve<ICoroutinesPerformer>()
                .StartPerform(_container
                .Resolve<SceneSwitcherService>()
                .ProcessSwitchTo(Scenes.MainMenu));
        }
    }
}