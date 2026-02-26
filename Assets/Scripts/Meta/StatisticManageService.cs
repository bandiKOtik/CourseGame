using Assets.Scripts.Meta.Features.Wallet;
using Assets.Scripts.Meta.Statistics;
using Assets.Scripts.Runtime.Gameplay;
using System;
using System.Collections.Generic;

namespace Assets.Scripts.Meta
{
    public class StatisticManageService : IDisposable
    {
        private GameSession _gameSession;
        private WalletService _walletService;
        private PlayedGamesStatistic _playerStatistic;
        IReadOnlyDictionary<CurrencyTypes, int> _winCashback;
        IReadOnlyDictionary<CurrencyTypes, int> _defeatPrice;

        public StatisticManageService(
            GameSession gameSession,
            WalletService walletService,
            PlayedGamesStatistic playerStatistic,
            IReadOnlyDictionary<CurrencyTypes, int> winCashback,
            IReadOnlyDictionary<CurrencyTypes, int> defeatPrice)
        {
            _gameSession = gameSession;
            _walletService = walletService;
            _playerStatistic = playerStatistic;
            _winCashback = winCashback;
            _defeatPrice = defeatPrice;

            _gameSession.Win += WinRewards;
            _gameSession.Defeat += DefeatRewards;
        }

        public void Dispose()
        {
            _gameSession.Win -= WinRewards;
            _gameSession.Defeat -= DefeatRewards;
        }

        private void WinRewards()
        {
            foreach (var cashback in _winCashback)
                _walletService.Append(cashback.Key, cashback.Value);

            _playerStatistic.Increase(GameStatType.Win);
        }

        private void DefeatRewards()
        {
            foreach (var cashback in _defeatPrice)
                if (_walletService.Enough(cashback.Key, cashback.Value))
                    _walletService.Spend(cashback.Key, cashback.Value);

            _playerStatistic.Increase(GameStatType.Defeat);
        }
    }
}