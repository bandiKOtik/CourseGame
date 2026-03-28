using Assets.Scripts.Runtime.Gameplay.EntitiesCore;
using Assets.Scripts.Utilities.Reactive;
using Assets.Scripts.Utilities.StateMachineCore;
using UnityEngine;

namespace Assets.Scripts.Runtime.Gameplay.Features.AI.States
{
    public class TeleportToTargetWithCooldownState : State, IUpdateableState
    {
        private ReactiveVariable<float> _radius;
        private ReactiveEvent<Vector3> _found;
        private ReactiveVariable<Entity> _target;
        private Transform _transform;
        private float _cooldown;
        private float _time;

        public TeleportToTargetWithCooldownState(Entity entity, float cooldown)
        {
            _radius = entity.TeleportRadius;
            _found = entity.PositionFound;
            _target = entity.CurrentTarget;
            _transform = entity.Transform;
            _cooldown = cooldown;
        }

        public override void Enter()
        {
            base.Enter();
            _time = 0;
        }

        public void Update(float deltaTime)
        {
            _time += deltaTime;

            if (_time >= _cooldown)
            {
                _found.Invoke(GetNewPosition());
                _time = 0;
            }
        }

        private Vector3 GetNewPosition()
        {
            Vector3 direction = (_target.Value.Transform.position - _transform.position).normalized;
            Vector3 teleportPoint = _transform.position + direction * _radius.Value;

            return teleportPoint;
        }
    }
}
