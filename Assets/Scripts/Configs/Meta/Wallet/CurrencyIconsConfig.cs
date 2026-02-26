using Assets.Scripts.Meta.Features.Wallet;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Configs.Meta.Wallet
{
    [CreateAssetMenu(menuName = "Configs/Meta/CurrencyIconConfig", fileName = "CurrencyIconConfig")]
    public class CurrencyIconsConfig : ScriptableObject
    {
        [SerializeField] private List<CurrencyConfig> _configs;

        public Sprite GetSpriteFor(CurrencyTypes type)
            => _configs.First(config => config.Type == type).Sprite;

        [Serializable]
        private class CurrencyConfig
        {
            [field: SerializeField] public CurrencyTypes Type { get; private set; }
            [field: SerializeField] public Sprite Sprite { get; private set; }
        }
    }
}