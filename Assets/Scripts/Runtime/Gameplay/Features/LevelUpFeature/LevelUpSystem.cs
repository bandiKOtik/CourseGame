using Assets.Scripts.Configs.Gameplay;
using Assets.Scripts.Runtime.Gameplay.EntitiesCore;
using Assets.Scripts.Runtime.Gameplay.EntitiesCore.Systems;
using Assets.Scripts.Utilities.Reactive;
using System;

namespace Assets.Scripts.Runtime.Gameplay.Features.LevelUpFeature
{
    public class LevelUpSystem : IInitializableSystem, IDisposableSystem
    {
        private ReactiveVariable<float> _experience;
        private ReactiveVariable<int> _level;
        private ExperienceForUpgradeLevelConfig _config;

        private IDisposable _experienceChangeDisposable;

        public LevelUpSystem(ExperienceForUpgradeLevelConfig config)
        {
            _config = config;
        }

        public float CurrentLimitForExp => _config.GetExperienceFor(_level.Value);

        public void OnInit(Entity entity)
        {
            _experience = entity.Experience;
            _level = entity.Level;

            _experienceChangeDisposable = _experience.Subscribe(OnExperienceChanged);
        }

        public void OnDispose()
        {
            _experienceChangeDisposable.Dispose();
        }

        private void OnExperienceChanged(float arg1, float newExp)
        {
            while (newExp >= CurrentLimitForExp && _level.Value < _config.MaxLevel)
            {
                newExp -= CurrentLimitForExp;
                _level.Value++;
            }

            _experience.Value = newExp;
        }
    }
}