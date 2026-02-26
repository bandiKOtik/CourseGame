using Assets.Scripts.Meta.Statistics;
using Assets.Scripts.Runtime.UI.CommonViews;
using Assets.Scripts.Runtime.UI.Core;
using Assets.Scripts.Utilities.Reactive;

namespace Assets.Scripts.Runtime.UI.StatisticsUI
{
    public class StatPresenter : IPresenter
    {
        private readonly int _value;
        private readonly GameStatType _statType;

        private readonly TextView _view;

        public StatPresenter(
            int value,
            GameStatType statType,
            TextView view)
        {
            _value = value;
            _statType = statType;
            _view = view;
        }

        public TextView View => _view;

        public void Initialize()
        {
            _view.SetText(_statType.ToString() + ": " + _value.ToString());
        }

        public void Dispose() { }
    }
}
