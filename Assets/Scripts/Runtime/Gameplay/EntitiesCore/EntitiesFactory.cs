using Assets.Scripts.Infrastructure.DI_Container;
using Assets.Scripts.Runtime.Gameplay.Common;
using Assets.Scripts.Runtime.Gameplay.EntitiesCore.Mono;
using Assets.Scripts.Runtime.Gameplay.Features.Attack;
using Assets.Scripts.Runtime.Gameplay.Features.Attack.AreaAttack;
using Assets.Scripts.Runtime.Gameplay.Features.Attack.Shoot;
using Assets.Scripts.Runtime.Gameplay.Features.Capabilities;
using Assets.Scripts.Runtime.Gameplay.Features.DamageFeature;
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
        private readonly DIContainer _container;
        private readonly EntitiesLifeContext _context;
        private readonly CollidersRegistryService _registry;
        private readonly MonoEntitiesFactory _monoEntitiesFactory;

        public EntitiesFactory(DIContainer container)
        {
            _container = container;
            _context = _container.Resolve<EntitiesLifeContext>();
            _registry = _container.Resolve<CollidersRegistryService>();
            _monoEntitiesFactory = _container.Resolve<MonoEntitiesFactory>();
        }

        public Entity CreateHero(Vector3 position)
        {
            var entity = CreateEmpty();

            _monoEntitiesFactory.Create(entity, position, "Entities/PlayerCharacters/Hero");

            entity
                // Movement
                .AddMoveDirection()
                .AddMoveSpeed(new(ConstValues.BaseMovementSpeed))
                .AddIsMoving()
                .AddRotationDirection()
                .AddRotationSpeed(new(ConstValues.BaseRotationSpeed))
                // Health
                .AddMaxHealth(new(3f))
                .AddCurrentHealth()
                .AddTakeDamageRequest()
                .AddTakeDamageEvent()
                .AddIsDead()
                .AddInDeathProcess()
                .AddDeathProcessInitialTime(new(3))
                .AddDeathProcessCurrentTime()
                // Attack
                .AddAttackProcessInitialTime(new(2))
                .AddAttackProcessCurrentTime()
                .AddInAttackProcess()
                .AddStartAttackRequest()
                .AddStartAttackEvent()
                .AddEndAttackEvent()
                .AddAttackDelayTime(new(1))
                .AddAttackDelayEndEvent()
                .AddInstantAttackDamage(new(1))
                .AddAttackCanceledEvent()
                .AddAttackCooldownInitialTime(new(2))
                .AddAttackCooldownCurrentTime()
                .AddInAttackCooldown();

            ICompositeCondition isAliveCondition = new CompositeCondition()
                .Add(new FuncCondition(() => entity.IsDead.Value == false));

            ICompositeCondition dieCondition = new CompositeCondition()
                .Add(new FuncCondition(() => entity.CurrentHealth.Value <= 0));

            ICompositeCondition selfReleaseCondition = new CompositeCondition()
                .Add(new FuncCondition(() => entity.IsDead.Value))
                .Add(new FuncCondition(() => entity.InDeathProcess.Value == false));

            ICompositeCondition canApplyDamage = new CompositeCondition()
                .Add(new FuncCondition(() => entity.IsDead.Value == false));

            ICompositeCondition canStartAttack = new CompositeCondition()
                .Add(new FuncCondition(() => entity.IsDead.Value == false))
                .Add(new FuncCondition(() => entity.InAttackProcess.Value == false))
                .Add(new FuncCondition(() => entity.IsMoving.Value == false))
                .Add(new FuncCondition(() => entity.InAttackCooldown.Value == false));

            ICompositeCondition mustCancelAttack = new CompositeCondition(LogicOperations.Or)
                .Add(new FuncCondition(() => entity.IsDead.Value))
                .Add(new FuncCondition(() => entity.IsMoving.Value));

            entity
                .AddCanMove(isAliveCondition)
                .AddCanRotate(isAliveCondition)
                .AddCanApplyDamage(canApplyDamage)
                .AddMustDie(dieCondition)
                .AddMustSelfRelease(selfReleaseCondition)
                .AddCanStartAttack(canStartAttack)
                .AddMustCancelAttack(mustCancelAttack);

            entity
                .AddSystem(new ApplyHealthToMaxSystem())
                .AddSystem(new RigidbodyMovementSystem())
                .AddSystem(new RigidbodyRotationSystem())
                .AddSystem(new StartAttackSystem())
                .AddSystem(new AttackProcessTimerSystem())
                .AddSystem(new AttackDelayEndTriggerSystem())
                .AddSystem(new InstantShootSystem(this))
                .AddSystem(new AttackCancelSystem())
                .AddSystem(new EndAttackSystem())
                .AddSystem(new AttackCooldownTimerSystem())
                .AddSystem(new ApplyDamageSystem())
                .AddSystem(new DeathSystem())
                .AddSystem(new DisableCollidersOnDeathSystem())
                .AddSystem(new DeathProcessTimerSystem())
                .AddSystem(new SelfReleaseSystem(_context));

            _context.Add(entity);

            return entity;
        }

        public Entity CreateGhost(Vector3 position)
        {
            var entity = CreateEmpty();

            _monoEntitiesFactory.Create(entity, position, "Entities/GameplayEnemies/Ghost");

            entity
                // Movement
                .AddMoveDirection()
                .AddMoveSpeed(new(ConstValues.BaseMovementSpeed))
                .AddIsMoving()
                .AddRotationDirection()
                .AddRotationSpeed(new(ConstValues.BaseRotationSpeed))
                // Health
                .AddMaxHealth(new(3f))
                .AddCurrentHealth(new(3f))
                .AddBodyContactDamage(new(1f))
                .AddTakeDamageRequest()
                .AddTakeDamageEvent()
                .AddIsDead()
                .AddInDeathProcess()
                .AddDeathProcessInitialTime(new(3))
                .AddDeathProcessCurrentTime()
                // Contact damage
                .AddContactsDetectingMask(1 << ConstValues.CharactersLayerMask)
                .AddContactsColliderBuffer(new(ConstValues.BaseBufferSize))
                .AddContactsEntitiesBuffer(new(ConstValues.BaseBufferSize));

            ICompositeCondition isAliveCondition = new CompositeCondition()
                .Add(new FuncCondition(() => entity.IsDead.Value == false));

            ICompositeCondition dieCondition = new CompositeCondition()
                .Add(new FuncCondition(() => entity.CurrentHealth.Value <= 0));

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
                .AddSystem(new RigidbodyMovementSystem())
                .AddSystem(new RigidbodyRotationSystem())
                .AddSystem(new BodyContactsDetectingSystem())
                .AddSystem(new BodyContactsEntitiesFilterSystem(_registry))
                .AddSystem(new DealDamageOnContactSystem())
                .AddSystem(new ApplyDamageSystem())
                .AddSystem(new DeathSystem())
                .AddSystem(new DisableCollidersOnDeathSystem())
                .AddSystem(new DeathProcessTimerSystem())
                .AddSystem(new SelfReleaseSystem(_context));

            _context.Add(entity);

            return entity;
        }

        public Entity CreateWizzard(Vector3 position)
        {
            var entity = CreateEmpty();

            _monoEntitiesFactory.Create(entity, position, "Entities/GameplayEnemies/Wizzard");

            // canMove -> energy, inProcess == false
            entity
                // Movement
                .AddTeleportRequest()
                .AddTeleportRadius(new(3))
                .AddTeleportDestinationAchieved()
                .AddPositionFound()
                // Energy
                .AddMaxEnergy(new(100))
                .AddCurrentEnergy()
                .AddTeleportEnergyPrice(new(10))
                .AddChargeAmountPerSecond(new(5))
                // Attack
                .AddStartAttackRequest()
                .AddStartAttackEvent()
                .AddInAttackProcess()
                .AddAreaAttackRadius(new(3))
                .AddInstantAttackDamage(new(1))
                // Health
                .AddMaxHealth(new(3f))
                .AddCurrentHealth(new(3f))
                .AddTakeDamageRequest()
                .AddTakeDamageEvent()
                .AddIsDead()
                .AddInDeathProcess()
                // Contact Damage
                .AddContactsDetectingMask(ConstValues.CharactersLayerMask)
                .AddContactsColliderBuffer(new(ConstValues.BaseBufferSize))
                .AddContactsEntitiesBuffer(new(ConstValues.BaseBufferSize));

            ICompositeCondition mustRequestTeleport = new CompositeCondition()
                .Add(new FuncCondition(() => entity.IsDead.Value == false))
                .Add(new FuncCondition(() => entity.CurrentEnergy.Value >= 10f));

            ICompositeCondition canStartAttack = new CompositeCondition()
                .Add(new FuncCondition(() => entity.CurrentHealth.Value > 0));

            ICompositeCondition dieCondition = new CompositeCondition()
                .Add(new FuncCondition(() => entity.CurrentHealth.Value <= 0));

            ICompositeCondition selfReleaseCondition = new CompositeCondition()
                .Add(new FuncCondition(() => entity.IsDead.Value));

            ICompositeCondition canApplyDamage = new CompositeCondition()
                .Add(new FuncCondition(() => entity.IsDead.Value == false));

            entity
                .AddMustRequestTeleport(mustRequestTeleport)
                .AddCanStartAttack(canStartAttack)
                .AddCanApplyDamage(canApplyDamage)
                .AddMustDie(dieCondition)
                .AddMustSelfRelease(selfReleaseCondition);

            // EnergyRechargeSystem, TeleportPositionPickSystem -> InstantTeleportSystem ->
            // DamageAfterTeleportSystem -> AreaAttackSystem
            entity
                // Teleport
                .AddSystem(new EnergyRechargeSystem())
                .AddSystem(new RequestTeleportOnConditionSystem())
                .AddSystem(new TeleportPositionPickSystem())
                .AddSystem(new InstantTeleportSystem())
                // Damage deal
                .AddSystem(new DamageAfterTeleportSystem())
                .AddSystem(new StartAttackSystem())
                .AddSystem(new AreaAttackSystem(this))
                // Applying damage
                .AddSystem(new ApplyDamageSystem())
                .AddSystem(new DeathSystem())
                .AddSystem(new SelfReleaseSystem(_context))
                .AddSystem(new DisableCollidersOnDeathSystem())
                .AddSystem(new DebugHealthSystem())
                // Contacts
                .AddSystem(new BodyContactsDetectingSystem())
                .AddSystem(new BodyContactsEntitiesFilterSystem(_registry));

            _context.Add(entity);

            return entity;
        }

        public Entity CreateDummy(Vector3 position)
        {
            var entity = CreateEmpty();

            _monoEntitiesFactory.Create(entity, position, "Entities/GameplayEnemies/Dummy");

            entity
                .AddMaxHealth(new(1f))
                .AddCurrentHealth(new(1f))
                .AddTakeDamageRequest()
                .AddTakeDamageEvent()
                .AddIsDead()
                .AddInDeathProcess()
                .AddContactsDetectingMask(1 << 6)
                .AddContactsColliderBuffer(new(ConstValues.BaseBufferSize))
                .AddContactsEntitiesBuffer(new(ConstValues.BaseBufferSize));

            ICompositeCondition dieCondition = new CompositeCondition()
                .Add(new FuncCondition(() => entity.CurrentHealth.Value <= 0));

            ICompositeCondition selfReleaseCondition = new CompositeCondition()
                .Add(new FuncCondition(() => entity.IsDead.Value));

            ICompositeCondition canApplyDamage = new CompositeCondition()
                .Add(new FuncCondition(() => entity.IsDead.Value == false));

            entity
                .AddCanApplyDamage(canApplyDamage)
                .AddMustDie(dieCondition)
                .AddMustSelfRelease(selfReleaseCondition);

            entity
                .AddSystem(new ApplyDamageSystem())
                .AddSystem(new DeathSystem())
                .AddSystem(new SelfReleaseSystem(_context))
                .AddSystem(new DisableCollidersOnDeathSystem())
                .AddSystem(new BodyContactsDetectingSystem())
                .AddSystem(new BodyContactsEntitiesFilterSystem(_registry));

            _context.Add(entity);

            return entity;
        }

        private Entity CreateEmpty() => new Entity();
    }
}