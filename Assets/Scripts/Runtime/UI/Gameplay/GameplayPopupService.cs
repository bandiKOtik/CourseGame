using Assets.Scripts.Runtime.UI.Core;
using Assets.Scripts.Runtime.UI.Gameplay.EndgamePopups;
using Assets.Scripts.Utilities.Factory.UI;
using System;
using UnityEngine;

namespace Assets.Scripts.Runtime.UI.Gameplay
{
    public class GameplayPopupService : PopupService
    {
        private readonly GameplayUIRoot _root;
        private readonly GameplayPresentersFactory _presentersFactory;

        public GameplayPopupService(
            ViewsFactory viewsFactory,
            ProjectPresentersFactory presentersFactory,
            GameplayUIRoot uIRoot,
            GameplayPresentersFactory gameplayPresentersFactory)
            : base(viewsFactory, presentersFactory)
        {
            _root = uIRoot;
            _presentersFactory = gameplayPresentersFactory;
        }

        protected override Transform PopupLayer => _root.PopupsLayer;

        public WinPopupPresenter OpenWinPopup(Action closedCallback = null)
        {
            WinPopupView view = _viewsFactory.Create<WinPopupView>(ViewIDs.WinPopup, PopupLayer);

            WinPopupPresenter popup = _presentersFactory.CreateWinPopupPresenter(view);

            OnPopupCreated(popup, view, closedCallback);

            return popup;
        }

        public DefeatPopupPresenter OpenDefeatPopup(Action closedCallback = null)
        {
            DefeatPopupView view = _viewsFactory.Create<DefeatPopupView>(ViewIDs.DefeatPopup, PopupLayer);

            DefeatPopupPresenter popup = _presentersFactory.CreateDefeatPopupPresenter(view);

            OnPopupCreated(popup, view, closedCallback);

            return popup;
        }
    }
}