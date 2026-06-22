using Assets.Scripts.Runtime.UI.Core;
using DG.Tweening;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Runtime.UI.Gameplay.AbilitySelectPopup
{
    public class SelectableAbilityView : MonoBehaviour, IShowableView
    {
        public event Action Clicked;

        [SerializeField] private CanvasGroup _canvasGroup;

        [SerializeField] private Button _button;

        [SerializeField] private AbilityIcon _icon;
        [SerializeField] private TMP_Text _name;
        [SerializeField] private TMP_Text _description;

        [SerializeField] private TMP_Text _tabletText;

        [SerializeField] private Image _selectedImage;

        private Sequence _currentAnimation;
        private float _startYOffset = 100;

        public AbilityIcon Icon => _icon;

        private void Awake()
        {
            _canvasGroup.alpha = 0;
        }

        private void OnEnable() => _button.onClick.AddListener(OnClicked);

        private void OnDisable() => _button.onClick.RemoveListener(OnClicked);

        public void SetName(string name) => _name.text = name;

        public void SetDescription(string description) => _description.text = description;

        public void SetTabletText(string text) => _tabletText.text = text;

        public void Select() => _selectedImage.gameObject.SetActive(true);

        public void Deselect() => _selectedImage.gameObject.SetActive(false);

        public Tween Show()
        {
            _currentAnimation?.Kill();

            _currentAnimation = DOTween.Sequence();

            return _currentAnimation
                .Append(_canvasGroup.DOFade(1, .4f))
                .Join(_canvasGroup.transform.DOLocalMoveY(0, .4f).From(_startYOffset))
                .SetUpdate(true)
                .Play();
        }

        public Tween Hide()
        {
            _currentAnimation?.Kill();

            _currentAnimation = DOTween.Sequence();

            return _currentAnimation
                .Append(_canvasGroup.DOFade(0, .4f))
                .Join(_canvasGroup.transform.DOLocalMoveY(_startYOffset, .4f))
                .SetUpdate(true)
                .Play();
        }

        private void OnClicked()
        {
            Clicked?.Invoke();
        }
    }
}