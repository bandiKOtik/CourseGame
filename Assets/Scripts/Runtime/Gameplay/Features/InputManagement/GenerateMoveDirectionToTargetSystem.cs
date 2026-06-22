using Assets.Scripts.Runtime.Gameplay.EntitiesCore;
using Assets.Scripts.Runtime.Gameplay.EntitiesCore.Systems;
using Assets.Scripts.Utilities.Reactive;
using UnityEngine;

namespace Assets.Scripts.Runtime.Gameplay.Features.InputManagement
{
    public class GenerateMoveDirectionToTargetSystem : IInitializableSystem, IUpdateableSystem
    {
        private ReactiveVariable<Entity> _target;
        private Transform _transform;
        private ReactiveVariable<Vector3> _moveDirection;

        public void OnInit(Entity entity)
        {
            _target = entity.CurrentTarget;
            _transform = entity.Transform;
            _moveDirection = entity.MoveDirection;
        }

        public void OnUpdate(float deltaTime)
        {
            if (_target.Value != null)
                _moveDirection.Value = _target.Value.Transform.position - _transform.position;
            else
                _moveDirection.Value = Vector3.zero;
        }
    }
}