using Assets.Scripts.Runtime.UI.Core;
using Assets.Scripts.Utilities.CoroutinesManagement;
using Assets.Scripts.Utilities.SceneManagement;

namespace Assets.Scripts.Runtime.UI.Gameplay.EndgamePopups
{
    public class WinPopupPresenter : PopupPresenterBase
    {
        private const string TitleName = "YOU WIN!";

        private readonly WinPopupView _view;
        private readonly SceneSwitcherService _sceneSwitcher;
        private readonly ICoroutinesPerformer _performer;

        public WinPopupPresenter(
            ICoroutinesPerformer performer,
            WinPopupView view,
            SceneSwitcherService sceneSwitcher) : base(performer)
        {
            _performer = performer;
            _view = view;
            _sceneSwitcher = sceneSwitcher;
        }

        protected override PopupViewBase PopupView => _view;

        public override void Initialize()
        {
            base.Initialize();

            _view.SetTitle(TitleName);

            _view.ContinueClicked += OnContinueClicked;
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
    }
}