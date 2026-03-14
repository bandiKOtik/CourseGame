using Assets.Scripts.Runtime.Gameplay.EntitiesCore.Systems;
using System;
using System.Collections.Generic;

namespace Assets.Scripts.Runtime.Gameplay.EntitiesCore
{
    public partial class Entity : IDisposable
    {
        private readonly Dictionary<Type, IEntityComponent> _components = new();

        private readonly List<IEntitySystem> _systems = new();

        private readonly List<IUpdateableSystem> _updateables = new();
        private readonly List<IInitializableSystem> _initializeables = new();
        private readonly List<IDisposableSystem> _disposables = new();

        private bool _initialized = false;

        public void Initialize()
        {
            foreach (var initable in _initializeables)
                initable.OnInit(this);

            _initialized = true;
        }

        public void OnUpdate(float deltaTime)
        {
            if (_initialized == false)
                return;

            foreach (var updateable in _updateables)
                updateable.OnUpdate(deltaTime);
        }

        public void Dispose()
        {
            foreach (var disposable in _disposables)
                disposable.OnDispose();

            _initialized = false;
        }

        public Entity AddComponent<TComponent>(TComponent component) where TComponent : class, IEntityComponent
        {
            _components.Add(typeof(TComponent), component);
            return this;
        }

        public bool HasComponent<TComponent>() where TComponent : class, IEntityComponent
        {
            return _components.ContainsKey(typeof(TComponent));
        }

        public bool TryGetComponent<TComponent>(out TComponent component) where TComponent : class, IEntityComponent
        {
            if (_components.TryGetValue(typeof(TComponent), out IEntityComponent findedObect))
            {
                component = (TComponent)findedObect;
                return true;
            }

            component = null;
            return false;
        }

        public TComponent GetComponent<TComponent>() where TComponent : class, IEntityComponent
        {
            if (TryGetComponent(out TComponent component) == false)
                throw new ArgumentException($"Entity is not exist {typeof(TComponent)}");

            return component;
        }

        public Entity AddSystem(IEntitySystem system)
        {
            if (_systems.Contains(system))
                throw new ArgumentException(system.GetType().ToString());

            _systems.Add(system);

            if (system is IInitializableSystem initable)
            {
                _initializeables.Add(initable);

                if (_initialized)
                    initable.OnInit(this);
            }

            if (system is IUpdateableSystem updateable)
                _updateables.Add(updateable);

            if (system is IDisposableSystem disposable)
                _disposables.Add(disposable);

            return this;
        }
    }
}
