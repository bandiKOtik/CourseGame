using Assets.Scripts.Runtime.Gameplay.EntitiesCore;
using Assets.Scripts.Runtime.Gameplay.EntitiesCore.Mono;
using Assets.Scripts.Utilities.Reactive;
using System;
using UnityEngine;

namespace Assets.Scripts.Runtime.Gameplay.Features.SpawnFeature
{
    public class SpawnEffectView : EntityView
    {
        [SerializeField] private ParticleSystem _spawnParticle;
        [SerializeField] private Transform _effectTransform;

        private IReadOnlyVariable<bool> _inSpawnProcess;
        private Transform _transform;

        private IDisposable _spawnProcessDisposable;

        protected override void OnEntityInitialize(Entity entity)
        {
            _inSpawnProcess = entity.InSpawnProcess;
            _transform = entity.Transform;

            _spawnProcessDisposable = _inSpawnProcess.Subscribe(OnSpawnProcessChanged);
            UpdateSpawnProcessKey(_inSpawnProcess.Value);
        }

        public override void CleanUp(Entity entity)
        {
            base.CleanUp(entity);

            _spawnProcessDisposable.Dispose();
        }

        private void OnSpawnProcessChanged(bool arg1, bool arg2) => UpdateSpawnProcessKey(arg2);

        private void UpdateSpawnProcessKey(bool value)
        {
            if (value)
            {
                if (_effectTransform == null)
                    _effectTransform = _transform;

                Instantiate(_spawnParticle, _effectTransform.position, Quaternion.identity, null);
            }
        }
    }
}