using Assets.Scripts.Runtime.Gameplay.EntitiesCore;
using Assets.Scripts.Runtime.Gameplay.EntitiesCore.Systems;
using Assets.Scripts.Utilities;

namespace Assets.Scripts.Runtime.Gameplay.Features.Sensors
{
    public class ExcludeEntityFromContactSystem : IInitializableSystem, IUpdateableSystem
    {
        private Entity[] _exclude;
        private Buffer<Entity> _contactEntities;

        public void OnInit(Entity entity)
        {
            _exclude = entity.ExcludedEntitiesFromContacts;
            _contactEntities = entity.ContactsEntitiesBuffer;
        }

        public void OnUpdate(float deltaTime)
        {
            for (int i = 0; i < _contactEntities.Count; i++)
            {
                for (int j = 0; j < _exclude.Length; j++)
                {
                    if (_contactEntities.Items[i] == _exclude[j])
                    {
                        _contactEntities.Items[i] = _contactEntities.Items[_contactEntities.Count - 1];
                        _contactEntities.Items[_contactEntities.Count - 1] = null;
                        _contactEntities.Count--;
                    }
                }
            }
        }
    }
}