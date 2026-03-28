using Assets.Scripts.Utilities.StateMachineCore;
using System;
using System.Collections.Generic;

namespace Assets.Scripts.Runtime.Gameplay.Features.AI
{
    public class AIStateMachine : StateMachine<IUpdateableState>
    {
        public AIStateMachine() : base(new List<IDisposable>())
        {
        }

        public AIStateMachine(List<IDisposable> disposables) : base(disposables)
        {
        }

        protected override void UpdateLogic(float deltaTime)
        {
            base.UpdateLogic(deltaTime);

            CurrentState?.Update(deltaTime);
        }
    }
}