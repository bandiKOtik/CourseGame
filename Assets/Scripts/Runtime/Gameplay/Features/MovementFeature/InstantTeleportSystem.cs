using Assets.Scripts.Runtime.Gameplay.EntitiesCore;
using Assets.Scripts.Runtime.Gameplay.EntitiesCore.Systems;
using Assets.Scripts.Utilities.Reactive;
using System;
using UnityEngine;

namespace Assets.Scripts.Runtime.Gameplay.Features.MovementFeature
{
    public class InstantTeleportSystem : IInitializableSystem, IDisposableSystem
    {
        private Transform _transform;
        private ReactiveVariable<float> _current;
        private ReactiveVariable<float> _price;
        private ReactiveEvent _achieved;
        private ReactiveEvent<Vector3> _found;

        private IDisposable _subscription;

        public void OnInit(Entity entity)
        {
            _transform = entity.Transform;
            _current = entity.CurrentEnergy;
            _price = entity.TeleportEnergyPrice;
            _achieved = entity.TeleportDestinationAchieved;
            _found = entity.PositionFound;

            _subscription = _found.Subscribe(OnPositionFound);
        }

        public void OnDispose()
        {
            _subscription.Dispose();
        }

        private void OnPositionFound(Vector3 position)
        {
            _current.Value -= _price.Value;
            _transform.position = position;
            _achieved.Invoke();
        }
    }
}