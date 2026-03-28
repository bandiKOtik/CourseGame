using Assets.Scripts.Runtime.Gameplay.EntitiesCore;
using Assets.Scripts.Utilities.Reactive;
using Assets.Scripts.Utilities.StateMachineCore;
using UnityEngine;

namespace Assets.Scripts.Runtime.Gameplay.Features.AI.States
{
    public class RandomMovementState : State, IUpdateableState
    {
        private ReactiveVariable<Vector3> _movementDirection;
        private ReactiveVariable<Vector3> _rotationDirection;
        private float _cooldown;
        private float _time;

        public RandomMovementState(Entity entity, float cooldown)
        {
            _movementDirection = entity.MoveDirection;
            _rotationDirection = entity.RotationDirection;
            _cooldown = cooldown;
        }

        public override void Enter()
        {
            base.Enter();

            Vector3 randomDirection = new Vector3(Random.Range(-1f, 1), 0, Random.Range(-1f, 1)).normalized;
            _movementDirection.Value = randomDirection;
            _rotationDirection.Value = randomDirection;

            _time = 0;
        }

        public override void Exit()
        {
            base.Exit();

            _movementDirection.Value = Vector3.zero;
        }

        public void Update(float deltaTime)
        {
            _time += deltaTime;

            if (_time >= _cooldown)
            {
                GenerateNewDirection();
                _time = 0;
            }
        }

        private void GenerateNewDirection()
        {
            Vector3 inverseDirection = -_movementDirection.Value.normalized;
            Quaternion randomTurn = Quaternion.Euler(0, Random.Range(-30, 30), 0);
            Vector3 newDirection = randomTurn * inverseDirection;

            _movementDirection.Value = newDirection;
            _rotationDirection.Value = newDirection;
        }
    }
}