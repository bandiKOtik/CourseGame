using Assets.Scripts.Infrastructure.DI_Container;
using Assets.Scripts.Utilities.AssetsManagement;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Runtime.Gameplay.EntitiesCore.Mono
{
    public class MonoEntitiesFactory : IInitializable, IDisposable
    {
        private readonly ResourcesAssetsLoader _loader;

        private readonly EntitiesLifeContext _context;

        private readonly Dictionary<Entity, MonoEntity> _entityToMono = new();

        public MonoEntitiesFactory(ResourcesAssetsLoader loader, EntitiesLifeContext context)
        {
            _loader = loader;
            _context = context;
        }

        public MonoEntity Create(Entity entity, Vector3 position, string path)
        {
            MonoEntity prefab = _loader.Load<MonoEntity>(path);

            var instance = UnityEngine.Object.Instantiate(prefab, position, Quaternion.identity, null);

            instance.Setup(entity);

            _entityToMono.Add(entity, instance);

            return instance;
        }

        public void Initialize()
        {
            _context.Released += OnEntityReleased;
        }

        public void Dispose()
        {
            _context.Released -= OnEntityReleased;

            foreach (Entity entity in _entityToMono.Keys)
                CleanupFor(entity);

            _entityToMono.Clear();
        }

        private void OnEntityReleased(Entity entity)
        {
            CleanupFor(entity);
            _entityToMono.Remove(entity);
        }

        private void CleanupFor(Entity entity)
        {
            var monoEntity = _entityToMono[entity];
            monoEntity.Cleanup(entity);
            UnityEngine.Object.Destroy(monoEntity);
        }
    }
}
