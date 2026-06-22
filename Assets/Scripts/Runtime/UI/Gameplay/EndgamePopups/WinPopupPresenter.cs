using Assets.Scripts.Runtime.UI.Core;
using Assets.Scripts.Utilities.SceneManagement;
using Cysharp.Threading.Tasks;

namespace Assets.Scripts.Runtime.UI.Gameplay.EndgamePopups
{
    public class WinPopupPresenter : PopupPresenterBase
    {
        private const string TitleName = "YOU WIN!";

        private readonly WinPopupView _view;
        private readonly SceneSwitcherService _sceneSwitcher;

        public WinPopupPresenter(
            WinPopupView view,
            SceneSwitcherService sceneSwitcher)
        {
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
            _sceneSwitcher.SwitchAsync(Scenes.MainMenu).Forget();
            OnCloseRequest();
        }
    }
}