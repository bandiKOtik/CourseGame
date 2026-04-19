using Assets.Scripts.Runtime.Gameplay.EntitiesCore;
using Assets.Scripts.Utilities.Reactive;
using Assets.Scripts.Utilities.StateMachineCore;
using Assets.Scripts.Utilities.Timer;
using UnityEngine;

namespace Assets.Scripts.Runtime.Gameplay.Features.AI.States
{
    public class TeleportToTargetWithCooldownState : State, IUpdateableState
    {
        private ReactiveVariable<Entity> _target;
        private ReactiveVariable<float> _radius;
        private ReactiveEvent<Vector3> _found;
        private Transform _transform;
        private TimerService _timer;

        public TeleportToTargetWithCooldownState(Entity entity, TimerService timer)
        {
            _target = entity.CurrentTarget;
            _radius = entity.TeleportRadius;
            _found = entity.PositionFound;
            _transform = entity.Transform;
            _timer = timer;
        }

        public override void Enter()
        {
            base.Enter();

            if (_target != null)
                _found.Invoke(GetNewPosition());

            _timer.Restart();
        }

        public void Update(float deltaTime)
        {
        }

        private Vector3 GetNewPosition()
        {
            if (_target.Value == null)
                return _transform.position;

            Vector3 direction = (_target.Value.Transform.position - _transform.position).normalized;
            Vector3 teleportPoint = _transform.position + direction * _radius.Value;

            return teleportPoint;
        }
    }
}
