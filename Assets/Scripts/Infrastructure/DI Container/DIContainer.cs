using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Infrastructure.DI_Container
{
    public class DIContainer
    {
        private readonly Dictionary<Type, Registration> _container = new();

        private readonly List<Type> _requests = new();

        private readonly DIContainer _parent;

        public DIContainer() : this(null)
        {
        }

        public DIContainer(DIContainer parent) => _parent = parent;

        public IRegistrationOptions RegisterAsSingle<T>(Func<DIContainer, T> creator)
        {
            if (IsRegister<T>())
                throw new InvalidOperationException(typeof(T) + " is already register");

            Registration registration = new(container => creator.Invoke(container));

            _container.Add(typeof(T), registration);
            return registration;
        }

        public bool IsRegister<T>()
        {
            if (_container.ContainsKey(typeof(T)))
                return true;

            if (_parent != null)
                return _parent.IsRegister<T>();

            return false;
        }

        public T Resolve<T>()
        {
            if (_requests.Contains(typeof(T)))
                throw new InvalidOperationException($"Cycle resolve for {typeof(T)}");

            _requests.Add(typeof(T));

            try
            {
                if (_container.TryGetValue(typeof(T), out Registration registration))
                    return (T)registration.CreateInstanceFrom(this);

                if (_parent != null)
                    return _parent.Resolve<T>();
            }
            finally
            {
                _requests.Remove(typeof(T));
            }

            throw new InvalidOperationException($"Registration for {typeof(T)} not exists");
        }

        public void Initialize()
        {
            foreach (var registration in _container.Values)
            {
                if (registration.IsNonLazy == true)
                    registration.CreateInstanceFrom(this);

                registration.OnInitialize();
            }
        }

        public void Dispose()
        {
            foreach (var registration in _container.Values)
                registration.OnDispose();
        }
    }
}