using Assets.Scripts.Runtime.Gameplay.EntitiesCore;
using Assets.Scripts.Runtime.Gameplay.EntitiesCore.Systems;
using Assets.Scripts.Runtime.Gameplay.Features.DamageFeature;
using Assets.Scripts.Utilities;
using Assets.Scripts.Utilities.Reactive;
using System.Collections.Generic;

namespace Assets.Scripts.Runtime.Gameplay.Common
{
    public class DealDamageOnContactSystem : IInitializableSystem, IUpdateableSystem
    {
        private Buffer<Entity> _contacts;
        private ReactiveVariable<float> _damage;
        private List<Entity> _processedEntities;

        public void OnInit(Entity entity)
        {
            _contacts = entity.ContactsEntitiesBuffer;
            _damage = entity.BodyContactDamage;
            _processedEntities = new(_contacts.Items.Length);
        }

        public void OnUpdate(float deltaTime)
        {
            for (int i = 0; i < _contacts.Count; i++)
            {
                var contactEnitiy = _contacts.Items[i];

                if (_processedEntities.Contains(contactEnitiy) == false)
                {
                    _processedEntities.Add(contactEnitiy);

                    if (contactEnitiy.HasComponent<TakeDamageRequest>())
                        contactEnitiy.TakeDamageRequest.Invoke(_damage.Value);
                }
            }

            for (int i = _processedEntities.Count - 1; i >= 0; i--)
                if (ContainInContacts(_processedEntities[i]) == false)
                    _processedEntities.RemoveAt(i);
        }

        public bool ContainInContacts(Entity entity)
        {
            for (int i = 0; i < _contacts.Count; i++)
                if (_contacts.Items[i] == entity)
                    return true;

            return false;
        }
    }
}
