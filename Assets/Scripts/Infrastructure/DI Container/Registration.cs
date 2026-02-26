using System;

namespace Assets.Scripts.Infrastructure.DI_Container
{
    public class Registration : IRegistrationOptions
    {
        private Func<DIContainer, object> _creator;
        private object _cachedInstance;

        public Registration(Func<DIContainer, object> creator) => _creator = creator;

        public bool IsNonLazy { get; private set; }

        public object CreateInstanceFrom(DIContainer container)
        {
            if (_cachedInstance != null)
                return _cachedInstance;

            if (_creator == null)
                throw new InvalidOperationException("No instance or creator");

            _cachedInstance = _creator.Invoke(container);

            return _cachedInstance;
        }

        public void OnInitialize()
        {
            if (_cachedInstance != null)
                if (_cachedInstance is IInitializable initializeable)
                    initializeable.Initialize();
        }

        public void OnDispose()
        {
            if (_cachedInstance != null)
                if (_cachedInstance is IDisposable disposable)
                    disposable.Dispose();
        }

        public void NonLazy() => IsNonLazy = true;
    }
}