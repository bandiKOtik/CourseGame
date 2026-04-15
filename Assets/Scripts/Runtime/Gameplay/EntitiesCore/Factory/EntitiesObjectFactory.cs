using Assets.Scripts.Runtime.Gameplay.Features.Sensors;
using Assets.Scripts.Utilities;
using Assets.Scripts.Utilities.Simple;
using System.IO;
using UnityEngine;

namespace Assets.Scripts.Runtime.Gameplay.EntitiesCore.Factory
{
    public partial class EntitiesFactory
    {
        private const string ObjectsPath = ConstValues.EntitiesObjectsRootFolderPath;

        public Entity CreateContactTrigger(Vector3 position)
        {
            var entity = CreateEmpty();
            var mono = _monoEntitiesFactory.Create(entity, position, Path.Combine(ObjectsPath, "ContactTrigger"));

            entity
                .AddContactsDetectingMask(Layers.CharacterMask)
                .AddContactsColliderBuffer(new(ConstValues.BaseBufferSize))
                .AddContactsEntitiesBuffer(new(ConstValues.BaseBufferSize));

            entity
                .AddSystem(new BodyContactsDetectingSystem())
                .AddSystem(new BodyContactsEntitiesFilterSystem(_registry));

            _context.Add(entity);

            return entity;
        }
    }
}
