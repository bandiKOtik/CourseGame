using Assets.Scripts.Runtime.Gameplay.EntitiesCore;
using Assets.Scripts.Runtime.Gameplay.EntitiesCore.Systems;
using Assets.Scripts.Utilities.Reactive;
using System;
using UnityEngine;

namespace Assets.Scripts.Runtime.Gameplay.Features.MovementFeature
{
    public class TeleportPositionPickSystem : IInitializableSystem, IDisposableSystem
    {
        private ReactiveEvent _request;
        private ReactiveVariable<float> _radius;
        private ReactiveEvent<Vector3> _found;

        private IDisposable _subscription;

        public void OnInit(Entity entity)
        {
            _request = entity.TeleportRequest;
            _radius = entity.TeleportRadius;
            _found = entity.PositionFound;

            _subscription = _request.Subscribe(GetNewPosition);
        }

        public void OnDispose()
        {
            _subscription.Dispose();
        }

        private void GetNewPosition()
        {
            Vector3 position;

            float angle = UnityEngine.Random.Range(0f, 360f);
            float distance = _radius.Value * Mathf.Sqrt(UnityEngine.Random.Range(0f, 1f));

            float x = distance * Mathf.Cos(angle * Mathf.Deg2Rad);
            float z = distance * Mathf.Sin(angle * Mathf.Deg2Rad);

            position = new (x, 0f, z);

            //Debug.Log(position);
            _found.Invoke(position);
        }
    }
}