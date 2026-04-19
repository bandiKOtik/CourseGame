using Assets.Scripts.Meta.Statistics;

namespace Assets.Scripts.Meta
{
    public class StatisticManageService
    {
        private PlayedGamesStatistic _playerStatistic;

        public StatisticManageService(PlayedGamesStatistic playerStatistic)
        {
            _playerStatistic = playerStatistic;
        }

        public void ApplyWinRewards() => _playerStatistic.Increase(GameStatType.Win);

        public void DefeatRewards() => _playerStatistic.Increase(GameStatType.Defeat);
    }
}