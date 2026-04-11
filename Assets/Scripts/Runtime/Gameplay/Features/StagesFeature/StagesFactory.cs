using Assets.Scripts.Configs.Gameplay.Levels.Stages;
using Assets.Scripts.Infrastructure.DI_Container;
using Assets.Scripts.Runtime.Gameplay.EntitiesCore;
using Assets.Scripts.Runtime.Gameplay.Features.Enemies;

namespace Assets.Scripts.Runtime.Gameplay.Features.StagesFeature
{
    public class StagesFactory
    {
        private readonly DIContainer _container;

        public StagesFactory(DIContainer container)
        {
            _container = container;
        }

        public IStage Create(StageConfig config)
        {
            switch (config)
            {
                case ClearAllEnemiesStageConfig clearAllConfig:
                    return new ClearAllEnemiesStage(
                        clearAllConfig,
                        _container.Resolve<EnemiesFactory>(),
                        _container.Resolve<EntitiesLifeContext>());

                default:
                    throw new System.NotImplementedException("Not implemented stage config type: " + config);
            }
        }
    }
}
