using Assets.Scripts.Runtime.Gameplay;
using Assets.Scripts.Runtime.SequenceGeneration;
using Assets.Scripts.Runtime.UI.Core;
using Assets.Scripts.Utilities.Sequence;
using System.Collections.Generic;

namespace Assets.Scripts.Runtime.UI.Gameplay
{
    public class GameplayScreenPresenter : IPresenter
    {
        private readonly GameplayScreenView _view;

        // Sequence generator & User input
        //private readonly MainMenuPopupService _popupService;
        //private readonly PlayerDataProvider _dataProvider;
        private readonly SequenceGenerator _sequenceGenerator;
        private readonly GameSession _gameSession;

        private readonly List<IPresenter> _childPresenters = new();

        public GameplayScreenPresenter(
            GameplayScreenView view,
            GameSession session)
        {
            _view = view;
            _gameSession = session;
        }

        public void Initialize()
        {
            _childPresenters.Clear();

            _view.SetSequenceText(_gameSession.Sequence);

            _gameSession.UserInputChanged += UpdateInputText;
        }

        public void Dispose()
        {
            _gameSession.UserInputChanged -= UpdateInputText;
        }

        private void UpdateInputText(string input)
            => _view.ChangeInputText(input);
    }
}
