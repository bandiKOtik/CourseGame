using Assets.Scripts.Configs.Meta.Wallet;
using Assets.Scripts.Infrastructure.ConfigsManagement;
using Assets.Scripts.Meta.Features.Wallet;
using Assets.Scripts.Meta.Statistics;
using System.Collections.Generic;

namespace Assets.Scripts.Utilities.DataManagement.DataProviders
{
    public class PlayerDataProvider : DataProvider<PlayerData>
    {
        private readonly ConfigsProviderService _configsProvider;

        public PlayerDataProvider(
            ISaveLoadService saveLoadService,
            ConfigsProviderService configsProvider) : base(saveLoadService)
        {
            _configsProvider = configsProvider;
        }

        protected override PlayerData GetOriginData()
        {
            return new PlayerData()
            {
                WalletData = InitWalletData(),
                PlayedGamesData = InitGamesData(),
                CompletedLevels = new()
            };
        }

        private Dictionary<CurrencyTypes, int> InitWalletData()
        {
            Dictionary<CurrencyTypes, int> walletData = new();

            StartWalletConfig config = _configsProvider.GetConfig<StartWalletConfig>();

            foreach (CurrencyTypes type in System.Enum.GetValues(typeof(CurrencyTypes)))
                walletData[type] = config.GetValueFor(type);

            return walletData;
        }

        private Dictionary<GameStatType, int> InitGamesData()
        {
            Dictionary<GameStatType, int> gamesData = new();

            foreach (GameStatType type in System.Enum.GetValues(typeof(GameStatType)))
                gamesData[type] = 0;

            return gamesData;
        }
    }
}