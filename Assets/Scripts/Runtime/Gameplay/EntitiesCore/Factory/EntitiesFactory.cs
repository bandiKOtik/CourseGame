using Assets.Scripts.Configs.Gameplay.Entities;
using Assets.Scripts.Configs.Gameplay.Levels;
using Assets.Scripts.Infrastructure.ConfigsManagement;
using Assets.Scripts.Infrastructure.DI_Container;
using Assets.Scripts.Runtime.Gameplay.EntitiesCore.Mono;
using Assets.Scripts.Runtime.Gameplay.Features.AI;
using Assets.Scripts.Runtime.Gameplay.Features.Attack;
using Assets.Scripts.Runtime.Gameplay.Features.Attack.AreaAttack;
using Assets.Scripts.Runtime.Gameplay.Features.DamageFeature;
using Assets.Scripts.Runtime.Gameplay.Features.ExplosionFeature;
using Assets.Scripts.Runtime.Gameplay.Features.LifeCycle;
using Assets.Scripts.Runtime.Gameplay.Features.MovementFeature;
using Assets.Scripts.Runtime.Gameplay.Features.RotationFeature;
using Assets.Scripts.Runtime.Gameplay.Features.Sensors;
using Assets.Scripts.Utilities;
using Assets.Scripts.Utilities.Conditions;
using Assets.Scripts.Utilities.Simple;
using UnityEngine;

namespace Assets.Scripts.Runtime.Gameplay.EntitiesCore.Factory
{
    public partial class EntitiesFactory
    {
        private readonly DIContainer _container;
        private readonly EntitiesLifeContext _context;
        private readonly CollidersRegistryService _registry;
        private readonly MonoEntitiesFactory _monoEntitiesFactory;
        private readonly BrainsFactory _brainsFactory;

        public EntitiesFactory(DIContainer container)
        {
            _container = container;
            _context = _container.Resolve<EntitiesLifeContext>();
            _registry = _container.Resolve<CollidersRegistryService>();
            _monoEntitiesFactory = _container.Resolve<MonoEntitiesFactory>();
            _brainsFactory = _container.Resolve<BrainsFactory>();
        }

        public Entity CreateMainDefendBuilding(DefendableBuildingConfig config, LevelConfig levelConfig, Vector3 position)
        {
            var entity = CreateEmpty();
            _monoEntitiesFactory.Create(entity, position, config.PrefabPath);

            entity
                // Health
                .AddMaxHealth(new(levelConfig.MainBaseHealth))
                .AddCurrentHealth()
                .AddTakeDamageRequest()
                .AddTakeDamageEvent()
                .AddIsDead()
                .AddInDeathProcess()
                // Attack
                .AddStartAttackRequest()
                .AddExplosionPosition()
                .AddContactsDetectingMask(Layers.CharacterMask)
                .AddContactsColliderBuffer(new(ConstValues.BaseBufferSize))
                .AddContactsEntitiesBuffer(new(ConstValues.BaseBufferSize));

            ICompositeCondition canApplyDamage = new CompositeCondition()
                .Add(new FuncCondition(() => entity.IsDead.Value == false));

            ICompositeCondition dieCondition = new CompositeCondition()
                .Add(new FuncCondition(() => entity.CurrentHealth.Value <= 0));

            ICompositeCondition selfReleaseCondition = new CompositeCondition()
                .Add(new FuncCondition(() => entity.IsDead.Value))
                .Add(new FuncCondition(() => entity.InDeathProcess.Value == false));

            entity
                .AddCanApplyDamage(canApplyDamage)
                .AddMustDie(dieCondition)
                .AddMustSelfRelease(selfReleaseCondition);

            entity
                .AddSystem(new ApplyHealthToMaxSystem())
                .AddSystem(new CastExplosionSystem(this))
                .AddSystem(new BodyContactsDetectingSystem())
                .AddSystem(new BodyContactsEntitiesFilterSystem(_registry))
                .AddSystem(new ApplyDamageSystem())
                .AddSystem(new DebugHealthSystem())
                .AddSystem(new DeathSystem())
                .AddSystem(new DisableCollidersOnDeathSystem())
                .AddSystem(new SelfReleaseSystem(_context));

            _brainsFactory.CreateExplosiveShooterBrain(entity);

            return entity;
        }

        public Entity CreateTargetMovingEnemy(Vector3 position, TargetMovingEnemy config)
        {
            var entity = CreateEmpty();
            _monoEntitiesFactory.Create(entity, position, config.PrefabPath);

            entity
                // Movement
                .AddMoveDirection()
                .AddMoveSpeed(new(config.MoveSpeed))
                .AddIsMoving()
                .AddRotationDirection()
                .AddRotationSpeed(new(config.RotationSpeed))
                .AddCurrentTarget()
                // Health
                .AddMaxHealth(new(config.MaxHealth))
                .AddCurrentHealth()
                .AddTakeDamageRequest()
                .AddTakeDamageEvent()
                .AddMustSelfDestroy()
                .AddIsDead()
                .AddInDeathProcess()
                .AddDeathProcessInitialTime(new(1))
                .AddDeathProcessCurrentTime()
                // Attack
                .AddStartAttackRequest()
                .AddExplosionPosition()
                .AddNearbyAttackTriggerRadius(new(config.AttackRadius))
                .AddContactsDetectingMask(Layers.CharacterMask)
                .AddContactsColliderBuffer(new(ConstValues.BaseBufferSize))
                .AddContactsEntitiesBuffer(new(ConstValues.BaseBufferSize));

            ICompositeCondition isAliveCondition = new CompositeCondition()
                .Add(new FuncCondition(() => entity.IsDead.Value == false));

            ICompositeCondition dieCondition = new CompositeCondition(LogicOperations.Or)
                .Add(new FuncCondition(() => entity.CurrentHealth.Value <= 0))
                .Add(new FuncCondition(() => entity.MustSelfDestroy.Value));

            ICompositeCondition selfReleaseCondition = new CompositeCondition()
                .Add(new FuncCondition(() => entity.IsDead.Value))
                .Add(new FuncCondition(() => entity.InDeathProcess.Value == false));

            ICompositeCondition canApplyDamage = new CompositeCondition()
                .Add(new FuncCondition(() => entity.IsDead.Value == false));

            entity
                .AddCanMove(isAliveCondition)
                .AddCanRotate(isAliveCondition)
                .AddCanApplyDamage(canApplyDamage)
                .AddMustDie(dieCondition)
                .AddMustSelfRelease(selfReleaseCondition);

            entity
                .AddSystem(new ApplyHealthToMaxSystem())
                .AddSystem(new CharacterControllerMovementSystem())
                .AddSystem(new TransformRotationSystem())
                .AddSystem(new BodyContactsDetectingSystem())
                .AddSystem(new BodyContactsEntitiesFilterSystem(_registry))
                .AddSystem(new ApplyDamageSystem())
                .AddSystem(new ExplodeWhenNearbySystem())
                .AddSystem(new CastExplosionSystem(this))
                .AddSystem(new SelfDestroyAfterAttackSystem())
                .AddSystem(new DeathSystem())
                .AddSystem(new DisableCollidersOnDeathSystem())
                .AddSystem(new DeathProcessTimerSystem())
                .AddSystem(new SelfReleaseSystem(_context));

            return entity;
        }

        private Entity CreateEmpty() => new Entity();
    }
}