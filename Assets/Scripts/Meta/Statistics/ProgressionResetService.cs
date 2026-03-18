using Assets.Scripts.Configs.Meta.Wallet;
using Assets.Scripts.Meta.Features.Wallet;

namespace Assets.Scripts.Meta.Statistics
{
    public class ProgressionResetService
    {
        private readonly WalletService _wallet;
        private readonly PlayedGamesStatistic _statistics;
        private readonly GamePriceConfig _prices;

        public ProgressionResetService(WalletService wallet, PlayedGamesStatistic statistics, GamePriceConfig prices)
        {
            _wallet = wallet;
            _statistics = statistics;
            _prices = prices;
        }

        public void Reset()
        {
            if (EnoughToReset() && IsNotDefaultStatistics())
            {
                foreach (var price in _prices.GetResetValues())
                    _wallet.Spend(price.Key, price.Value);

                _statistics.Reset();
            }
        }

        private bool EnoughToReset()
        {
            foreach (var price in _prices.GetResetValues())
                if (_wallet.Enough(price.Key, price.Value) == false)
                    return false;

            return true;
        }

        private bool IsNotDefaultStatistics()
        {
            int amount = _statistics.AvaiableStatistics.Count;
            int iterations = 0;

            foreach (var stat in _statistics.AvaiableStatistics)
                if (_statistics.GetStatValue(stat) == default)
                    iterations++;

            return iterations < amount;
        }
    }
}