using Assets.Scripts.Runtime.Gameplay.EntitiesCore;
using Assets.Scripts.Runtime.Gameplay.EntitiesCore.Mono;
using Assets.Scripts.Utilities.Reactive;
using System;
using UnityEngine;

namespace Assets.Scripts.Runtime.Gameplay.Features.DamageFeature
{
    public class ApplyDamageView : EntityView
    {
        [SerializeField] private ParticleSystem _effectParticleSystem;
        [SerializeField] private Transform _transform;

        private ReactiveEvent<float> _damageEvent;
        private IDisposable _eventDisposable;

        protected override void OnEntityInitialize(Entity entity)
        {
            _damageEvent = entity.TakeDamageEvent;
            _eventDisposable = _damageEvent.Subscribe(OnDamaged);
        }

        public override void CleanUp(Entity entity)
        {
            base.CleanUp(entity);

            _eventDisposable.Dispose();
        }

        private void OnDamaged(float obj) => Instantiate(_effectParticleSystem, _transform.position, Quaternion.identity, null);
    }
}