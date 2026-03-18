using Assets.Scripts.Runtime.Gameplay.EntitiesCore;
using Assets.Scripts.Utilities;
using Assets.Scripts.Utilities.Reactive;
using UnityEngine;

namespace Assets.Scripts.Runtime.Gameplay.Features.Sensors
{
    public class BodyCollider : IEntityComponent
    {
        public CapsuleCollider Value;
    }

    public class ContactsDetectingMask : IEntityComponent
    {
        public LayerMask Value;
    }

    public class ContactsColliderBuffer : IEntityComponent
    {
        public Buffer<Collider> Value;
    }

    public class ContactsEntitiesBuffer : IEntityComponent
    {
        public Buffer<Entity> Value;
    }

    public class DeathMask : IEntityComponent
    {
        public LayerMask Value;
    }

    public class IsTouchedDeathMask : IEntityComponent
    {
        public ReactiveVariable<bool> Value;
    }

    public class ExcludedEntitiesFromContacts : IEntityComponent
    {
        public Entity[] Value;
    }
}