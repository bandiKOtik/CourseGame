using Assets.Scripts.Utilities.CoroutinesManagement;
using Assets.Scripts.Utilities.DataManagement;
using Assets.Scripts.Utilities.DataManagement.DataProviders;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Assets.Scripts.Meta.Statistics
{
    public class PlayedGamesStatistic : IDataReader<PlayerData>, IDataWriter<PlayerData>
    {
        public event Action<GameStatType, int> StatChanged;

        private readonly Dictionary<GameStatType, int> _statistics;

        private readonly ICoroutinesPerformer _performer;
        private readonly PlayerDataProvider _playerDataProvider;

        public PlayedGamesStatistic(
            Dictionary<GameStatType, int> statistic,
            PlayerDataProvider playerDataProvider,
            ICoroutinesPerformer performer)
        {
            _playerDataProvider = playerDataProvider;
            _performer = performer;

            _statistics = new(statistic);
            _playerDataProvider.RegisterReader(this);
            _playerDataProvider.RegisterWriter(this);
        }

        public void Reset()
        {
            foreach (var stat in _statistics.Keys.ToList())
            {
                _statistics[stat] = default;

                StatChanged?.Invoke(stat, _statistics[stat]);

                _performer.StartPerform(_playerDataProvider.SaveAsync());
            }
        }

        public List<GameStatType> AvaiableStatistics => _statistics.Keys.ToList();

        public int GetStatValue(GameStatType type) => _statistics[type];

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
            foreach (KeyValuePair<GameStatType, int> stat in _statistics)
            {
                if (data.PlayedGamesData.ContainsKey(stat.Key))
                    data.PlayedGamesData[stat.Key] = stat.Value;
                else
                    data.PlayedGamesData.Add(stat.Key, stat.Value);
            }
        }
    }
}