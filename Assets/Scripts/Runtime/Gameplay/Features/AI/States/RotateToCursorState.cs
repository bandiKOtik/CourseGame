using Assets.Scripts.Runtime.Gameplay.EntitiesCore;
using Assets.Scripts.Utilities.Reactive;
using Assets.Scripts.Utilities.StateMachineCore;
using UnityEngine;

namespace Assets.Scripts.Runtime.Gameplay.Features.AI.States
{
    public class RotateToCursorState : State, IUpdateableState
    {
        private ReactiveVariable<Vector3> _direction;
        private Transform _transform;

        public RotateToCursorState(Entity entity)
        {
            _direction = entity.RotationDirection;
            _transform = entity.Transform;
        }

        public void Update(float deltaTime)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Plane groundPlane = new Plane(Vector3.up, Vector3.zero);

            if (groundPlane.Raycast(ray, out float enter))
            {
                Vector3 targetPoint = ray.GetPoint(enter);
                Vector3 direction = targetPoint - _transform.position;
                direction.y = 0;

                if (direction != Vector3.zero)
                    _direction.Value = direction;
            }
        }
    }
}
