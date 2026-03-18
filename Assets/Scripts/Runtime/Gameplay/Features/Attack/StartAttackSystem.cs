using Assets.Scripts.Runtime.Gameplay.EntitiesCore;
using Assets.Scripts.Runtime.Gameplay.EntitiesCore.Systems;
using Assets.Scripts.Utilities.Conditions;
using Assets.Scripts.Utilities.Reactive;
using System;

namespace Assets.Scripts.Runtime.Gameplay.Features.Attack
{
    public class StartAttackSystem : IInitializableSystem, IDisposableSystem
    {
        private ReactiveEvent _startAttakRequest;
        private ReactiveEvent _startAttackEvent;
        private ReactiveVariable<bool> _inProcess;
        private ICompositeCondition _canStartAttack;

        private IDisposable _subscription;

        public void OnInit(Entity entity)
        {
            _startAttakRequest = entity.StartAttackRequest;
            _startAttackEvent = entity.StartAttackEvent;
            _inProcess = entity.InAttackProcess;
            _canStartAttack = entity.CanStartAttack;

            _subscription = _startAttakRequest.Subscribe(OnAttackRequest);
        }

        public void OnDispose()
        {
            _subscription.Dispose();
        }

        private void OnAttackRequest()
        {
            if (_canStartAttack.Evaluate())
            {
                UnityEngine.Debug.Log("Start attack");
                _inProcess.Value = true;
                _startAttackEvent.Invoke();
            }
            else
            {
                UnityEngine.Debug.Log("Cannot attack");
            }
        }
    }
}