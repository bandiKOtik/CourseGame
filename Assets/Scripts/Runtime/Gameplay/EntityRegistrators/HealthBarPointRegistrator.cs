using Assets.Scripts.Runtime.Gameplay.EntitiesCore;
using Assets.Scripts.Runtime.Gameplay.EntitiesCore.Mono;
using UnityEngine;

namespace Assets.Scripts.Runtime.Gameplay.EntityRegistrators
{
    public class HealthBarPointRegistrator : MonoEntityRegistrator
    {
        [SerializeField] private Transform _point;

        public override void Register(Entity entity)
        {
            entity.AddHealthBarPoint(_point);
        }
    }
}