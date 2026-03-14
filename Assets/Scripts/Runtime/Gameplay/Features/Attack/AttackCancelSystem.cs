using Assets.Scripts.Runtime.Gameplay.EntitiesCore;
using Assets.Scripts.Runtime.Gameplay.EntitiesCore.Systems;
using Assets.Scripts.Utilities.Conditions;
using Assets.Scripts.Utilities.Reactive;
using UnityEngine;

namespace Assets.Scripts.Runtime.Gameplay.Features.Attack
{
    public class AttackCancelSystem : IInitializableSystem, IUpdateableSystem
    {
        private ReactiveVariable<bool> _inAtackProcess;
        private ReactiveEvent _attackCanceledEvent;
        private ICompositeCondition _mustCancel;

        public void OnInit(Entity entity)
        {
            _inAtackProcess = entity.InAttackProcess;
            _attackCanceledEvent = entity.AttackCanceledEvent;
            _mustCancel = entity.MustCancelAttack;
        }

        public void OnUpdate(float deltaTime)
        {
            if (_inAtackProcess.Value == false)
                return;

            if (_mustCancel.Evaluate())
            {
                Debug.Log("Attack canceled");
                _inAtackProcess.Value = false;
                _attackCanceledEvent.Invoke();
            }
        }
    }
}
