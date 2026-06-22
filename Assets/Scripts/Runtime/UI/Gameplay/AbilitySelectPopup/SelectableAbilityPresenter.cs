using Assets.Scripts.Configs.Gameplay.Abilities;
using Assets.Scripts.Runtime.Gameplay.EntitiesCore;
using Assets.Scripts.Runtime.Gameplay.Features.AbilitiesFeature;
using Assets.Scripts.Runtime.UI.Core;
using System;

namespace Assets.Scripts.Runtime.UI.Gameplay.AbilitySelectPopup
{
    public class SelectableAbilityPresenter : IPresenter
    {
        public event Action<SelectableAbilityPresenter> Selected;

        private AbilitiesFactory _abilitiesFactory;
        private Entity _entity;

        public SelectableAbilityPresenter(
            AbilityConfig config,
            SelectableAbilityView view,
            AbilitiesFactory abilitiesFactory,
            Entity entity)
        {
            Config = config;
            View = view;
            _abilitiesFactory = abilitiesFactory;
            _entity = entity;
        }

        public AbilityConfig Config { get; }
        public SelectableAbilityView View { get; }

        public void Initialize()
        {
            View.SetName(Config.Name);
            View.SetDescription(Config.Description);
            View.Icon.SetIcon(Config.Icon);

            View.Icon.HideLevel();
            View.SetTabletText("NEW");

            View.Clicked += OnViewClicked;
        }

        public void Dispose()
        {
            View.Clicked -= OnViewClicked;
        }

        public void Provide()
        {
            Ability ability = _abilitiesFactory.CreateAbilityFor(_entity, Config);
            _entity.Abilities.Add(ability);
        }

        private void OnViewClicked() => Selected?.Invoke(this);
    }
}