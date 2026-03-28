using Assets.Scripts.Utilities.CoroutinesManagement;
using Assets.Scripts.Utilities.Reactive;
using System;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Utilities.Timer
{
    public class TimerService : IDisposable
    {
        private float _cooldown;
        private ReactiveEvent _cooldownEnded;

        private ReactiveVariable<float> _currentTime;

        private ICoroutinesPerformer _performer;
        private Coroutine _cooldownProcess;

        public TimerService(float cooldown, ICoroutinesPerformer performer)
        {
            _cooldown = cooldown;
            _performer = performer;

            _cooldownEnded = new();
            _currentTime = new();
        }

        public IReadOnlyEvent CooldownEnded => _cooldownEnded;
        public IReadOnlyVariable<float> CurrentTime => _currentTime;
        public bool IsOver => _currentTime.Value <= 0;

        public void Dispose() => Stop();

        public void Stop()
        {
            if (_cooldownProcess != null)
                _performer.StopPerform(_cooldownProcess);
        }

        public void Restart()
        {
            Stop();

            _cooldownProcess = _performer.StartPerform(CooldownProcess());
        }

        private IEnumerator CooldownProcess()
        {
            _currentTime.Value = _cooldown;

            while (IsOver == false)
            {
                _currentTime.Value -= Time.deltaTime;
                yield return null;
            }

            _cooldownEnded.Invoke();
        }
    }
}