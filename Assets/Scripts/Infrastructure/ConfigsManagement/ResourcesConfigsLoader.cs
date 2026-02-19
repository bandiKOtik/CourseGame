using Assets.Scripts.Configs.Meta.Wallet;
using Assets.Scripts.Utilities.AssetsManagement;
using System;
using System.Collections;
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
            { typeof(CurrencyIconsConfig), "Configs/Meta/Wallet/CurrencyIconsConfig" }
        };

        public ResourcesConfigsLoader(ResourcesAssetsLoader loader)
        {
            _loader = loader;
        }

        public IEnumerator LoadAsync(Action<Dictionary<Type, object>> onConfigsLoaded)
        {
            Dictionary<Type, object> loadedConfigs = new();

            foreach (KeyValuePair<Type, string> configsPath in _configsPath)
            {
                ScriptableObject config = _loader.Load<ScriptableObject>(configsPath.Value);
                loadedConfigs.Add(configsPath.Key, config);
                yield return null;
            }

            onConfigsLoaded?.Invoke(loadedConfigs);
        }
    }
}