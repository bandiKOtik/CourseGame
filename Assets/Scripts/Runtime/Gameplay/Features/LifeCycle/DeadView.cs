using Assets.Scripts.Runtime.Gameplay.EntitiesCore;
using Assets.Scripts.Runtime.Gameplay.EntitiesCore.Mono;
using Assets.Scripts.Utilities.Reactive;
using System;
using UnityEngine;

namespace Assets.Scripts.Runtime.Gameplay.Features.LifeCycle
{
    [RequireComponent(typeof(Animator))]
    public class DeadView : EntityView
    {
        private readonly int IsMovingKey = Animator.StringToHash("IsDead");
        [SerializeField] private Animator _animator;
        private IReadOnlyVariable<bool> _isDead;
        private IDisposable _isDeadChangeDisposable;

        private void OnValidate()
        {
            _animator ??= GetComponent<Animator>();
        }

        protected override void OnEntityInitialize(Entity entity)
        {
            _isDead = entity.IsDead;

            _isDeadChangeDisposable = _isDead.Subscribe(OnIsDeadChanged);
            UpdateIsDead(_isDead.Value);
        }

        public override void CleanUp(Entity entity)
        {
            base.CleanUp(entity);

            _isDeadChangeDisposable.Dispose();
        }

        private void OnIsDeadChanged(bool arg1, bool arg2) => UpdateIsDead(arg2);

        private void UpdateIsDead(bool value) => _animator.SetBool(IsMovingKey, value);
    }
}