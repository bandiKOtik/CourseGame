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
        [SerializeField] private List<PriceConfig> _values;

        public IReadOnlyDictionary<CurrencyTypes, int> GetMinePrice()
        {
            return _values.ToDictionary(
                config => config.Currency,
                config => config.PlantMinePrice
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
            [field: SerializeField] public int PlantMinePrice { get; private set; } = 10;
            [field: SerializeField] public int WinCashback { get; private set; } = 20;
        }
    }
}