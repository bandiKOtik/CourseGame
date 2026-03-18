using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Runtime.Gameplay.EntitiesCore
{
    public class CollidersRegistryService
    {
        private readonly Dictionary<Collider, Entity> _colliderToEnitiy = new();

        public void Register(Collider collider, Entity entity)
        {
            _colliderToEnitiy.Add(collider, entity);
        }

        public void Unregister(Collider collider)
        {
            _colliderToEnitiy.Remove(collider);
        }

        public Entity GetEntity(Collider collider)
        {
            if (_colliderToEnitiy.TryGetValue(collider, out Entity entity))
                return entity;

            return null;
        }
    }
}