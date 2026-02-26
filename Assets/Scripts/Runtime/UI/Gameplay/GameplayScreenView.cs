using Assets.Scripts.Runtime.UI.CommonViews;
using Assets.Scripts.Runtime.UI.Core;
using UnityEngine;

namespace Assets.Scripts.Runtime.UI.Gameplay
{
    public class GameplayScreenView : MonoBehaviour, IView
    {
        [field: SerializeField] private TextView _sequenceView;
        [field: SerializeField] private TextView _userInputView;

        public void SetSequenceText(string sequenceText)
        {
            _sequenceView.SetText(sequenceText);
        }

        public void ChangeInputText(string inputText)
        {
            _userInputView.SetText(inputText);
        }
    }
}
