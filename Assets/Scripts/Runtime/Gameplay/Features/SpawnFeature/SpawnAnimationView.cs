using Assets.Scripts.Runtime.Gameplay.EntitiesCore;
using Assets.Scripts.Runtime.Gameplay.EntitiesCore.Mono;
using Assets.Scripts.Utilities.Reactive;
using System;
using UnityEngine;

namespace Assets.Scripts.Runtime.Gameplay.Features.SpawnFeature
{
    [RequireComponent(typeof(Animator))]
    public class SpawnAnimationView : EntityView
    {
        private readonly int SpawningProcessKey = Animator.StringToHash("InSpawnProcess");

        [SerializeField] private Animator _animator;

        private IReadOnlyVariable<bool> _inSpawnProcess;

        private IDisposable _spawnProcessDisposable;

        private void OnValidate()
        {
            _animator ??= GetComponent<Animator>();
        }

        protected override void OnEntityInitialize(Entity entity)
        {
            _inSpawnProcess = entity.InSpawnProcess;

            _spawnProcessDisposable = _inSpawnProcess.Subscribe(OnSpawnProcessChanged);
            UpdateSpawnProcessKey(_inSpawnProcess.Value);
        }

        public override void CleanUp(Entity entity)
        {
            base.CleanUp(entity);

            _spawnProcessDisposable.Dispose();
        }

        private void OnSpawnProcessChanged(bool arg1, bool arg2) => UpdateSpawnProcessKey(arg2);

        private void UpdateSpawnProcessKey(bool value) => _animator.SetBool(SpawningProcessKey, value);
    }
}