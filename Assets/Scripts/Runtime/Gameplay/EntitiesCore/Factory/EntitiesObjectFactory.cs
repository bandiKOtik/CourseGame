using Assets.Scripts.Runtime.Gameplay.Features.Attack;
using Assets.Scripts.Runtime.Gameplay.Features.ExplosionFeature;
using Assets.Scripts.Runtime.Gameplay.Features.LifeCycle;
using Assets.Scripts.Runtime.Gameplay.Features.Sensors;
using Assets.Scripts.Runtime.Gameplay.Features.TeamsFeature;
using Assets.Scripts.Utilities;
using Assets.Scripts.Utilities.Conditions;
using Assets.Scripts.Utilities.Simple;
using System.IO;
using UnityEngine;

namespace Assets.Scripts.Runtime.Gameplay.EntitiesCore.Factory
{
    public partial class EntitiesFactory
    {
        private const string ObjectsPath = ConstValues.EntitiesObjectsRootFolderPath;

        public Entity CreateContactTrigger(Teams team, Vector3 position)
        {
            var entity = CreateEmpty();
            var mono = _monoEntitiesFactory
                .Create(entity, position, Path.Combine(ObjectsPath, "ContactTrigger"));

            entity
                .AddTeam(new(team))
                .AddContactsDetectingMask(Layers.CharacterMask)
                .AddContactsColliderBuffer(new(ConstValues.BaseBufferSize))
                .AddContactsEntitiesBuffer(new(ConstValues.BaseBufferSize))
                .AddStartAttackRequest()
                .AddExplosionPosition(new(position))
                .AddTakeDamageRequest()
                .AddTakeDamageEvent()
                .AddMustSelfDestroy()
                .AddIsDead()
                .AddInDeathProcess()
                .AddDeathProcessInitialTime(new(1))
                .AddDeathProcessCurrentTime();

            ICompositeCondition dieCondition = new CompositeCondition()
                .Add(new FuncCondition(() => entity.MustSelfDestroy.Value));

            ICompositeCondition selfReleaseCondition = new CompositeCondition()
                .Add(new FuncCondition(() => entity.IsDead.Value))
                .Add(new FuncCondition(() => entity.InDeathProcess.Value == false));

            entity
                .AddMustDie(dieCondition)
                .AddMustSelfRelease(selfReleaseCondition);

            entity
                .AddSystem(new BodyContactsDetectingSystem())
                .AddSystem(new BodyContactsEntitiesFilterSystem(_registry))
                .AddSystem(new AttackByTriggerSystem())
                .AddSystem(new CastExplosionSystem(this))
                .AddSystem(new DeathSystem())
                .AddSystem(new DisableCollidersOnDeathSystem())
                .AddSystem(new SelfDestroyAfterAttackSystem())
                .AddSystem(new SelfReleaseSystem(_context));

            _context.Add(entity);

            return entity;
        }
    }
}
