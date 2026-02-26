using Assets.Scripts.Meta.Features.Wallet;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Configs.Meta.Wallet
{
    [CreateAssetMenu(menuName = "Configs/Meta/StartWalletConfig", fileName = "StartWalletConfig")]
    public class StartWalletConfig : ScriptableObject
    {
        [SerializeField] private List<CurrencyConfig> _values;

        public int GetValueFor(CurrencyTypes type)
            => _values.First(config => config.Type == type).Value;

        [Serializable]
        private class CurrencyConfig
        {
            [field: SerializeField] public CurrencyTypes Type { get; private set; }
            [field: SerializeField] public int Value { get; private set; }
        }
    }
}