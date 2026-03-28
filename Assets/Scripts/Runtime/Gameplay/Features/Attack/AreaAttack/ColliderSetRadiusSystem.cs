using Assets.Scripts.Runtime.Gameplay.EntitiesCore;
using Assets.Scripts.Runtime.Gameplay.EntitiesCore.Systems;
using Assets.Scripts.Utilities.Reactive;
using UnityEngine;

namespace Assets.Scripts.Runtime.Gameplay.Features.Attack.AreaAttack
{
    public class ColliderSetRadiusSystem : IInitializableSystem
    {
        private CapsuleCollider _collider;
        private ReactiveVariable<float> _radius;

        public void OnInit(Entity entity)
        {
            _collider = entity.BodyCollider;
            _radius = entity.AreaAttackRadius;

            _collider.radius = _radius.Value;
        }
    }
}