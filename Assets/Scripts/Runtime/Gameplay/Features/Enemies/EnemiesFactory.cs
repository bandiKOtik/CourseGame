using Assets.Scripts.Configs.Gameplay.Entities;
using Assets.Scripts.Infrastructure.ConfigsManagement;
using Assets.Scripts.Infrastructure.DI_Container;
using Assets.Scripts.Runtime.Gameplay.EntitiesCore;
using Assets.Scripts.Runtime.Gameplay.EntitiesCore.Factory;
using Assets.Scripts.Runtime.Gameplay.Features.AI;
using Assets.Scripts.Runtime.Gameplay.Features.AI.States;
using Assets.Scripts.Runtime.Gameplay.Features.TeamsFeature;
using UnityEngine;

namespace Assets.Scripts.Runtime.Gameplay.Features.Enemies
{
    public class EnemiesFactory
    {
        private readonly DIContainer _container;

        private readonly EntitiesFactory _entitiesFactory;
        private readonly BrainsFactory _brainsFactory;
        private readonly EntitiesLifeContext _context;

        public EnemiesFactory(DIContainer container)
        {
            _container = container;
            _entitiesFactory = container.Resolve<EntitiesFactory>();
            _brainsFactory = container.Resolve<BrainsFactory>();
            _context = container.Resolve<EntitiesLifeContext>();
        }

        public Entity Create(Vector3 position, EntityConfig config)
        {
            Entity entity;

            switch (config)
            {
                case TargetMovingEnemy targetEnemyConfig:
                    entity = _entitiesFactory.CreateTargetMovingEnemy(position, targetEnemyConfig);
                    _brainsFactory.CreateTargetWalkBrain(entity, new EntityTeamTargetSelector(entity, Teams.MainHero));
                    break;

                case GhostConfig ghostConfig:
                    entity = _entitiesFactory.CreateGhost(position, ghostConfig);
                    _brainsFactory.CreateRandomWalkBrain(entity);
                    break;

                case WizzardConfig wizzardConfig:
                    entity = _entitiesFactory.CreateWizzard(position, wizzardConfig);
                    _brainsFactory.CreateRandomTeleportationBrain(entity, 3);
                    break;

                default:
                    throw new System.ArgumentException("Not supported type of config: " + config.GetType());
            }

            entity.AddTeam(new(Teams.Enemies));

            _context.Add(entity);

            return entity;
        }
    }
}
