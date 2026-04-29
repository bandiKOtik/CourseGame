using Assets.Scripts.Runtime.Gameplay.EntitiesCore;
using Assets.Scripts.Runtime.Gameplay.Features.TeamsFeature;
using Assets.Scripts.Runtime.UI.CommonViews;
using Assets.Scripts.Runtime.UI.Core;
using Assets.Scripts.Utilities.Reactive;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Runtime.UI.Gameplay.HealthDisplay
{
    public class EntityHealthPresenter : IPresenter
    {
        private Entity _entity;
        private BarWithText _bar;
        private ReactiveVariable<Teams> _team;
        private ReactiveVariable<float> _health;
        private ReactiveVariable<float> _maxHealth;

        private List<IDisposable> _subscriptions = new();

        public EntityHealthPresenter(Entity entity, BarWithText bar)
        {
            _entity = entity;
            _bar = bar;
        }

        public BarWithText Bar => _bar;

        public void Initialize()
        {
            _health = _entity.CurrentHealth;
            _maxHealth = _entity.MaxHealth;
            _team = _entity.Team;

            _subscriptions.Add(_health.Subscribe(OnHealthChanged));
            _subscriptions.Add(_maxHealth.Subscribe(OnMaxHealthChanged));
            _subscriptions.Add(_team.Subscribe(OnTeamChanged));

            UpdateHealth();
            UpdateFillerColorBy(_team.Value);
        }

        public void Dispose()
        {
            foreach (var disposable in _subscriptions)
                disposable.Dispose();

            _subscriptions.Clear();
        }

        private void UpdateHealth()
        {
            _bar.UpdateText($"{_health.Value.ToString("0")} / {_maxHealth.Value.ToString("0")}");
            _bar.UpdateValue(_health.Value / _maxHealth.Value);
        }

        private void UpdateFillerColorBy(Teams team)
        {
            if (team == Teams.MainHero)
                _bar.SetFillerColor(Color.green);
            else if (team == Teams.Enemies)
                _bar.SetFillerColor(Color.red);
        }

        private void OnHealthChanged(float arg1, float arg2) => UpdateHealth();

        private void OnMaxHealthChanged(float arg1, float arg2) => UpdateHealth();

        private void OnTeamChanged(Teams arg1, Teams newTeam) => UpdateFillerColorBy(newTeam);
    }
}
