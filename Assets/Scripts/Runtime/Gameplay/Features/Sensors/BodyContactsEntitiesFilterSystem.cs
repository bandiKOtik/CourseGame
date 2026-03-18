using Assets.Scripts.Runtime.Gameplay.EntitiesCore;
using Assets.Scripts.Runtime.Gameplay.EntitiesCore.Systems;
using Assets.Scripts.Utilities;
using UnityEngine;

namespace Assets.Scripts.Runtime.Gameplay.Features.Sensors
{
    public class BodyContactsEntitiesFilterSystem : IInitializableSystem, IUpdateableSystem
    {
        private readonly CollidersRegistryService _registry;
        private Buffer<Collider> _contactColliders;
        private Buffer<Entity> _contactEntities;

        public BodyContactsEntitiesFilterSystem(CollidersRegistryService registry)
        {
            _registry = registry;
        }

        public void OnInit(Entity entity)
        {
            _contactColliders = entity.ContactsColliderBuffer;
            _contactEntities = entity.ContactsEntitiesBuffer;
        }

        public void OnUpdate(float deltaTime)
        {
            _contactEntities.Count = 0;

            for (int i = 0; i < _contactColliders.Count; i++)
            {
                var collider = _contactColliders.Items[i];

                var contactEntity = _registry.GetEntity(collider);

                if (contactEntity != null)
                {
                    _contactEntities.Items[_contactEntities.Count] = contactEntity;
                    _contactEntities.Count++;
                }
            }
        }
    }
}