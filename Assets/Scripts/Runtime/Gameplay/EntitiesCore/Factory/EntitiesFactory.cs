using Assets.Scripts.Configs.Gameplay.Entities;
using Assets.Scripts.Configs.Gameplay.Levels;
using Assets.Scripts.Infrastructure.DI_Container;
using Assets.Scripts.Runtime.Gameplay.EntitiesCore.Mono;
using Assets.Scripts.Runtime.Gameplay.Features.AI;
using Assets.Scripts.Runtime.Gameplay.Features.Attack;
using Assets.Scripts.Runtime.Gameplay.Features.Attack.AreaAttack;
using Assets.Scripts.Runtime.Gameplay.Features.DamageFeature;
using Assets.Scripts.Runtime.Gameplay.Features.ExplosionFeature;
using Assets.Scripts.Runtime.Gameplay.Features.InputManagement;
using Assets.Scripts.Runtime.Gameplay.Features.LifeCycle;
using Assets.Scripts.Runtime.Gameplay.Features.LootFeature;
using Assets.Scripts.Runtime.Gameplay.Features.MovementFeature;
using Assets.Scripts.Runtime.Gameplay.Features.RotationFeature;
using Assets.Scripts.Runtime.Gameplay.Features.Sensors;
using Assets.Scripts.Runtime.Gameplay.Features.SpawnFeature;
using Assets.Scripts.Runtime.Gameplay.Features.StatFeature;
using Assets.Scripts.Utilities;
using Assets.Scripts.Utilities.Conditions;
using Assets.Scripts.Utilities.Simple;
using System.Collections.Generic;
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

            Dictionary<StatTypes, float> baseStats = new()
            {
                { StatTypes.MaxHealth, config.MaxHealth },
            };

            Dictionary<StatTypes, float> modStats = new(baseStats);

            entity
                .AddBaseStats(baseStats)
                .AddModifiedStats(modStats)
                .AddStatsEffects()
                // Health
                .AddMaxHealth(new(baseStats[StatTypes.MaxHealth]))
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
                .AddSystem(new MaxHealthSynchronizerSystem())
                .AddSystem(new CastExplosionSystem(this))
                .AddSystem(new BodyContactsDetectingSystem())
                .AddSystem(new BodyContactsEntitiesFilterSystem(_registry))
                .AddSystem(new ApplyDamageSystem())
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
                .AddContactsEntitiesBuffer(new(ConstValues.BaseBufferSize))
                // Spawn
                .AddSpawnInitialTime(new(1))
                .AddSpawnCurrentTime()
                .AddInSpawnProcess();

            ICompositeCondition isAliveCondition = new CompositeCondition()
                .Add(new FuncCondition(() => entity.InSpawnProcess.Value == false))
                .Add(new FuncCondition(() => entity.IsDead.Value == false));

            ICompositeCondition dieCondition = new CompositeCondition(LogicOperations.Or)
                .Add(new FuncCondition(() => entity.CurrentHealth.Value <= 0))
                .Add(new FuncCondition(() => entity.MustSelfDestroy.Value));

            ICompositeCondition selfReleaseCondition = new CompositeCondition()
                .Add(new FuncCondition(() => entity.IsDead.Value))
                .Add(new FuncCondition(() => entity.InDeathProcess.Value == false));

            ICompositeCondition canApplyDamage = new CompositeCondition()
                .Add(new FuncCondition(() => entity.InSpawnProcess.Value == false))
                .Add(new FuncCondition(() => entity.IsDead.Value == false));

            entity
                .AddCanMove(isAliveCondition)
                .AddCanRotate(isAliveCondition)
                .AddCanApplyDamage(canApplyDamage)
                .AddMustDie(dieCondition)
                .AddMustSelfRelease(selfReleaseCondition);

            entity
                .AddSystem(new ApplyHealthToMaxSystem())
                .AddSystem(new SpawnProcessTimerSystem())
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

        public Entity CreatePullable(string prefabPath, Vector3 position)
        {
            var entity = CreateEmpty();
            _monoEntitiesFactory.Create(entity, position, prefabPath);

            entity
                .AddIsPullable()
                .AddInPullingProcess()
                .AddInSpawnProcess(new(true))
                .AddCurrentTarget(new(null))
                .AddMoveDirection()
                .AddMoveSpeed(new(12f))
                .AddIsMoving()
                .AddIsCollected();

            ICompositeCondition moveCondition = new CompositeCondition()
                .Add(new FuncCondition(() => entity.InPullingProcess.Value))
                .Add(new FuncCondition(() => entity.InSpawnProcess.Value == false));

            ICompositeCondition selfReleaseCondition = new CompositeCondition()
                .Add(new FuncCondition(() => entity.IsCollected.Value));

            entity
                .AddCanMove(moveCondition)
                .AddMustSelfRelease(selfReleaseCondition);

            entity
                .AddSystem(new GenerateMoveDirectionToTargetSystem())
                .AddSystem(new RigidbodyMovementSystem())
                .AddSystem(new CollectOnNearToTargetSystem())
                .AddSystem(new SelfReleaseSystem(_context));

            return entity;
        }

        private Entity CreateEmpty() => new Entity();
    }
}