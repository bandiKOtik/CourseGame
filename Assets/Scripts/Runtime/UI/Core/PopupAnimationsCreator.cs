using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Runtime.UI.Core
{
    public class PopupAnimationsCreator
    {
        public static Sequence CreateShowAnimation(
            CanvasGroup body,
            Image anticlicker,
            PopupAnimationType type,
            float anticlickerMaxAlpha,
            float animationSpeed)
        {
            switch (type)
            {
                case PopupAnimationType.None:
                    return DOTween.Sequence();

                case PopupAnimationType.Expand:
                    return DOTween.Sequence()
                        .Append(anticlicker
                            .DOFade(anticlickerMaxAlpha, animationSpeed)
                            .From(0))
                        .Join(body.transform
                            .DOScale(1, animationSpeed)
                            .From(0)
                            .SetEase(Ease.OutBack));

                default:
                    throw new System.ArgumentException(nameof(type));
            }
        }

        public static Sequence CreateHideAnimation(
            CanvasGroup body,
            Image anticlicker,
            PopupAnimationType type,
            float anticlickerMaxAlpha,
            float animationSpeed)
        {
            return DOTween.Sequence();
        }
    }
}