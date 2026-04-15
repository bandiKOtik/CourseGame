using Assets.Scripts.Meta.Features.Wallet;
using Assets.Scripts.Meta.Statistics;
using Assets.Scripts.Runtime.Gameplay;
using System;
using System.Collections.Generic;

namespace Assets.Scripts.Meta
{
    public class StatisticManageService
    {
        private WalletService _walletService;
        private PlayedGamesStatistic _playerStatistic;
        private IReadOnlyDictionary<CurrencyTypes, int> _winCashback;
        private IReadOnlyDictionary<CurrencyTypes, int> _defeatPrice;

        public StatisticManageService(
            WalletService walletService,
            PlayedGamesStatistic playerStatistic,
            IReadOnlyDictionary<CurrencyTypes, int> winCashback,
            IReadOnlyDictionary<CurrencyTypes, int> defeatPrice)
        {
            _walletService = walletService;
            _playerStatistic = playerStatistic;
            _winCashback = winCashback;
            _defeatPrice = defeatPrice;
        }

        public void ApplyWinRewards()
        {
            foreach (var cashback in _winCashback)
                _walletService.Append(cashback.Key, cashback.Value);

            _playerStatistic.Increase(GameStatType.Win);
        }

        public void DefeatRewards()
        {
            foreach (var cashback in _defeatPrice)
                if (_walletService.Enough(cashback.Key, cashback.Value))
                    _walletService.Spend(cashback.Key, cashback.Value);

            _playerStatistic.Increase(GameStatType.Defeat);
        }
    }
}