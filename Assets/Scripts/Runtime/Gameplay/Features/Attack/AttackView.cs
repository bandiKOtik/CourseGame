using Assets.Scripts.Runtime.Gameplay.EntitiesCore;
using Assets.Scripts.Runtime.Gameplay.EntitiesCore.Mono;
using Assets.Scripts.Utilities.Reactive;
using System;
using UnityEngine;

namespace Assets.Scripts.Runtime.Gameplay.Features.Attack
{
    [RequireComponent(typeof(Animator))]
    public class AttackView : EntityView
    {
        private readonly int IsMovingKey = Animator.StringToHash("IsAttack");
        [SerializeField] private Animator _animator;
        private IReadOnlyVariable<bool> _isAttackProcess;
        private IDisposable _isAttackChangeDisposable;

        private void OnValidate()
        {
            _animator ??= GetComponent<Animator>();
        }

        protected override void OnEntityInitialize(Entity entity)
        {
            _isAttackProcess = entity.InAttackProcess;

            _isAttackChangeDisposable = _isAttackProcess.Subscribe(OnIsAttackProcessChanged);
            UpdateAttackProcess(_isAttackProcess.Value);
        }

        public override void CleanUp(Entity entity)
        {
            base.CleanUp(entity);

            _isAttackChangeDisposable.Dispose();
        }

        private void OnIsAttackProcessChanged(bool arg1, bool arg2) => UpdateAttackProcess(arg2);

        private void UpdateAttackProcess(bool value) => _animator.SetBool(IsMovingKey, value);
    }
}