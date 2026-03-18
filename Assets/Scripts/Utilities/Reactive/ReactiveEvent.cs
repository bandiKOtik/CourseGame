using System;
using System.Collections.Generic;

namespace Assets.Scripts.Utilities.Reactive
{
    public class ReactiveEvent : IReadOnlyEvent
    {
        private readonly List<Subscriber> _subscribers = new();
        private readonly List<Subscriber> _toAdd = new();
        private readonly List<Subscriber> _toRemove = new();

        public IDisposable Subscribe(Action action)
        {
            Subscriber subscriber = new Subscriber(action, Remove);
            _toAdd.Add(subscriber);
            return subscriber;
        }

        private void Remove(Subscriber s)
            => _toRemove.Add(s);

        public void Invoke()
        {
            if (_toAdd.Count > 0)
            {
                _subscribers.AddRange(_toAdd);
                _toAdd.Clear();
            }

            if (_toRemove.Count > 0)
            {
                foreach (var s in _toRemove)
                    _subscribers.Remove(s);

                _toRemove.Clear();
            }

            foreach (var s in _subscribers)
                s.Invoke();
        }
    }

    public class ReactiveEvent<T> : IReadOnlyEvent<T>
    {
        private readonly List<Subscriber<T>> _subscribers = new();
        private readonly List<Subscriber<T>> _toAdd = new();
        private readonly List<Subscriber<T>> _toRemove = new();

        public IDisposable Subscribe(Action<T> action)
        {
            Subscriber<T> subscriber = new Subscriber<T>(action, Remove);
            _toAdd.Add(subscriber);
            return subscriber;
        }

        private void Remove(Subscriber<T> s)
            => _toRemove.Add(s);

        public void Invoke(T arg)
        {
            if (_toAdd.Count > 0)
            {
                _subscribers.AddRange(_toAdd);
                _toAdd.Clear();
            }

            if (_toRemove.Count > 0)
            {
                foreach (var s in _toRemove)
                    _subscribers.Remove(s);

                _toRemove.Clear();
            }

            foreach (var s in _subscribers)
                s.Invoke(arg);
        }
    }
}