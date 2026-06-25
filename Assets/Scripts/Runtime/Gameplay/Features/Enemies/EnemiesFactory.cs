using Assets.Scripts.Configs.Gameplay.Entities;
using Assets.Scripts.Infrastructure.DI_Container;
using Assets.Scripts.Runtime.Gameplay.EntitiesCore;
using Assets.Scripts.Runtime.Gameplay.EntitiesCore.Factory;
using Assets.Scripts.Runtime.Gameplay.Features.AI;
using Assets.Scripts.Runtime.Gameplay.Features.AI.States;
using Assets.Scripts.Runtime.Gameplay.Features.LootFeature;
using Assets.Scripts.Runtime.Gameplay.Features.MainBaseBuilding;
using Assets.Scripts.Runtime.Gameplay.Features.TeamsFeature;
using Assets.Scripts.Utilities.Conditions;
using UnityEngine;

namespace Assets.Scripts.Runtime.Gameplay.Features.Enemies
{
    public class EnemiesFactory
    {
        private readonly EntitiesFactory _entitiesFactory;
        private readonly BrainsFactory _brainsFactory;
        private readonly EntitiesLifeContext _lifeContext;
        private readonly DropLootService _dropLootService;
        private readonly MainBaseHolderService _heroHolder;

        public EnemiesFactory(
            EntitiesFactory entitiesFactory,
            BrainsFactory brainsFactory,
            EntitiesLifeContext lifeContext,
            DropLootService dropLootService,
            MainBaseHolderService mainBaseHolderService)
        {
            _entitiesFactory = entitiesFactory;
            _brainsFactory = brainsFactory;
            _lifeContext = lifeContext;
            _dropLootService = dropLootService;
            _heroHolder = mainBaseHolderService;
        }

        public Entity Create(Vector3 position, EntityConfig config)
        {
            Entity entity;

            switch (config)
            {
                case TargetMovingEnemy targetEnemyConfig:
                    entity = _entitiesFactory.CreateTargetMovingEnemy(position, targetEnemyConfig);
                    _brainsFactory.CreateTargetWalkBrain(entity, new MainBaseTargetSelector(
                        entity,
                        _heroHolder));
                    break;

                default:
                    throw new System.ArgumentException("Not supported type of config: " + config.GetType());
            }

            AddDropLootBehaviorTo(entity);

            entity.AddTeam(new(Teams.Enemies));

            _lifeContext.Add(entity);

            return entity;
        }

        private void AddDropLootBehaviorTo(Entity entity)
        {
            ICompositeCondition canDrop = new CompositeCondition()
                .Add(new FuncCondition(() => entity.IsDead.Value))
                .Add(new FuncCondition(() => entity.LootIsDropped.Value == false));

            entity
                .AddLootIsDropped()
                .AddCanDropLoot(canDrop);

            entity.MustSelfRelease.Add(new FuncCondition(() => entity.LootIsDropped.Value));

            entity.AddSystem(new DropLootSystem(_dropLootService));
        }
    }
}