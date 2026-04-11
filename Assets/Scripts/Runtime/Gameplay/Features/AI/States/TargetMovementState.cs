using Assets.Scripts.Runtime.Gameplay.EntitiesCore;
using Assets.Scripts.Utilities.Reactive;
using Assets.Scripts.Utilities.StateMachineCore;
using UnityEngine;

namespace Assets.Scripts.Runtime.Gameplay.Features.AI.States
{
    public class TargetMovementState : State, IUpdateableState
    {
        private ReactiveVariable<Vector3> _movementDirection;
        private ReactiveVariable<Vector3> _rotationDirection;
        private ReactiveVariable<Entity> _moveTarget;
        private Transform _transform;

        public TargetMovementState(Entity entity)
        {
            _movementDirection = entity.MoveDirection;
            _rotationDirection = entity.RotationDirection;
            _moveTarget = entity.CurrentTarget;
            _transform = entity.Transform;
        }

        public override void Enter()
        {
            base.Enter();
        }

        public override void Exit()
        {
            base.Exit();

            _movementDirection.Value = Vector3.zero;
        }

        public void Update(float deltaTime)
        {
            Vector3 targetDirection = (_moveTarget.Value.Transform.position - _transform.position).normalized;
            _movementDirection.Value = targetDirection;
            _rotationDirection.Value = targetDirection;
        }
    }
}
