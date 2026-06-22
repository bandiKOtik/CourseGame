using Assets.Scripts.Runtime.Gameplay.EntitiesCore;
using System.Collections.Generic;

namespace Assets.Scripts.Runtime.Gameplay.Features.StatFeature
{
    public class BaseStats : IEntityComponent
    {
        public Dictionary<StatTypes, float> Value;
    }

    public class ModifiedStats : IEntityComponent
    {
        public Dictionary<StatTypes, float> Value;
    }

    public class StatsEffects : IEntityComponent
    {
        public StatsEffectList Value;
    }
}