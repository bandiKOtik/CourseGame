using Assets.Scripts.Configs.Meta.Wallet;
using Assets.Scripts.Infrastructure.ConfigsManagement;
using Assets.Scripts.Meta.Statistics;
using Assets.Scripts.Meta.Wallet;
using System.Collections.Generic;

namespace Assets.Scripts.Utilities.DataManagement.DataProviders
{
    public class PlayerDataProvider : DataProvider<PlayerData>
    {
        private readonly ConfigsProviderService _configsProviderService;

        public PlayerDataProvider(
            ISaveLoadService saveLoadService,
            ConfigsProviderService configsProviderService) : base(saveLoadService)
        {
            _configsProviderService = configsProviderService;
        }

        protected override PlayerData GetOriginData()
        {
            return new PlayerData()
            {
                WalletData = InitWalletData(),
                PlayedGamesData = InitGamesData()
            };
        }

        private Dictionary<CurrencyTypes, int> InitWalletData()
        {
            Dictionary<CurrencyTypes, int> walletData = new();

            StartWalletConfig config = _configsProviderService.GetConfig<StartWalletConfig>();

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