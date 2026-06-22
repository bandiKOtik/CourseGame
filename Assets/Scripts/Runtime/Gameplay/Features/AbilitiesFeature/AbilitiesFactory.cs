using Assets.Scripts.Configs.Gameplay.Abilities;
using Assets.Scripts.Infrastructure.DI_Container;
using Assets.Scripts.Runtime.Gameplay.EntitiesCore;
using Assets.Scripts.Runtime.Gameplay.Features.AbilitiesFeature.Abilities;

namespace Assets.Scripts.Runtime.Gameplay.Features.AbilitiesFeature
{
    public class AbilitiesFactory
    {
        private DIContainer _container;

        public AbilitiesFactory(DIContainer container)
        {
            _container = container;
        }

        public Ability CreateAbilityFor(Entity entity, AbilityConfig config)
        {
            switch (config)
            {
                case StatChangeAbilityConfig statChangeConfig:
                    return new StatChangeAbility(entity, statChangeConfig);

                default:
                    throw new System.ArgumentException();
            }
        }
    }
}