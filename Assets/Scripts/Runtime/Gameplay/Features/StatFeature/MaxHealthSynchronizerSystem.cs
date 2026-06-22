using Assets.Scripts.Runtime.Gameplay.EntitiesCore;
using Assets.Scripts.Runtime.Gameplay.EntitiesCore.Systems;
using Assets.Scripts.Utilities.Reactive;
using System.Collections.Generic;

namespace Assets.Scripts.Runtime.Gameplay.Features.StatFeature
{
    public class MaxHealthSynchronizerSystem : IInitializableSystem, IUpdateableSystem
    {
        private ReactiveVariable<float> _maxHealth;
        private ReactiveVariable<float> _currentHealth;
        private Dictionary<StatTypes, float> _modifiedStats;

        public void OnInit(Entity entity)
        {
            _maxHealth = entity.MaxHealth;
            _currentHealth = entity.CurrentHealth;
            _modifiedStats = entity.ModifiedStats;
        }

        public void OnUpdate(float deltaTime)
        {
            float temp = _modifiedStats[StatTypes.MaxHealth];

            float previousRatio = _currentHealth.Value / _maxHealth.Value;

            if (temp < 0)
                temp = 0;

            _maxHealth.Value = temp;
            _currentHealth.Value = _maxHealth.Value * previousRatio;
        }
    }
}