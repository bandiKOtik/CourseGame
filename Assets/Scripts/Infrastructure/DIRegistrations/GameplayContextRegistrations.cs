using Assets.Scripts.Infrastructure.DI_Container;
using Assets.Scripts.Infrastructure.Gameplay;
using Assets.Scripts.Runtime.InputManagement;
using Assets.Scripts.Utilities.InputManagement;
using Assets.Scripts.Utilities.Sequence;

namespace Assets.Scripts.Infrastructure.DIRegistrations
{
    public class GameplayContextRegistrations
    {
        private GameplayInputArgs _args;

        public void Process(DIContainer container, GameplayInputArgs args)
        {
            _args = args;

            container.RegisterAsSingle<ISequenceGenerator>(CreateSequenceGenerator);

            container.RegisterAsSingle<IInputHandler>(CreateGameplayInputHandler);

            container.RegisterAsSingle<SequenceMatcher>(CreateSequenceMatcher);
        }

        private SequenceGenerator CreateSequenceGenerator(DIContainer container)
        {
            var config = _args.GameConfig.SequenceConfig;

            return new(config.MinLength, config.MaxLength, config.Sequence);
        }

        private GameplayInputHandler CreateGameplayInputHandler(DIContainer c) => new(c);

        private SequenceMatcher CreateSequenceMatcher(DIContainer c) => new();
    }
}