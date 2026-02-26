using Assets.Scripts.Meta.Features.Wallet;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Configs.Meta.Wallet
{
    [CreateAssetMenu(menuName = "Configs/Meta/GamePriceConfig", fileName = "GamePriceConfig")]
    public class GamePriceConfig : ScriptableObject
    {
        [SerializeField] private List<ResetPriceConfig> _resetPrice;
        [SerializeField] private List<PriceConfig> _values;

        public IReadOnlyDictionary<CurrencyTypes, int> GetResetValues()
        {
            return _resetPrice.ToDictionary(
                config => config.Currency,
                config => config.Price
            );
        }

        public IReadOnlyDictionary<CurrencyTypes, int> GetDefeatPrice()
        {
            return _values.ToDictionary(
                config => config.Currency,
                config => config.DefeatPrice
            );
        }

        public IReadOnlyDictionary<CurrencyTypes, int> GetWinCashback()
        {
            return _values.ToDictionary(
                config => config.Currency,
                config => config.WinCashback
            );
        }

        [Serializable]
        private class PriceConfig
        {
            [field: SerializeField] public CurrencyTypes Currency { get; private set; }
            [field: SerializeField] public int DefeatPrice { get; private set; } = 10;
            [field: SerializeField] public int WinCashback { get; private set; } = 20;
        }

        [Serializable]
        private class ResetPriceConfig
        {
            [field: SerializeField] public CurrencyTypes Currency { get; private set; }
            [field: SerializeField] public int Price { get; private set; }
        }
    }
}