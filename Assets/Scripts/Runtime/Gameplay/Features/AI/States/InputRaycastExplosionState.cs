using Assets.Scripts.Runtime.Gameplay.EntitiesCore;
using Assets.Scripts.Runtime.Gameplay.Features.InputManagement;
using Assets.Scripts.Utilities.Reactive;
using Assets.Scripts.Utilities.StateMachineCore;
using UnityEngine;

namespace Assets.Scripts.Runtime.Gameplay.Features.AI.States
{
    public class InputRaycastExplosionState : State, IUpdateableState
    {
        private readonly IInputService _inputService;

        private ReactiveEvent _request;
        private ReactiveVariable<Vector3> _explosionPosition;

        public InputRaycastExplosionState(Entity source, IInputService inputService)
        {
            _inputService = inputService;
            _request = source.StartAttackRequest;
            _explosionPosition = source.ExplosionPosition;
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
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out var hit))
                {
                    _explosionPosition.Value = hit.point;
                    _request.Invoke();
                }
            }
        }
    }
}
