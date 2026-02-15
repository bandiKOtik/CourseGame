using Assets.Scripts.Configs.Meta.GameModeConfigs;
using Assets.Scripts.Infrastructure.DI_Container;
using Assets.Scripts.Utilities.Sequence;

namespace Assets.Scripts.Utilities.Factory
{
    public class SequenceGeneratorFactory
    {
        private DIContainer _container;

        public SequenceGeneratorFactory(DIContainer container)
        {
            _container = container;
        }

        public SequenceGenerator CreateSequenceGenerator(SequenceGenerationConfig config)
            => new(config.MinLength, config.MaxLength, config.Sequence);
    }
}