using Assets.Scripts.Runtime.Gameplay.Features.InputManagement;
using Assets.Scripts.Utilities.StateMachineCore;
using UnityEngine;

namespace Assets.Scripts.Runtime.Gameplay.Features.AI.States
{
    public class InputRaycastExplosionState : State, IUpdateableState
    {
        private readonly IInputService _inputService;

        public InputRaycastExplosionState(IInputService inputService)
        {
            _inputService = inputService;
        }

        public override void Enter()
        {
            base.Enter();
            Debug.Log("Shoot state enter");
        }

        public void Update(float deltaTime)
        {
            if (_inputService.AttackRequest)
            {
                // main camera raycast

                // explosionAttackRequest.Invoke, explosionPosition(raycast)

                Debug.Log("Explosion attack");
            }
        }
    }
}
