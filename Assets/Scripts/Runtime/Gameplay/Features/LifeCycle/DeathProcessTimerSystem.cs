using Assets.Scripts.Runtime.Gameplay.EntitiesCore;
using Assets.Scripts.Runtime.Gameplay.EntitiesCore.Systems;
using Assets.Scripts.Utilities.Reactive;
using System;
using UnityEngine;

namespace Assets.Scripts.Runtime.Gameplay.Features.LifeCycle
{
    public class DeathProcessTimerSystem : IInitializableSystem, IUpdateableSystem
    {
        private IDisposable _subscription;

        private ReactiveVariable<bool> _isDead;
        private ReactiveVariable<bool> _inDeathProcess;
        private ReactiveVariable<float> _initialTime;
        private ReactiveVariable<float> _currentTime;

        public void OnInit(Entity entity)
        {
            _isDead = entity.IsDead;
            _inDeathProcess = entity.InDeathProcess;
            _initialTime = entity.DeathProcessInitialTime;
            _currentTime = entity.DeathProcessCurrentTime;

            _subscription = _isDead.Subscribe(OnIsDeadChanged);
        }

        public void OnUpdate(float deltaTime)
        {
            if (_inDeathProcess.Value == false)
                return;

            _currentTime.Value -= deltaTime;

            if (CooldownIsOver())
                _inDeathProcess.Value = false;
        }

        private void OnIsDeadChanged(bool oldValue, bool newValue)
        {
            if (newValue == true)
            {
                _currentTime.Value = _initialTime.Value;
                _inDeathProcess.Value = true;
                _subscription.Dispose();
            }
        }

        private bool CooldownIsOver() => _currentTime.Value <= 0;
    }
}
