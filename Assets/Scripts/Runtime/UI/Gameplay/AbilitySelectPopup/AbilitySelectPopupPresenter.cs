using Assets.Scripts.Configs.Gameplay.Abilities;
using Assets.Scripts.Runtime.Gameplay.EntitiesCore;
using Assets.Scripts.Runtime.Gameplay.Features.AbilitiesDropingFeature;
using Assets.Scripts.Runtime.UI.Core;
using Assets.Scripts.Utilities.Factory.UI;
using System.Collections.Generic;

namespace Assets.Scripts.Runtime.UI.Gameplay.AbilitySelectPopup
{
    public class AbilitySelectPopupPresenter : PopupPresenterBase
    {
        private const int AbilitiesCount = 3;

        private const string Title = "LEVEL {0}";
        private const string SelectAbilityText = "Select ability";

        private readonly AbilitySelectPopupView _view;

        private readonly Entity _entity;
        private readonly AbilityDropingService _dropper;
        private readonly GameplayPresentersFactory _presentersFactory;
        private readonly ViewsFactory _viewsFactory;

        private List<SelectableAbilityPresenter> _presenters = new();
        private SelectableAbilityPresenter _selectedPresenter;

        private int _level;

        public AbilitySelectPopupPresenter(
            AbilitySelectPopupView view,
            Entity entity,
            AbilityDropingService dropper,
            GameplayPresentersFactory presentersFactory,
            ViewsFactory viewsFactory,
            int level)
        {
            _view = view;
            _entity = entity;
            _dropper = dropper;
            _presentersFactory = presentersFactory;
            _viewsFactory = viewsFactory;
            _level = level;
        }

        protected override PopupViewBase PopupView => _view;

        public override void Initialize()
        {
            base.Initialize();

            _view.SetTitle(string.Format(Title, _level));
            _view.SetAdditionalText(SelectAbilityText);
            _view.DisableSelectButton();

            _view.SelectButtonClicked += OnSelectButtonClicked;

            List<AbilityConfig> dropOptions = _dropper.Drop(AbilitiesCount, _entity);

            for (int i = 0; i < dropOptions.Count; i++)
            {
                var selectableView = _viewsFactory.Create<SelectableAbilityView>(ViewIDs.SelectableAbilityView);

                _view.SelectableAbilityListView.Add(selectableView);

                var presenter = _presentersFactory
                    .CreateSelectableAbilityPresenter(dropOptions[i], selectableView, _entity);

                presenter.Selected += OnPresenterSelected;
                presenter.Initialize();

                _presenters.Add(presenter);
            }
        }

        protected override void OnPreHide()
        {
            base.OnPreHide();

            _view.DisableSelectButton();

            _view.SelectButtonClicked -= OnSelectButtonClicked;

            foreach (var presenter in _presenters)
                presenter.Selected -= OnPresenterSelected;
        }

        public override void Dispose()
        {
            base.Dispose();

            _view.SelectButtonClicked -= OnSelectButtonClicked;

            foreach (var presenter in _presenters)
            {
                presenter.Selected -= OnPresenterSelected;
                _view.SelectableAbilityListView.Remove(presenter.View);
                _viewsFactory.Release(presenter.View);
                presenter.Dispose();
            }

            _presenters.Clear();
        }

        private void OnPresenterSelected(SelectableAbilityPresenter selected)
        {
            _view.EnableSelectButton();
            _view.SelectableAbilityListView.Select(selected.View);
            _selectedPresenter = selected;
        }

        private void OnSelectButtonClicked()
        {
            _selectedPresenter.Provide();
            OnCloseRequest();
        }
    }
}