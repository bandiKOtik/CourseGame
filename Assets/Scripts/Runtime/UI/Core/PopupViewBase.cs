using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Runtime.UI.Core
{
    public abstract class PopupViewBase : MonoBehaviour, IShowableView
    {
        public event Action CloseRequest;

        [SerializeField] private CanvasGroup _mainGroup;
        [SerializeField] private Transform _body;
        [SerializeField] private Image _antiClicker;

        [SerializeField] private PopupAnimationType _animationType;

        private float _defaultAlpha;
        private float _animationSpeed = .5f;
        private Tween _currentAnimation;

        private void Awake()
        {
            _defaultAlpha = _antiClicker.color.a;
            _mainGroup.alpha = 0f;
        }

        public Tween Show()
        {
            _currentAnimation?.Kill();

            OnPreShow();

            _mainGroup.alpha = 1f;

            Sequence sequence = PopupAnimationsCreator.CreateShowAnimation(
                _mainGroup,
                _antiClicker,
                _animationType,
                _defaultAlpha,
                _animationSpeed);

            ModifyShowAnimation(sequence);

            sequence.OnComplete(OnPostShow);

            return _currentAnimation = sequence.SetUpdate(true).Play();
        }

        public Tween Hide()
        {
            _currentAnimation?.Kill();

            OnPreHide();

            Sequence sequence = PopupAnimationsCreator.CreateHideAnimation(
                _mainGroup,
                _antiClicker,
                _animationType,
                _defaultAlpha,
                _animationSpeed);

            ModifyHideAnimation(sequence);

            sequence.OnComplete(OnPostHide);

            return _currentAnimation = sequence.SetUpdate(true).Play();
        }

        protected virtual void ModifyShowAnimation(Sequence animation)
        { }

        protected virtual void ModifyHideAnimation(Sequence animation)
        { }

        public void OnCloseButtonClicked() => CloseRequest?.Invoke();

        protected virtual void OnPreShow()
        { }

        protected virtual void OnPostShow()
        { }

        protected virtual void OnPreHide()
        { }

        protected virtual void OnPostHide()
        { }

        private void OnDestroy() => _currentAnimation?.Kill();
    }
}