using Assets.Scripts.Meta.Statistics;
using Assets.Scripts.Runtime.UI.CommonViews;
using Assets.Scripts.Runtime.UI.Core;
using Assets.Scripts.Utilities.Factory.UI;
using System.Collections.Generic;

namespace Assets.Scripts.Runtime.UI.StatisticsUI
{
    public class StatisticsWindowPresenter : IPresenter
    {
        private readonly PlayedGamesStatistic _statistics;
        private readonly ProjectPresentersFactory _presentersFactory;
        private readonly ViewsFactory _viewsFactory;

        private readonly TextListView _view;

        private readonly Dictionary<GameStatType, StatPresenter> _presenterTypeToView = new();

        public StatisticsWindowPresenter(
            PlayedGamesStatistic statistic,
            ProjectPresentersFactory presentersFactory,
            ViewsFactory viewsFactory,
            TextListView view)
        {
            _statistics = statistic;
            _presentersFactory = presentersFactory;
            _viewsFactory = viewsFactory;
            _view = view;
        }

        public void Initialize()
        {
            foreach (var statisticType in _statistics.AvaiableStatistics)
            {
                var statisticView = _viewsFactory.Create<TextView>(ViewIDs.StatisticView);

                _view.Add(statisticView);

                var statisticPresenter = _presentersFactory.CreateStatisticPresenter(
                    statisticView,
                    _statistics.GetStatValue(statisticType),
                    statisticType);

                statisticPresenter.Initialize();
                _presenterTypeToView.Add(statisticType, statisticPresenter);
            }

            _statistics.StatChanged += OnValueChanged;
        }

        public void Dispose()
        {
            foreach (var presenter in _presenterTypeToView)
            {
                _view.Remove(presenter.Value.View);
                _viewsFactory.Release(presenter.Value.View);
                presenter.Value.Dispose();
            }

            _presenterTypeToView.Clear();

            _statistics.StatChanged -= OnValueChanged;
        }

        private void OnValueChanged(GameStatType type, int value)
        {
            _presenterTypeToView[type].View.SetText(type.ToString() + ": " + value.ToString());
        }
    }
}
