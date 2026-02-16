using Assets.Scripts.Infrastructure.DI_Container;
using Assets.Scripts.Infrastructure.Gameplay;
using Assets.Scripts.Runtime.InputManagement;
using Assets.Scripts.Runtime.Sequence;
using Assets.Scripts.Utilities.Factory;
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

            container.RegisterAsSingle(CreateSequenceGeneratorFactory);

            container.RegisterAsSingle<ISequenceGenerator>(CreateSequenceGenerator);

            container.RegisterAsSingle<IInputHandler>(CreateGameplayInputHandler);

            container.RegisterAsSingle(CreateSequenceMatcher);
        }

        private SequenceGeneratorFactory CreateSequenceGeneratorFactory(DIContainer c) => new(c);

        private SequenceGenerator CreateSequenceGenerator(DIContainer c)
        {
            return c
                .Resolve<SequenceGeneratorFactory>()
                .CreateSequenceGenerator(_args.GenerationConfig);
        }

        private InputStringHandler CreateGameplayInputHandler(DIContainer c) => new();

        private SequenceMatcher CreateSequenceMatcher(DIContainer c) => new();
    }
}