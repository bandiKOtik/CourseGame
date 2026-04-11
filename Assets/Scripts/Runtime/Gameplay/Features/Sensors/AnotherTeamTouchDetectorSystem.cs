using Assets.Scripts.Runtime.Gameplay.EntitiesCore;
using Assets.Scripts.Runtime.Gameplay.EntitiesCore.Systems;
using Assets.Scripts.Runtime.Gameplay.Features.TeamsFeature;
using Assets.Scripts.Utilities;
using Assets.Scripts.Utilities.Reactive;

namespace Assets.Scripts.Runtime.Gameplay.Features.Sensors
{
    public class AnotherTeamTouchDetectorSystem : IInitializableSystem, IUpdateableSystem
    {
        private Buffer<Entity> _contacts;
        private ReactiveVariable<bool> _isTouched;
        private ReactiveVariable<Teams> _sourceTeam;

        public void OnInit(Entity entity)
        {
            _contacts = entity.ContactsEntitiesBuffer;
            _isTouched = entity.IsTouchedAnotherTeam;
            _sourceTeam = entity.Team;
        }

        public void OnUpdate(float deltaTime)
        {
            for (int i = 0; i < _contacts.Count; i++)
            {
                var contact = _contacts.Items[i];

                if (contact.TryGetTeam(out var contactTeam))
                {
                    if (_sourceTeam.Value != contactTeam.Value)
                    {
                        _isTouched.Value = true;
                        return;
                    }
                }
            }

            _isTouched.Value = false;
        }
    }
}
