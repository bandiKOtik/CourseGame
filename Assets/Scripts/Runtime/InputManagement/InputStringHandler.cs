using Assets.Scripts.Runtime.InputManagement;
using System;
using UnityEngine;

namespace Assets.Scripts.Utilities.InputManagement
{
    public class InputStringHandler : IInputHandler
    {
        public event Action<char> UserInput;

        public event Action CheckSequence;

        public event Action ClearValue;

        public event Action ExitToMenuRequest;

        public void Update() => ListenUserInput();

        private void ListenUserInput()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                ExitToMenuRequest?.Invoke();
                return;
            }

            foreach (char c in Input.inputString)
            {
                if ((c == '\n') || (c == '\r'))
                    CheckSequence?.Invoke();
                else if (c == '\b')
                {
                    ClearValue?.Invoke();
                }
                else
                    UserInput?.Invoke(c);
            }
        }
    }
}