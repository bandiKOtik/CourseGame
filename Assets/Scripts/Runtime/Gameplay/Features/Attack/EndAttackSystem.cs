using Assets.Scripts.Runtime.Gameplay.EntitiesCore;
using Assets.Scripts.Runtime.Gameplay.EntitiesCore.Systems;
using Assets.Scripts.Utilities.Reactive;
using System;

namespace Assets.Scripts.Runtime.Gameplay.Features.Attack
{
    public class EndAttackSystem : IInitializableSystem, IDisposableSystem
    {
        private ReactiveEvent _endAttackEvent;
        private ReactiveVariable<bool> _inAttackProcess;
        private ReactiveVariable<float> _attackProcessInitialTime;
        private ReactiveVariable<float> _attackProcessCurrentTime;

        private IDisposable _subscription;

        public void OnInit(Entity entity)
        {
            _endAttackEvent = entity.EndAttackEvent;
            _inAttackProcess = entity.InAttackProcess;
            _attackProcessCurrentTime = entity.AttackProcessCurrentTime;
            _attackProcessInitialTime = entity.AttackProcessInitialTime;

            _subscription = _attackProcessCurrentTime.Subscribe(OnTimerChanged);
        }

        public void OnDispose()
        {
            _subscription.Dispose();
        }

        private void OnTimerChanged(float oldValue, float newValue)
        {
            if (TimeIsDone(newValue))
            {
                UnityEngine.Debug.Log("End of attack");
                _inAttackProcess.Value = false;
                _endAttackEvent.Invoke();
            }
        }

        private bool TimeIsDone(float value) => value >= _attackProcessInitialTime.Value;
    }
}
