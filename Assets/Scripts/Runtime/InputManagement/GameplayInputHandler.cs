using Assets.Scripts.Infrastructure.DI_Container;
using Assets.Scripts.Runtime.InputManagement;
using Assets.Scripts.Utilities.CoroutinesManagement;
using Assets.Scripts.Utilities.SceneManagement;
using System;
using UnityEngine;

namespace Assets.Scripts.Utilities.InputManagement
{
    public class GameplayInputHandler : IInputHandler
    {
        public event Action<string> UserAnswer;

        private DIContainer _container;
        private string _input;

        public GameplayInputHandler(DIContainer container)
        {
            _container = container;
        }

        public void Update() => ListenUserInput();

        private void ListenUserInput()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                _container
                    .Resolve<ICoroutinesPerformer>()
                    .StartPerform(_container
                    .Resolve<SceneSwitcherService>()
                    .ProcessSwitchTo(Scenes.MainMenu));
                return;
            }

            foreach (char c in Input.inputString)
            {
                if ((c == '\n') || (c == '\r'))
                {
                    Debug.Log("Your answer is: " + _input);
                    UserAnswer?.Invoke(_input);
                    _input = "";
                }
                else
                {
                    _input += c;
                }
            }
        }
    }
}