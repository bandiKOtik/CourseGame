using Assets.Scripts.Infrastructure.Gameplay;
using Assets.Scripts.Runtime.UI.Core;
using Assets.Scripts.Utilities.CoroutinesManagement;
using Assets.Scripts.Utilities.SceneManagement;

namespace Assets.Scripts.Runtime.UI.Gameplay.EndgamePopups
{
    public class DefeatPopupPresenter : PopupPresenterBase
    {
        private const string TitleName = "YOU LOOSE!";

        private readonly DefeatPopupView _view;
        private readonly SceneSwitcherService _sceneSwitcher;
        private readonly ICoroutinesPerformer _performer;
        private readonly GameplayInputArgs _args;

        public DefeatPopupPresenter(
            ICoroutinesPerformer performer,
            DefeatPopupView view,
            SceneSwitcherService sceneSwitcher,
            GameplayInputArgs args) : base(performer)
        {
            _performer = performer;
            _view = view;
            _sceneSwitcher = sceneSwitcher;
            _args = args;
        }

        public override void Initialize()
        {
            base.Initialize();

            _view.SetTitle(TitleName);

            _view.ContinueClicked += OnContinueClicked;
            _view.RestartClicked += OnRestartClicked;
        }

        protected override void OnPreHide()
        {
            base.OnPreHide();

            _view.ContinueClicked -= OnContinueClicked;
        }

        public override void Dispose()
        {
            base.Dispose();

            _view.ContinueClicked -= OnContinueClicked;
        }

        private void OnContinueClicked()
        {
            _performer.StartPerform(_sceneSwitcher.SwitchAsync(Scenes.MainMenu));
            OnCloseRequest();
        }

        private void OnRestartClicked()
        {
            _performer.StartPerform(_sceneSwitcher
                .SwitchAsync(Scenes.Gameplay, new GameplayInputArgs(_args.LevelNumber)));

            OnCloseRequest();
        }

        protected override PopupViewBase PopupView => _view;
    }
}