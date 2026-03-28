using Assets.Scripts.Runtime.Gameplay.EntitiesCore;
using Assets.Scripts.Utilities.Reactive;
using Assets.Scripts.Utilities.StateMachineCore;
using UnityEngine;

namespace Assets.Scripts.Runtime.Gameplay.Features.AI.States
{
    public class RotateToTargetState : State, IUpdateableState
    {
        private ReactiveVariable<Vector3> _direction;
        private ReactiveVariable<Entity> _target;
        private Transform _transform;

        public RotateToTargetState(Entity entity)
        {
            _direction = entity.RotationDirection;
            _target = entity.CurrentTarget;
            _transform = entity.Transform;
        }

        public void Update(float deltaTime)
        {
            if (_target.Value != null)
                _direction.Value = (_target.Value.Transform.position - _transform.position).normalized;
        }
    }
}