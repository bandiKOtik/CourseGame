using Assets.Scripts.Runtime.Gameplay.Common;
using Assets.Scripts.Runtime.Gameplay.Features.Attack.AreaAttack;
using Assets.Scripts.Runtime.Gameplay.Features.LifeCycle;
using Assets.Scripts.Runtime.Gameplay.Features.MovementFeature;
using Assets.Scripts.Runtime.Gameplay.Features.RotationFeature;
using Assets.Scripts.Runtime.Gameplay.Features.Sensors;
using Assets.Scripts.Utilities.Conditions;
using Assets.Scripts.Utilities.Simple;
using UnityEngine;

namespace Assets.Scripts.Runtime.Gameplay.EntitiesCore
{
    public partial class EntitiesFactory
    {
        public Entity CreateBullet(Vector3 position, Vector3 direction, float damage)
        {
            var entity = CreateEmpty();

            var mono = _monoEntitiesFactory.Create(entity, position, "Entities/Projectile/Bullet");

            entity
                .AddMoveDirection(new(direction))
                .AddMoveSpeed(new(ConstValues.BaseMovementSpeed * 2))
                .AddIsMoving()
                .AddRotationDirection(new(direction))
                .AddRotationSpeed(new(9999))
                .AddBodyContactDamage(new(damage))
                .AddIsDead()
                .AddContactsDetectingMask(1 << LayerMask.NameToLayer("Character"))
                .AddContactsColliderBuffer(new(ConstValues.BaseBufferSize))
                .AddContactsEntitiesBuffer(new(ConstValues.BaseBufferSize))
                .AddDeathMask(1 << LayerMask.NameToLayer("Character"))
                .AddIsTouchedDeathMask();

            ICompositeCondition isAliveCondition = new CompositeCondition()
                .Add(new FuncCondition(() => entity.IsDead.Value == false));

            ICompositeCondition dieCondition = new CompositeCondition()
                .Add(new FuncCondition(() => entity.IsTouchedDeathMask.Value));

            ICompositeCondition selfReleaseCondition = new CompositeCondition()
                .Add(new FuncCondition(() => entity.IsDead.Value));

            entity
                .AddCanMove(isAliveCondition)
                .AddCanRotate(isAliveCondition)
                .AddMustDie(dieCondition)
                .AddMustSelfRelease(selfReleaseCondition);

            entity
                .AddSystem(new RigidbodyMovementSystem())
                .AddSystem(new RigidbodyRotationSystem())
                .AddSystem(new BodyContactsDetectingSystem())
                .AddSystem(new BodyContactsEntitiesFilterSystem(_registry))
                .AddSystem(new DealDamageOnContactSystem())
                .AddSystem(new DeathMaskTouchDetectingSystem())
                .AddSystem(new DeathSystem())
                .AddSystem(new DisableCollidersOnDeathSystem())
                .AddSystem(new SelfReleaseSystem(_context));

            _context.Add(entity);

            return entity;
        }

        public Entity CreateExplosion(Entity excluded, Vector3 position, float damage, float radius)
        {
            var entity = CreateEmpty();

            var mono = _monoEntitiesFactory.Create(entity, position, "Entities/Projectile/Explosion");

            entity
                .AddAreaAttackRadius(new(radius))
                .AddBodyContactDamage(new(damage))
                .AddDamageInitialized()
                .AddExcludedEntitiesFromContacts(new[] { excluded })
                .AddIsDead()
                .AddContactsDetectingMask(1 << LayerMask.NameToLayer("Character"))
                .AddContactsColliderBuffer(new(ConstValues.BaseBufferSize))
                .AddContactsEntitiesBuffer(new(ConstValues.BaseBufferSize))
                .AddDeathMask(1 << LayerMask.NameToLayer("Character"))
                .AddIsTouchedDeathMask();

            ICompositeCondition dieCondition = new CompositeCondition()
                .Add(new FuncCondition(() => entity.DamageInitialized.Value == true));

            ICompositeCondition selfReleaseCondition = new CompositeCondition()
                .Add(new FuncCondition(() => true));

            entity
                .AddMustDie(dieCondition)
                .AddMustSelfRelease(selfReleaseCondition);

            entity
                .AddSystem(new ColliderSetRadiusSystem())
                .AddSystem(new BodyContactsDetectingSystem())
                .AddSystem(new BodyContactsEntitiesFilterSystem(_registry))
                .AddSystem(new ExcludeEntityFromContactSystem())
                .AddSystem(new DealDamageOnContactSystem())
                .AddSystem(new SelfReleaseSystem(_context));

            _context.Add(entity);

            return entity;
        }
    }
}