using Assets.Scripts.Runtime.Gameplay.EntitiesCore;
using Assets.Scripts.Runtime.Gameplay.EntitiesCore.Systems;
using System.Collections.Generic;

namespace Assets.Scripts.Runtime.Gameplay.Features.StatFeature
{
    public class StatsEffectsApplierSystem : IInitializableSystem, IDisposableSystem
    {
        private StatsEffectList _effects;
        private Dictionary<StatTypes, float> _baseStats;
        private Dictionary<StatTypes, float> _modifiedStats;

        public void OnInit(Entity entity)
        {
            _effects = entity.StatsEffects;
            _baseStats = entity.BaseStats;
            _modifiedStats = entity.ModifiedStats;

            _effects.Added += OnStatEffectAdded;
            _effects.Removed += OnStatEffectRemoved;
        }

        public void OnDispose()
        {
            _effects.Added -= OnStatEffectAdded;
            _effects.Removed -= OnStatEffectRemoved;
        }

        private void OnStatEffectAdded(IStatsEffect effect)
            => RecalculateStats();

        private void OnStatEffectRemoved(IStatsEffect effect)
            => RecalculateStats();

        private void RecalculateStats()
        {
            foreach (var stat in _baseStats.Keys)
                _modifiedStats[stat] = _baseStats[stat];

            foreach (var effect in _effects.Elements)
                effect.ApplyTo(_modifiedStats);
        }
    }
}