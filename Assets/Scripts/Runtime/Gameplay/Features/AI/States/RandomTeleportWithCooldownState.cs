using Assets.Scripts.Runtime.Gameplay.EntitiesCore;
using Assets.Scripts.Utilities.Reactive;
using Assets.Scripts.Utilities.StateMachineCore;
using Assets.Scripts.Utilities.Timer;
using UnityEngine;

namespace Assets.Scripts.Runtime.Gameplay.Features.AI.States
{
    public class RandomTeleportWithCooldownState : State, IUpdateableState
    {
        private ReactiveVariable<float> _radius;
        private ReactiveEvent<Vector3> _found;
        private TimerService _timer;

        public RandomTeleportWithCooldownState(Entity entity, TimerService timer)
        {
            _radius = entity.TeleportRadius;
            _found = entity.PositionFound;
            _timer = timer;
        }

        public override void Enter()
        {
            base.Enter();

            _found.Invoke(GetNewPosition());

            _timer.Restart();
        }

        public void Update(float deltaTime)
        {
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