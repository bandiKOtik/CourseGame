using Assets.Scripts.Meta.Statistics;
using Assets.Scripts.Meta.Wallet;
using Assets.Scripts.Runtime.Gameplay;
using System;

namespace Assets.Scripts.Meta
{
    public class StatisticManageService : IDisposable
    {
        private GameSession _gameSession;
        private WalletService _walletService;
        private PlayedGamesStatistic _playerStatistic;

        public StatisticManageService(
            GameSession gameSession,
            WalletService walletService,
            PlayedGamesStatistic playerStatistic)
        {
            _gameSession = gameSession;
            _walletService = walletService;
            _playerStatistic = playerStatistic;

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
            _walletService.Add(CurrencyTypes.Gold, 10);
            _playerStatistic.Increase(GameStatType.Win);
        }

        private void DefeatRewards()
        {
            _playerStatistic.Increase(GameStatType.Defeat);
        }
    }
}
