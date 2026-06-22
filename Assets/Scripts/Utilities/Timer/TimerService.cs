using Assets.Scripts.Utilities.Reactive;
using Cysharp.Threading.Tasks;
using System;
using System.Threading;
using UnityEngine;

namespace Assets.Scripts.Utilities.Timer
{
    public class TimerService : IDisposable
    {
        private float _cooldown;
        private ReactiveEvent _cooldownEnded;

        private ReactiveVariable<float> _currentTime;

        private CancellationTokenSource _cts = new();

        public TimerService(float cooldown)
        {
            _cooldown = cooldown;

            _cooldownEnded = new();
            _currentTime = new();
        }

        public IReadOnlyEvent CooldownEnded => _cooldownEnded;
        public IReadOnlyVariable<float> CurrentTime => _currentTime;
        public bool IsOver => _currentTime.Value <= 0;

        public void Dispose() => Stop();

        public void Stop()
        {
            _cts?.Cancel();
            _cts?.Dispose();
        }

        public void Restart()
        {
            Stop();
            _cts = new CancellationTokenSource();
            CooldownProcess(_cts.Token).Forget();
        }

        private async UniTask CooldownProcess(CancellationToken token)
        {
            _currentTime.Value = _cooldown;

            while (IsOver == false)
            {
                token.ThrowIfCancellationRequested();
                _currentTime.Value -= Time.deltaTime;
                await UniTask.Yield();
            }

            _cooldownEnded.Invoke();
        }
    }
}