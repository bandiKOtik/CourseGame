using Assets.Scripts.Runtime.Gameplay.EntitiesCore;
using Assets.Scripts.Runtime.Gameplay.EntitiesCore.Mono;
using UnityEngine;

namespace Assets.Scripts.Runtime.Gameplay.EntityRegistrators
{
    public class TransformEntityRegistrator : MonoEntityRegistrator
    {
        public override void Register(Entity entity)
        {
            entity.AddTransform(GetComponent<Transform>());
        }
    }
}
