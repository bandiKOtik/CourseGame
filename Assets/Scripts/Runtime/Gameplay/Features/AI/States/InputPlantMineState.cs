using Assets.Scripts.Runtime.Gameplay.EntitiesCore;
using Assets.Scripts.Runtime.Gameplay.Features.Attack;
using Assets.Scripts.Runtime.Gameplay.Features.InputManagement;
using Assets.Scripts.Utilities.StateMachineCore;
using UnityEditor.PackageManager.Requests;
using UnityEngine;

namespace Assets.Scripts.Runtime.Gameplay.Features.AI.States
{
    public class InputPlantMineState : State, IUpdateableState
    {
        private readonly IInputService _inputService;

        public InputPlantMineState(Entity source, IInputService inputService)
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
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out var hit))
                    Debug.Log("Plant mine on: " + hit.point);

                //_request.Invoke();
            }
        }
    }
}
