using Assets.Scripts.Runtime.UI.Core;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Runtime.UI.LevelsMenuPopup
{
    public class LevelsMenuPopupView : PopupViewBase
    {
        [SerializeField] private Text _title;
        [SerializeField] private LevelTilesListView _levelTilesListView;

        public LevelTilesListView LevelTilesListView => _levelTilesListView;

        public void SetTitle(string title) => _title.text = title;

        protected override void ModifyShowAnimation(Sequence animation)
        {
            base.ModifyShowAnimation(animation);

            foreach (LevelTileView levelTileView in _levelTilesListView.Elements)
            {
                animation.Append(levelTileView.Show());
                animation.AppendInterval(0.1f);
            }
        }
    }
}
