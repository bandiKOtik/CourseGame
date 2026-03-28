using Assets.Scripts.Runtime.Gameplay.EntitiesCore;
using Assets.Scripts.Utilities.Reactive;
using Assets.Scripts.Utilities.StateMachineCore;
using UnityEngine;

namespace Assets.Scripts.Runtime.Gameplay.Features.AI.States
{
    public class RandomTeleportWithCooldownState : State, IUpdateableState
    {
        private ReactiveVariable<float> _radius;
        private ReactiveEvent<Vector3> _found;
        private float _cooldown;
        private float _time;

        public RandomTeleportWithCooldownState(Entity entity, float cooldown)
        {
            _radius = entity.TeleportRadius;
            _found = entity.PositionFound;
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
            float angle = Random.Range(0f, 360f);
            float distance = _radius.Value * Mathf.Sqrt(Random.Range(0f, 1f));

            float x = distance * Mathf.Cos(angle * Mathf.Deg2Rad);
            float z = distance * Mathf.Sin(angle * Mathf.Deg2Rad);

            return new(x, 0f, z);
        }
    }
}