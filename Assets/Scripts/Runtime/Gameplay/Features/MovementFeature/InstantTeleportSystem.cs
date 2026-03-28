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
        private ReactiveVariable<float> _currentEnergy;
        private ReactiveVariable<float> _energyPrice;
        private ReactiveEvent _achieved;
        private ReactiveEvent<Vector3> _found;

        private IDisposable _subscription;

        public void OnInit(Entity entity)
        {
            _transform = entity.Transform;
            _currentEnergy = entity.CurrentEnergy;
            _energyPrice = entity.TeleportEnergyPrice;
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
            if (_currentEnergy.Value - _energyPrice.Value < 0)
                return;

            _currentEnergy.Value -= _energyPrice.Value;
            _transform.position = position;
            _achieved.Invoke();
        }
    }
}