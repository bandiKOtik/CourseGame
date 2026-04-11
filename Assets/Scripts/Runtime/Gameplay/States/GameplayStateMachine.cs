using Assets.Scripts.Utilities.StateMachineCore;
using System;
using System.Collections.Generic;

namespace Assets.Scripts.Runtime.Gameplay.States
{
    public class GameplayStateMachine : StateMachine<IUpdateableState>
    {
        public GameplayStateMachine() : base(new List<IDisposable>())
        {
        }

        public GameplayStateMachine(List<IDisposable> disposables) : base(disposables)
        {
        }

        protected override void UpdateLogic(float deltaTime)
        {
            base.UpdateLogic(deltaTime);

            CurrentState?.Update(deltaTime);
        }
    }
}
