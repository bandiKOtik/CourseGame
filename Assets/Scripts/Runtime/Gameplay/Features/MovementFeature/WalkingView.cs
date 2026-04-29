using Assets.Scripts.Runtime.Gameplay.EntitiesCore;
using Assets.Scripts.Runtime.Gameplay.EntitiesCore.Mono;
using Assets.Scripts.Utilities.Reactive;
using System;
using UnityEngine;

namespace Assets.Scripts.Runtime.Gameplay.Features.MovementFeature
{
    [RequireComponent(typeof(Animator))]
    public class WalkingView : EntityView
    {
        private readonly int IsMovingKey = Animator.StringToHash("IsMoving");
        [SerializeField] private Animator _animator;
        private IReadOnlyVariable<bool> _isMoving;
        private IDisposable _isMovingChangeDisposable;

        private void OnValidate()
        {
            _animator ??= GetComponent<Animator>();
        }

        protected override void OnEntityInitialize(Entity entity)
        {
            _isMoving = entity.IsMoving;

            _isMovingChangeDisposable = _isMoving.Subscribe(OnIsMovingChanged);
            UpdateIsMoving(_isMoving.Value);
        }

        public override void CleanUp(Entity entity)
        {
            base.CleanUp(entity);

            _isMovingChangeDisposable.Dispose();
        }

        private void OnIsMovingChanged(bool arg1, bool arg2) => UpdateIsMoving(arg2);

        private void UpdateIsMoving(bool value) => _animator.SetBool(IsMovingKey, value);
    }
}