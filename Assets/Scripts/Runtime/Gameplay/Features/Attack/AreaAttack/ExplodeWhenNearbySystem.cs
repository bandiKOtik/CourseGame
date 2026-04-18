using Assets.Scripts.Runtime.Gameplay.EntitiesCore;
using Assets.Scripts.Runtime.Gameplay.EntitiesCore.Systems;
using Assets.Scripts.Utilities.Reactive;
using UnityEngine;

namespace Assets.Scripts.Runtime.Gameplay.Features.Attack.AreaAttack
{
    public class ExplodeWhenNearbySystem : IInitializableSystem, IUpdateableSystem
    {
        private Transform _transform;
        private ReactiveVariable<Entity> _target;
        private ReactiveVariable<float> _radius;
        private ReactiveVariable<Vector3> _position;
        private ReactiveEvent _attackRequest;

        private bool _triggered;

        public void OnInit(Entity entity)
        {
            _transform = entity.Transform;
            _target = entity.CurrentTarget;
            _radius = entity.NearbyAttackTriggerRadius;
            _attackRequest = entity.StartAttackRequest;
            _position = entity.ExplosionPosition;
        }

        public void OnUpdate(float deltaTime)
        {
            if (_target.Value == null)
                return;

            if (_target.Value.TryGetTransform(out var targetTransform))
            {
                var distance = Vector3.Distance(_transform.position, targetTransform.position);

                if (distance < _radius.Value && _triggered == false)
                {
                    _position.Value = _transform.position;
                    _attackRequest.Invoke();
                    _triggered = true;
                }
            }
        }
    }
}
