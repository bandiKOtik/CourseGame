using Assets.Scripts.Runtime.Gameplay.Features.InputManagement;
using Assets.Scripts.Utilities.StateMachineCore;
using UnityEngine;

namespace Assets.Scripts.Runtime.Gameplay.Features.AI.States
{
    public class InputPlantMineState : State, IUpdateableState
    {
        private readonly IInputService _inputService;

        public InputPlantMineState(IInputService inputService)
        {
            _inputService = inputService;
        }

        public override void Enter()
        {
            base.Enter();

            Debug.Log("Mine state enter");
        }
        public void Update(float deltaTime)
        {
            if (_inputService.AttackRequest)
            {
                Debug.Log("Mine attack");
            }
        }
    }
}
