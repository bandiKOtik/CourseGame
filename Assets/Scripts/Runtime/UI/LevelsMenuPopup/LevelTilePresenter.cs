using Assets.Scripts.Infrastructure.Gameplay;
using Assets.Scripts.Meta.Features.LevelsProgression;
using Assets.Scripts.Runtime.Gameplay;
using Assets.Scripts.Runtime.UI.Core;
using Assets.Scripts.Utilities.CoroutinesManagement;
using Assets.Scripts.Utilities.SceneManagement;
using UnityEngine;

namespace Assets.Scripts.Runtime.UI.LevelsMenuPopup
{
    public class LevelTilePresenter : ISubscribedPresenter
    {
        private readonly LevelsProgressionService _levelsService;
        private readonly SceneSwitcherService _sceneSwitcherService;
        private readonly ICoroutinesPerformer _coroutinesPerformer;

        private readonly GameMode _gameMode;

        private readonly LevelTileView _view;

        public LevelTilePresenter(
            LevelsProgressionService levelsService,
            SceneSwitcherService sceneSwitcherService,
            ICoroutinesPerformer coroutinesPerformer,
            GameMode gameMode,
            LevelTileView view)
        {
            _levelsService = levelsService;
            _sceneSwitcherService = sceneSwitcherService;
            _coroutinesPerformer = coroutinesPerformer;
            _gameMode = gameMode;
            _view = view;
        }

        public LevelTileView View => _view;

        public void Initialize()
        {
            _view.SetLevel(_gameMode.ToString());
        }

        public void Dispose()
        {
            _view.Clicked -= OnViewClicked;
        }

        public void Subscribe()
        {
            _view.Clicked += OnViewClicked;
        }

        public void Unsubscribe()
        {
            _view.Clicked -= OnViewClicked;
        }

        private void OnViewClicked()
        {
            _coroutinesPerformer
                .StartPerform(_sceneSwitcherService
                .ProcessSwitchTo(Scenes.Gameplay, new GameplayInputArgs(_gameMode)));
        }
    }
}
