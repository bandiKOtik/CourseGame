using Assets.Scripts.Runtime.UI.Core;
using DG.Tweening;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Runtime.UI.Gameplay.EndgamePopups
{
    public class WinPopupView : PopupViewBase
    {
        public event Action ContinueClicked;

        [SerializeField] private Text _title;
        [SerializeField] private List<Transform> _stars;

        public void SetTitle(string title) => _title.text = title;

        public void OnContinueClicked() => ContinueClicked?.Invoke();

        protected override void ModifyShowAnimation(Sequence animation)
        {
            base.ModifyShowAnimation(animation);

            foreach (var star in _stars)
            {
                animation
                    .Append(star.DOScale(1, .3f).SetEase(Ease.OutBack).From(0))
                    .Join(star.DOLocalRotate(Vector3.forward * 360, .3f, RotateMode.LocalAxisAdd)
                        .SetEase(Ease.OutCubic)
                        .From(Vector3.zero));

                animation.AppendInterval(.1f);
            }
        }
    }
}