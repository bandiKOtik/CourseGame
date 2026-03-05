using Assets.Scripts.Runtime.Gameplay.EntitiesCore;
using Assets.Scripts.Runtime.Gameplay.EntitiesCore.Systems;
using Assets.Scripts.Utilities.Reactive;
using UnityEngine;

namespace Assets.Scripts.Runtime.Gameplay.Features.RotationFeature
{
    public class RigidbodyRotationSystem : IInitializeableSystem, IUpdateableSystem
    {
        private ReactiveVariable<Vector3> _rotateDirection;
        private ReactiveVariable<float> _rotationSpeed;
        private Rigidbody _rigidbody;

        public void OnInit(Entity entity)
        {
            _rotateDirection = entity.RotationDirection;
            _rotationSpeed = entity.RotationSpeed;
            _rigidbody = entity.Rigidbody;
        }

        public void OnUpdate(float deltaTime)
        {
            Vector3 direction = _rotateDirection.Value.normalized;

            if (direction.magnitude > 0.1f)
            {
                Quaternion targetRotation = Quaternion.LookRotation(direction);
                Quaternion newRotation = Quaternion.RotateTowards(_rigidbody.rotation,
                    targetRotation, _rotationSpeed.Value * deltaTime);

                _rigidbody.MoveRotation(newRotation);
            }
        }
    }
}
