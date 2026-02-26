using Assets.Scripts.Utilities.CoroutinesManagement;
using DG.Tweening;
using System;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Runtime.UI.Core
{
    public abstract class PopupPresenterBase : IPresenter
    {
        public event Action<PopupPresenterBase> CloseRequest;

        private readonly ICoroutinesPerformer _performer;
        private Coroutine _process;

        protected PopupPresenterBase(ICoroutinesPerformer performer)
        {
            _performer = performer;
        }

        protected abstract PopupViewBase PopupView { get; }

        public virtual void Initialize() { }

        public virtual void Dispose()
        {
            KillProcess();
            PopupView.CloseRequest -= OnCloseRequest;
        }

        public void Show()
        {
            KillProcess();
            _process = _performer.StartPerform(ProcessShow());
        }

        public void Hide(Action callback = null)
        {
            KillProcess();
            _process = _performer.StartPerform(ProcessHide(callback));
        }

        protected virtual void OnPreShow()
        {
            PopupView.CloseRequest += OnCloseRequest;
        }

        protected virtual void OnPostShow() { }

        protected virtual void OnPreHide()
        {
            PopupView.CloseRequest -= OnCloseRequest;
        }

        protected virtual void OnPostHide() { }

        protected void OnCloseRequest() => CloseRequest?.Invoke(this);

        private IEnumerator ProcessShow()
        {
            OnPreShow();

            yield return PopupView.Show().WaitForCompletion();

            OnPostShow();
        }

        private IEnumerator ProcessHide(Action callback)
        {
            OnPreHide();

            yield return PopupView.Hide().WaitForCompletion();

            OnPostHide();

            callback?.Invoke();
        }

        private void KillProcess()
        {
            if (_process != null)
                _performer.StopPerform(_process);
        }
    }
}