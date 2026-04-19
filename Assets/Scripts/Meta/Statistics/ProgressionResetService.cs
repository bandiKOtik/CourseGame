using Assets.Scripts.Meta.Features.Wallet;
using Assets.Scripts.Utilities.CoroutinesManagement;
using Assets.Scripts.Utilities.DataManagement.DataProviders;
using UnityEngine;

namespace Assets.Scripts.Meta.Statistics
{
    public class ProgressionResetService
    {
        private readonly WalletService _walletService;
        private readonly PlayedGamesStatistic _statistics;
        private readonly ICoroutinesPerformer _performer;
        private readonly PlayerDataProvider _playerDataProvider;

        public ProgressionResetService(
            WalletService walletService,
            PlayedGamesStatistic statistics,
            ICoroutinesPerformer performer,
            PlayerDataProvider playerDataProvider)
        {
            _walletService = walletService;
            _statistics = statistics;
            _performer = performer;
            _playerDataProvider = playerDataProvider;
        }

        public void Reset()
        {
            if (IsNotDefaultStatistics())
            {
                _statistics.Reset();
                _walletService.Reset();

                _performer.StartPerform(_playerDataProvider.SaveAsync());
            }
        }

        private bool IsNotDefaultStatistics()
        {
            int amount = _statistics.AvaiableStatistics.Count;
            int iterations = 0;

            foreach (var stat in _statistics.AvaiableStatistics)
                if (_statistics.GetStatValue(stat) != default)
                    iterations++;

            bool defaultStatistics = iterations != amount;

            amount = _walletService.AvaiableCurrencies.Count;
            iterations = 0;

            foreach (var currency in _walletService.AvaiableCurrencies)
                if (_walletService.GetCurrency(currency).Value != default)
                    iterations++;

            bool defaultCurrencies = iterations != amount;

            return defaultCurrencies || defaultStatistics;
        }
    }
}