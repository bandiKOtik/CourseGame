using Assets.Scripts.Runtime.UI.Core;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Runtime.UI.Gameplay.EndgamePopups
{
    public class DefeatPopupView : PopupViewBase
    {
        public event Action ContinueClicked;

        public event Action RestartClicked;

        [SerializeField] private Text _title;
        [SerializeField] private Button _continueButton;
        [SerializeField] private Button _restartButton;

        public void SetTitle(string title) => _title.text = title;

        protected override void OnPreShow()
        {
            base.OnPreShow();

            _continueButton.onClick.AddListener(OnContinueButtonClicked);
            _restartButton.onClick.AddListener(OnRestartButtonClicked);
        }

        protected override void OnPostHide()
        {
            base.OnPostHide();

            _continueButton.onClick.RemoveListener(OnContinueButtonClicked);
            _restartButton.onClick.RemoveListener(OnRestartButtonClicked);
        }

        private void OnDisable()
        {
            _continueButton.onClick.RemoveListener(OnContinueButtonClicked);
            _restartButton.onClick.RemoveListener(OnRestartButtonClicked);
        }

        private void OnRestartButtonClicked() => RestartClicked?.Invoke();

        private void OnContinueButtonClicked() => ContinueClicked?.Invoke();
    }
}