using Assets.Scripts.Runtime.UI.CommonViews;
using Assets.Scripts.Runtime.UI.Core;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Runtime.UI.MainMenu
{
    public class MainMenuScreenView : MonoBehaviour, IView
    {
        public event Action LevelsButtonClicked;

        public event Action ResetProgressButtonClicked;

        [field: SerializeField] public IconTextListView WalletView { get; private set; }
        [field: SerializeField] public TextListView StatisticsView { get; private set; }

        [SerializeField] private Button _openLevelsMenu;
        [SerializeField] private Button _resetProgress;

        private void OnEnable()
        {
            _openLevelsMenu.onClick.AddListener(OnLevelsButtonClicked);
            _resetProgress.onClick.AddListener(OnProgressResetButtonClick);
        }

        private void OnDisable()
        {
            _openLevelsMenu.onClick.RemoveListener(OnLevelsButtonClicked);
            _resetProgress.onClick.RemoveListener(OnProgressResetButtonClick);
        }

        private void OnLevelsButtonClicked() => LevelsButtonClicked?.Invoke();

        private void OnProgressResetButtonClick() => ResetProgressButtonClicked?.Invoke();
    }
}