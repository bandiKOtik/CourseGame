using Assets.Scripts.Configs.Gameplay.Levels;
using Assets.Scripts.Infrastructure.ConfigsManagement;
using Assets.Scripts.Runtime.Gameplay;
using Assets.Scripts.Runtime.UI.Core;
using Assets.Scripts.Utilities.CoroutinesManagement;
using Assets.Scripts.Utilities.Factory.UI;
using System;
using System.Collections.Generic;

namespace Assets.Scripts.Runtime.UI.LevelsMenuPopup
{
    public class LevelsMenuPopupPresenter : PopupPresenterBase
    {
        private const string TitleName = "Select Level";

        private readonly ConfigsProviderService _configsProvider;
        private readonly ProjectPresentersFactory _presentersFactory;
        private ViewsFactory _viewsFactory;

        private readonly LevelsMenuPopupView _view;

        private readonly List<LevelTilePresenter> _levelTilePresenters = new();

        public LevelsMenuPopupPresenter(
            ICoroutinesPerformer performer,
            ConfigsProviderService configsProvider,
            ProjectPresentersFactory presentersFactory,
            ViewsFactory viewsFactory,
            LevelsMenuPopupView view)
            : base(performer)
        {
            _configsProvider = configsProvider;
            _presentersFactory = presentersFactory;
            _viewsFactory = viewsFactory;
            _view = view;
        }

        protected override PopupViewBase PopupView => _view;

        public override void Initialize()
        {
            base.Initialize();

            _view.SetTitle(TitleName);

            var levelsConfig = _configsProvider.GetConfig<LevelsListConfig>();

            foreach (GameMode gameType in Enum.GetValues(typeof(GameMode)))
            {
                var tileView = _viewsFactory.Create<LevelTileView>(ViewIDs.LevelTile);

                _view.LevelTilesListView.Add(tileView);

                var tilePresenter = _presentersFactory.CreateLevelTilePresenter(tileView, gameType);

                tilePresenter.Initialize();

                _levelTilePresenters.Add(tilePresenter);
            }
        }

        public override void Dispose()
        {
            base.Dispose();

            foreach (var presenter in _levelTilePresenters)
            {
                _view.LevelTilesListView.Remove(presenter.View);
                _viewsFactory.Release(presenter.View);
                presenter.Dispose();
            }

            _levelTilePresenters.Clear();
        }

        protected override void OnPreShow()
        {
            base.OnPreShow();

            foreach (var level in _levelTilePresenters)
                level.Subscribe();
        }

        protected override void OnPreHide()
        {
            base.OnPreHide();

            foreach (var level in _levelTilePresenters)
                level.Unsubscribe();
        }
    }
}
