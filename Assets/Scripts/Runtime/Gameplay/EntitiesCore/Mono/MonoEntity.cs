using UnityEngine;

namespace Assets.Scripts.Runtime.Gameplay.EntitiesCore.Mono
{
    public class MonoEntity : MonoBehaviour
    {
        private CollidersRegistryService _registry;
        private Entity _linked;

        public Entity LinkedEntity => _linked;

        public void Initialize(CollidersRegistryService registry)
        {
            _registry = registry;
        }

        public void Link(Entity entity)
        {
            _linked = entity;

            MonoEntityRegistrator[] registrators = GetComponentsInChildren<MonoEntityRegistrator>();

            if (registrators != null )
                foreach (var register in registrators)
                    register.Register(entity);

            foreach (var col in GetComponentsInChildren<Collider>())
                _registry.Register(col, entity);
        }

        public void Cleanup(Entity entity)
        {
            foreach (var col in GetComponentsInChildren<Collider>())
                _registry.Unregister(col);

            _linked = null;
        }
    }
}
