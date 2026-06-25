using Assets.Scripts.Configs.Gameplay.Levels.Stages;
using Assets.Scripts.Runtime.Gameplay.EntitiesCore;
using Assets.Scripts.Runtime.Gameplay.Features.Enemies;

namespace Assets.Scripts.Runtime.Gameplay.Features.StagesFeature
{
    public class StagesFactory
    {
        private readonly EnemiesFactory _enemiesFactory;
        private readonly EntitiesLifeContext _context;

        public StagesFactory(EnemiesFactory enemiesFactory, EntitiesLifeContext context)
        {
            _enemiesFactory = enemiesFactory;
            _context = context;
        }

        public IStage Create(StageConfig config)
        {
            switch (config)
            {
                case ClearAllEnemiesStageConfig clearAllConfig:
                    return new ClearAllEnemiesStage(
                        clearAllConfig,
                        _enemiesFactory,
                        _context);

                default:
                    throw new System.NotImplementedException("Not implemented stage config type: " + config);
            }
        }
    }
}