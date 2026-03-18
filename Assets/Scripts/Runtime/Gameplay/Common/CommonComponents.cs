using Assets.Scripts.Runtime.Gameplay.EntitiesCore;
using UnityEngine;

namespace Assets.Scripts.Runtime.Gameplay.Common
{
    public class TransformComponent : IEntityComponent
    {
        public Transform Value;
    }

    public class RigidbodyComponent : IEntityComponent
    {
        public Rigidbody Value;
    }

    public class CharacterControllerComponent : IEntityComponent
    {
        public CharacterController Value;
    }
}