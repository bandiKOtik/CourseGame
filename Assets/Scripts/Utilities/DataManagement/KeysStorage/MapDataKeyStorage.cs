using System;
using System.Collections.Generic;

namespace Assets.Scripts.Utilities.DataManagement.KeysStorage
{
    public class MapDataKeyStorage : IDataKeysStorage
    {
        private readonly Dictionary<Type, string> Keys = new()
        {
            { typeof(PlayerData), "PlayerData" }
        };

        public string GetKeyFor<TData>() where TData : ISaveData
            => Keys[typeof(TData)];
    }
}