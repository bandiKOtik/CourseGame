using Assets.Scripts.Meta.Statistics;
using Assets.Scripts.Meta.Wallet;
using System.Collections.Generic;

namespace Assets.Scripts.Utilities.DataManagement
{
    public class PlayerData : ISaveData
    {
        public Dictionary<CurrencyTypes, int> WalletData;
        public Dictionary<GameStatType, int> PlayedGamesData;
    }
}