using Assets.Scripts.Runtime.Gameplay.EntitiesCore;
using Assets.Scripts.Runtime.Gameplay.EntitiesCore.Systems;
using Assets.Scripts.Runtime.Gameplay.Features.TeamsFeature;
using Assets.Scripts.Utilities;
using Assets.Scripts.Utilities.Reactive;

namespace Assets.Scripts.Runtime.Gameplay.Features.ExplosionFeature
{
    public class AttackByTriggerSystem : IInitializableSystem, IUpdateableSystem
    {
        private ReactiveVariable<Teams> _sourceTeam;
        private ReactiveEvent _request;
        private Buffer<Entity> _triggerContacts;

        public void OnInit(Entity entity)
        {
            _sourceTeam = entity.Team;
            _request = entity.StartAttackRequest;
            _triggerContacts = entity.ContactsEntitiesBuffer;
        }

        public void OnUpdate(float deltaTime)
        {
            for (int i = 0; i < _triggerContacts.Count; i++)
            {
                var contact = _triggerContacts.Items[i];

                if (contact.TryGetComponent<Team>(out var team))
                    if (team.Value.Value != _sourceTeam.Value)
                        _request.Invoke();
            }
        }
    }
}