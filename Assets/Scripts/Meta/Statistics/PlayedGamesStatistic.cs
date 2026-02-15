using Assets.Scripts.Utilities.DataManagement;
using Assets.Scripts.Utilities.DataManagement.DataProviders;
using System;
using System.Collections.Generic;

namespace Assets.Scripts.Meta.Statistics
{
    public class PlayedGamesStatistic : IDataReader<PlayerData>, IDataWriter<PlayerData>
    {
        private readonly Dictionary<GameStatType, int> _statistics;

        public PlayedGamesStatistic(
            Dictionary<GameStatType, int> statistic,
            PlayerDataProvider playerDataProvider)
        {
            _statistics = new(statistic);
            playerDataProvider.RegisterReader(this);
            playerDataProvider.RegisterWriter(this);
        }

        public object GetStat(GameStatType type) => _statistics[type];

        public void Increase(GameStatType stat)
        {
            if (_statistics.ContainsKey(stat) == false)
                throw new ArgumentException("You trying to increase stat that not exist: " + nameof(stat));

            _statistics[stat]++;
        }

        public void ReadFrom(PlayerData data)
        {
            foreach (KeyValuePair<GameStatType, int> statistic in data.PlayedGamesData)
            {
                if (_statistics.ContainsKey(statistic.Key))
                    _statistics[statistic.Key] = statistic.Value;
                else
                    _statistics.Add(statistic.Key, statistic.Value);
            }
        }

        public void WriteTo(PlayerData data)
        {
            foreach (KeyValuePair<GameStatType, int> statistic in data.PlayedGamesData)
            {
                if (data.PlayedGamesData.ContainsKey(statistic.Key))
                    data.PlayedGamesData[statistic.Key] = statistic.Value;
                else
                    data.PlayedGamesData.Add(statistic.Key, statistic.Value);
            }
        }
    }
}
