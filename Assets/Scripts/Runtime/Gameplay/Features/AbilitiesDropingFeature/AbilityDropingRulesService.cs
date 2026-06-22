using Assets.Scripts.Configs.Gameplay.Abilities;
using Assets.Scripts.Runtime.Gameplay.EntitiesCore;

namespace Assets.Scripts.Runtime.Gameplay.Features.AbilitiesDropingFeature
{
    public class AbilityDropingRulesService
    {
        public bool IsAvailable(AbilityConfig config, Entity entity)
        {
            switch (config)
            {
                case StatChangeAbilityConfig statChange:
                    return entity.TryGetModifiedStats(out var modified)
                        && modified.ContainsKey(statChange.StatTypes);
            }

            return true;
        }
    }
}