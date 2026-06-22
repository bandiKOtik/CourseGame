using Assets.Scripts.Runtime.UI.CommonViews;
using Assets.Scripts.Runtime.UI.Core;
using DG.Tweening;

namespace Assets.Scripts.Runtime.UI.Gameplay.AbilitySelectPopup
{
    public class SelectableAbilityListView : ElementsListView<SelectableAbilityView>, IShowableView
    {
        private Sequence _currentAnimation;

        public void Select(SelectableAbilityView selectedView)
        {
            foreach (var view in Elements)
                view.Deselect();

            selectedView.Select();
        }

        public Tween Show()
        {
            _currentAnimation?.Kill();
            _currentAnimation = DOTween.Sequence();

            foreach (var view in Elements)
            {
                _currentAnimation.Append(view.Show());
                _currentAnimation.AppendInterval(.2f);
            }

            return _currentAnimation.SetUpdate(true).Play();
        }

        public Tween Hide()
        {
            _currentAnimation?.Kill();
            _currentAnimation = DOTween.Sequence();

            foreach (var view in Elements)
            {
                _currentAnimation.Append(view.Hide());
                _currentAnimation.AppendInterval(.2f);
            }

            return _currentAnimation.SetUpdate(true).Play();
        }
    }
}