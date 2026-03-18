using Assets.Scripts.Runtime.Gameplay.EntitiesCore;
using Assets.Scripts.Runtime.Gameplay.EntitiesCore.Systems;
using Assets.Scripts.Utilities.Reactive;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Runtime.Gameplay.Features.LifeCycle
{
    public class DisableCollidersOnDeathSystem : IInitializableSystem, IDisposableSystem
    {
        private IDisposable _subscription;

        private List<Collider> _colliders;
        private ReactiveVariable<bool> _isDead;

        public void OnInit(Entity entity)
        {
            _colliders = entity.DisableCollidersOnDeath;
            _isDead = entity.IsDead;

            _subscription = _isDead.Subscribe(OnIsDeadChanged);
        }

        public void OnDispose()
        {
            _subscription.Dispose();
        }

        private void OnIsDeadChanged(bool oldValue, bool newValue)
        {
            if (newValue)
                foreach (Collider collider in _colliders)
                    collider.enabled = false;
        }
    }
}