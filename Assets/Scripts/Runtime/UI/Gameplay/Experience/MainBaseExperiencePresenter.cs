using Assets.Scripts.Configs.Gameplay;
using Assets.Scripts.Runtime.Gameplay.EntitiesCore;
using Assets.Scripts.Runtime.Gameplay.Features.MainBaseBuilding;
using Assets.Scripts.Runtime.UI.CommonViews;
using Assets.Scripts.Runtime.UI.Core;
using Assets.Scripts.Utilities.Reactive;
using System;
using System.Collections.Generic;

namespace Assets.Scripts.Runtime.UI.Gameplay.Experience
{
    public class MainBaseExperiencePresenter : IPresenter
    {
        private BarWithText _view;

        private MainBaseHolderService _heroHolder;
        private ExperienceForUpgradeLevelConfig _expConfig;
        private ReactiveVariable<float> _experience;
        private ReactiveVariable<int> _currentLevel;

        private List<IDisposable> _disposables = new();

        public MainBaseExperiencePresenter(
            BarWithText view,
            MainBaseHolderService heroHolder,
            ExperienceForUpgradeLevelConfig expConfig)
        {
            _view = view;
            _heroHolder = heroHolder;
            _expConfig = expConfig;
        }

        public void Initialize()
        {
            _disposables.Add(_heroHolder.BaseRegistred.Subscribe(OnMainBaseRegistred));
        }

        public void Dispose()
        {
            foreach (var item in _disposables)
                item.Dispose();

            _disposables.Clear();
        }

        private void OnMainBaseRegistred(Entity entity)
        {
            _experience = entity.Experience;
            _currentLevel = entity.Level;

            _disposables.Add(_experience.Subscribe(OnExperienceChanged));
            _disposables.Add(_currentLevel.Subscribe(OnLevelChanged));

            UpdateBarText(_currentLevel.Value);
            UpdateExperience(_experience.Value);
        }

        private void OnLevelChanged(int arg1, int newLevel) => UpdateBarText(newLevel);

        private void OnExperienceChanged(float arg1, float newExpValue) => UpdateExperience(newExpValue);

        private void UpdateExperience(float value) => _view.UpdateValue(value / _expConfig.GetExperienceFor(_currentLevel.Value));

        private void UpdateBarText(int value) => _view.UpdateText($"LVL: {value}");
    }
}