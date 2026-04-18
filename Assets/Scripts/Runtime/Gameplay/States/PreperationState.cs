using Assets.Scripts.Runtime.Gameplay.Features.StagesFeature;
using Assets.Scripts.Utilities.StateMachineCore;
using UnityEngine;

namespace Assets.Scripts.Runtime.Gameplay.States
{
    public class PreperationState : State, IUpdateableState
    {
        private readonly PreperationInputService _preperation;

        public PreperationState(PreperationInputService preperation)
        {
            _preperation = preperation;
        }

        public void Update(float deltaTime)
        {
            if (Input.GetKeyDown(KeyCode.Space))
                _preperation.SetReady();
        }

        public override void Exit()
        {
            base.Exit();

            _preperation.CleanUp();
        }
    }
}
