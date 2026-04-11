using Assets.Scripts.Runtime.Gameplay.Features.StagesFeature;
using Assets.Scripts.Utilities.StateMachineCore;
using UnityEngine;

namespace Assets.Scripts.Runtime.Gameplay.States
{
    public class PreperationState : State, IUpdateableState
    {
        private readonly PreperationTriggerService _triggerService;

        public PreperationState(PreperationTriggerService triggerService)
        {
            _triggerService = triggerService;
        }

        public override void Enter()
        {
            base.Enter();

            Vector3 nextPosition = Vector3.back * 4;
            _triggerService.Create(nextPosition);
        }

        public void Update(float deltaTime)
        {
            if (Input.GetKeyDown(KeyCode.Space))
                _triggerService.HasContact.Value = true;
        }

        public override void Exit()
        {
            base.Exit();

            _triggerService.CleanUp();
        }
    }
}
