using Assets.Scripts.Infrastructure.Gameplay;
using Assets.Scripts.Runtime.UI.Core;
using Assets.Scripts.Utilities.SceneManagement;
using Cysharp.Threading.Tasks;

namespace Assets.Scripts.Runtime.UI.Gameplay.EndgamePopups
{
    public class DefeatPopupPresenter : PopupPresenterBase
    {
        private const string TitleName = "YOU LOOSE!";

        private readonly DefeatPopupView _view;
        private readonly SceneSwitcherService _sceneSwitcher;
        private readonly GameplayInputArgs _args;

        public DefeatPopupPresenter(
            DefeatPopupView view,
            SceneSwitcherService sceneSwitcher,
            GameplayInputArgs args)
        {
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
            _sceneSwitcher.SwitchAsync(Scenes.MainMenu).Forget();
            OnCloseRequest();
        }

        private void OnRestartClicked()
        {
            _sceneSwitcher.SwitchAsync(
                Scenes.Gameplay, new GameplayInputArgs(_args.LevelNumber)).Forget();

            OnCloseRequest();
        }

        protected override PopupViewBase PopupView => _view;
    }
}