using Assets.Scripts.Configs.Meta.GameModeConfigs;
using Assets.Scripts.Infrastructure.ConfigsManagement;
using Assets.Scripts.Infrastructure.DI_Container;
using Assets.Scripts.Runtime.Gameplay;
using Assets.Scripts.Utilities.Sequence;

namespace Assets.Scripts.Utilities.Factory
{
    public class SequenceGeneratorFactory
    {
        private DIContainer _container;
        private GameModeConfig _gameModeConfig;

        public SequenceGeneratorFactory(DIContainer container)
        {
            _container = container;

            _gameModeConfig = _container
                .Resolve<ConfigsProviderService>()
                .GetConfig<GameModeConfig>();
        }

        public SequenceGenerator CreateSequenceGenerator(GameMode type)
        {
            var config = _gameModeConfig.GetSequenceConfig(type);

            return new(config.MinLength, config.MaxLength, config.Sequence);
        }
    }
}