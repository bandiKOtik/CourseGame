using Assets.Scripts.Configs.Gameplay.Abilities;
using Assets.Scripts.Runtime.Gameplay.EntitiesCore;
using Assets.Scripts.Runtime.Gameplay.Features.StatFeature;

namespace Assets.Scripts.Runtime.Gameplay.Features.AbilitiesFeature.Abilities
{
    public class StatChangeAbility : Ability
    {
        private Entity _entity;
        private StatChangeAbilityConfig _config;

        public StatChangeAbility(Entity entity, StatChangeAbilityConfig config) : base(config.ID)
        {
            _entity = entity;
            _config = config;
        }

        public override void Activate()
        {
            _entity.StatsEffects.Add(new StatsEffect(_config.StatTypes, _config.GetApplyEffect()));
        }
    }
}