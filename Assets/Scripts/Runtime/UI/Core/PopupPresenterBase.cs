using Cysharp.Threading.Tasks;
using System;
using System.Threading;

namespace Assets.Scripts.Runtime.UI.Core
{
    public abstract class PopupPresenterBase : IPresenter
    {
        public event Action<PopupPresenterBase> CloseRequest;

        private CancellationTokenSource _cts = new();

        protected abstract PopupViewBase PopupView { get; }

        public virtual void Initialize()
        { }

        public virtual void Dispose()
        {
            KillProcess();
            PopupView.CloseRequest -= OnCloseRequest;
        }

        public void Show()
        {
            KillProcess();
            _cts = new CancellationTokenSource();
            ProcessShow(_cts.Token).Forget();
        }

        public void Hide(Action callback = null)
        {
            KillProcess();
            _cts = new CancellationTokenSource();
            ProcessHide(callback, _cts.Token).Forget();
        }

        protected virtual void OnPreShow()
        {
            PopupView.CloseRequest += OnCloseRequest;
        }

        protected virtual void OnPostShow()
        { }

        protected virtual void OnPreHide()
        {
            PopupView.CloseRequest -= OnCloseRequest;
        }

        protected virtual void OnPostHide()
        { }

        protected void OnCloseRequest() => CloseRequest?.Invoke(this);

        private async UniTask ProcessShow(CancellationToken token)
        {
            OnPreShow();

            await PopupView.Show().ToUniTask(TweenCancelBehaviour.Complete, token);

            OnPostShow();
        }

        private async UniTask ProcessHide(Action callback, CancellationToken token)
        {
            OnPreHide();

            await PopupView.Hide().ToUniTask(TweenCancelBehaviour.Complete, token);

            OnPostHide();

            callback?.Invoke();
        }

        private void KillProcess()
        {
            _cts?.Cancel();
            _cts?.Dispose();
        }
    }
}