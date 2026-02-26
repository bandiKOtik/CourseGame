using Assets.Scripts.Meta.Features.Wallet;
using Assets.Scripts.Meta.Statistics;
using System.Collections.Generic;

namespace Assets.Scripts.Utilities.DataManagement
{
    public class PlayerData : ISaveData
    {
        public Dictionary<CurrencyTypes, int> WalletData;
        public Dictionary<GameStatType, int> PlayedGamesData;
        public List<int> CompletedLevels;
    }
}