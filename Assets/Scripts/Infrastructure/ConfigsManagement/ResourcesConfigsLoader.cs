using Assets.Scripts.Configs.Gameplay;
using Assets.Scripts.Configs.Gameplay.Abilities;
using Assets.Scripts.Configs.Gameplay.Entities;
using Assets.Scripts.Configs.Gameplay.Levels;
using Assets.Scripts.Configs.Gameplay.Loot;
using Assets.Scripts.Configs.Meta.Wallet;
using Assets.Scripts.Utilities.AssetsManagement;
using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Infrastructure.ConfigsManagement
{
    public class ResourcesConfigsLoader : IConfigsLoader
    {
        private readonly ResourcesAssetsLoader _loader;

        private readonly Dictionary<Type, string> _configsPath = new()
        {
            { typeof(StartWalletConfig), "Configs/Meta/Wallet/StartWalletConfig" },
            { typeof(GamePriceConfig), "Configs/Meta/Wallet/GamePriceConfig" },
            { typeof(CurrencyIconsConfig), "Configs/Meta/Wallet/CurrencyIconsConfig" },
            { typeof(LevelsListConfig), "Configs/Gameplay/Levels/LevelsListConfig" },
            { typeof(DefendableBuildingConfig), "Configs/Gameplay/Entities/MainBaseConfig" },
            { typeof(AbilitiesConfigsContainer), "Configs/Gameplay/Abilities/AbilitiesConfigsContainer" },
            { typeof(ExperienceForUpgradeLevelConfig), "Configs/Gameplay/ExperienceConfig" },
            { typeof(LootListConfig), "Configs/Gameplay/Loot/LootListConfig" },
        };

        public ResourcesConfigsLoader(ResourcesAssetsLoader loader)
        {
            _loader = loader;
        }

        public async UniTask LoadAsync(Action<Dictionary<Type, object>> onConfigsLoaded)
        {
            Dictionary<Type, object> loadedConfigs = new();

            foreach (KeyValuePair<Type, string> configsPath in _configsPath)
            {
                ScriptableObject config = _loader.Load<ScriptableObject>(configsPath.Value);
                loadedConfigs.Add(configsPath.Key, config);
                await UniTask.CompletedTask;
            }

            onConfigsLoaded?.Invoke(loadedConfigs);
        }
    }
}