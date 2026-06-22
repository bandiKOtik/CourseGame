using Assets.Scripts.Runtime.UI.Core;
using DG.Tweening;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Runtime.UI.Gameplay.AbilitySelectPopup
{
    public class AbilitySelectPopupView : PopupViewBase
    {
        public event Action SelectButtonClicked;

        [SerializeField] private TMP_Text _title;
        [SerializeField] private TMP_Text _selectAbilityText;

        [SerializeField] private Button _selectButton;

        [SerializeField] private SelectableAbilityListView _viewsList;

        public SelectableAbilityListView SelectableAbilityListView => _viewsList;

        private void OnEnable() => _selectButton.onClick.AddListener(OnSelectButtonClicked);

        private void OnDisable() => _selectButton.onClick.RemoveListener(OnSelectButtonClicked);

        public void SetTitle(string title) => _title.text = title;

        public void EnableSelectButton() => _selectButton.gameObject.SetActive(true);

        public void DisableSelectButton() => _selectButton.gameObject.SetActive(false);

        public void SetAdditionalText(string text) => _selectAbilityText.text = text;

        protected override void ModifyShowAnimation(Sequence animation)
        {
            base.ModifyShowAnimation(animation);

            animation.Append(_viewsList.Show());
        }

        protected override void ModifyHideAnimation(Sequence animation)
        {
            base.ModifyHideAnimation(animation);

            animation.Append(_viewsList.Hide());
        }

        private void OnSelectButtonClicked() => SelectButtonClicked?.Invoke();
    }
}