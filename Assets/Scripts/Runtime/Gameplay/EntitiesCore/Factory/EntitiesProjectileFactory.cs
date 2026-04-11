using Assets.Scripts.Runtime.Gameplay.Common;
using Assets.Scripts.Runtime.Gameplay.Features.Attack.AreaAttack;
using Assets.Scripts.Runtime.Gameplay.Features.LifeCycle;
using Assets.Scripts.Runtime.Gameplay.Features.MovementFeature;
using Assets.Scripts.Runtime.Gameplay.Features.RotationFeature;
using Assets.Scripts.Runtime.Gameplay.Features.Sensors;
using Assets.Scripts.Utilities;
using Assets.Scripts.Utilities.Conditions;
using Assets.Scripts.Utilities.Simple;
using System.IO;
using UnityEngine;

namespace Assets.Scripts.Runtime.Gameplay.EntitiesCore.Factory
{
    public partial class EntitiesFactory
    {
        private const string ProjectilesPath = ConstValues.EntitiesProjectilesRootFolderPath;

        public Entity CreateBullet(Entity owner, Vector3 position, Vector3 direction, float damage)
        {
            var entity = CreateEmpty();

            var mono = _monoEntitiesFactory.Create(entity, position, Path.Combine(ProjectilesPath, "Bullet"));

            entity
                .AddMoveDirection(new(direction))
                .AddMoveSpeed(new(ConstValues.BaseMovementSpeed * 2))
                .AddIsMoving()
                .AddRotationDirection(new(direction))
                .AddRotationSpeed(new(9999))
                .AddBodyContactDamage(new(damage))
                .AddIsDead()
                .AddContactsDetectingMask(Layers.CharacterMask | Layers.EnvironmentMask)
                .AddContactsColliderBuffer(new(ConstValues.BaseBufferSize))
                .AddContactsEntitiesBuffer(new(ConstValues.BaseBufferSize))
                .AddDeathMask(Layers.EnvironmentMask)
                .AddIsTouchedDeathMask()
                .AddIsTouchedAnotherTeam()
                .AddTeam(new(owner.Team.Value));

            ICompositeCondition isAliveCondition = new CompositeCondition()
                .Add(new FuncCondition(() => entity.IsDead.Value == false));

            ICompositeCondition dieCondition = new CompositeCondition(LogicOperations.Or)
                .Add(new FuncCondition(() => entity.IsTouchedDeathMask.Value))
                .Add(new FuncCondition(() => entity.IsTouchedAnotherTeam.Value));

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
                .AddSystem(new AnotherTeamTouchDetectorSystem())
                .AddSystem(new DealDamageOnContactSystem())
                .AddSystem(new DeathMaskTouchDetectingSystem())
                .AddSystem(new AnotherTeamTouchDetectorSystem())
                .AddSystem(new DeathSystem())
                .AddSystem(new DisableCollidersOnDeathSystem())
                .AddSystem(new SelfReleaseSystem(_context));

            _context.Add(entity);

            return entity;
        }

        public Entity CreateExplosion(Entity excluded, Vector3 position, float damage, float radius)
        {
            var entity = CreateEmpty();

            var mono = _monoEntitiesFactory.Create(entity, position, Path.Combine(ProjectilesPath, "Explosion"));

            entity
                .AddAreaAttackRadius(new(radius))
                .AddBodyContactDamage(new(damage))
                .AddDamageInitialized()
                .AddExcludedEntitiesFromContacts(new[] { excluded })
                .AddIsDead()
                .AddContactsDetectingMask(Layers.CharacterMask)
                .AddContactsColliderBuffer(new(ConstValues.BaseBufferSize))
                .AddContactsEntitiesBuffer(new(ConstValues.BaseBufferSize))
                .AddDeathMask(Layers.CharacterMask)
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