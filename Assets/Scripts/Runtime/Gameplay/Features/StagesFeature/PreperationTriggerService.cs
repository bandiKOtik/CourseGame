using Assets.Scripts.Runtime.Gameplay.EntitiesCore;
using Assets.Scripts.Runtime.Gameplay.EntitiesCore.Factory;
using Assets.Scripts.Runtime.Gameplay.Features.MainHero;
using Assets.Scripts.Utilities;
using Assets.Scripts.Utilities.Reactive;
using UnityEngine;

namespace Assets.Scripts.Runtime.Gameplay.Features.StagesFeature
{
    public class PreperationTriggerService
    {
        private ReactiveVariable<bool> _hasMainHeroContact = new();

        private EntitiesFactory _entitiesFactory;
        private EntitiesLifeContext _context;

        private Entity _nextStageTrigger;
        private Buffer<Entity> _nextStageTriggerContacts;

        public PreperationTriggerService(EntitiesFactory entitiesFactory, EntitiesLifeContext context)
        {
            _entitiesFactory = entitiesFactory;
            _context = context;
        }

        public ReactiveVariable<bool> HasContact => _hasMainHeroContact;

        public void Create(Vector3 position)
        {
            if (_nextStageTrigger != null)
                throw new System.InvalidOperationException("Next stage trigger already exists");

            _nextStageTrigger = _entitiesFactory.CreateContactTrigger(position);
            _nextStageTriggerContacts = _nextStageTrigger.ContactsEntitiesBuffer;
        }

        public void Update(float deltaTime)
        {
            if (_nextStageTrigger == null)
                return;

            for (int i = 0; i < _nextStageTriggerContacts.Count; i++)
            {
                var contact = _nextStageTriggerContacts.Items[i];

                if (contact.HasComponent<IsMainHero>())
                {
                    _hasMainHeroContact.Value = true;
                    return;
                }

                _hasMainHeroContact.Value = false;
            }
        }

        public void CleanUp()
        {
            _context.Release(_nextStageTrigger);
            _hasMainHeroContact.Value = false;
            _nextStageTrigger = null;
            _nextStageTriggerContacts = null;
        }
    }
}
