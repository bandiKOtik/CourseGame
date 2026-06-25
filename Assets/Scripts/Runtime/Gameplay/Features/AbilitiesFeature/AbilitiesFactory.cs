using Assets.Scripts.Configs.Gameplay.Abilities;
using Assets.Scripts.Runtime.Gameplay.EntitiesCore;
using Assets.Scripts.Runtime.Gameplay.Features.AbilitiesFeature.Abilities;

namespace Assets.Scripts.Runtime.Gameplay.Features.AbilitiesFeature
{
    public class AbilitiesFactory
    {
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