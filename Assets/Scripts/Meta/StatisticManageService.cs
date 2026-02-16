using Assets.Scripts.Meta.Statistics;
using Assets.Scripts.Meta.Wallet;
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
        private IReadOnlyDictionary<CurrencyTypes, int> _endgameCashback;

        public StatisticManageService(
            GameSession gameSession,
            WalletService walletService,
            PlayedGamesStatistic playerStatistic,
            IReadOnlyDictionary<CurrencyTypes, int> endgameCashback)
        {
            _gameSession = gameSession;
            _walletService = walletService;
            _playerStatistic = playerStatistic;
            _endgameCashback = endgameCashback;

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
            foreach (var cashback in _endgameCashback)
                _walletService.Append(cashback.Key, cashback.Value);

            _playerStatistic.Increase(GameStatType.Win);
        }

        private void DefeatRewards()
        {
            _playerStatistic.Increase(GameStatType.Defeat);
        }
    }
}