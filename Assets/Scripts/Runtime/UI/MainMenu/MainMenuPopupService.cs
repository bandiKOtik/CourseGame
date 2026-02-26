using Assets.Scripts.Runtime.UI.Core;
using Assets.Scripts.Utilities.Factory.UI;
using UnityEngine;

namespace Assets.Scripts.Runtime.UI.MainMenu
{
    public class MainMenuPopupService : PopupService
    {
        private readonly MainMenuUIRoot _root;

        public MainMenuPopupService(
            ViewsFactory viewsFactory,
            ProjectPresentersFactory
            presentersFactory,
            MainMenuUIRoot root)
            : base(viewsFactory, presentersFactory)
        {
            _root = root;
        }

        protected override Transform PopupLayer => _root.PopupsLayer;
    }
}
