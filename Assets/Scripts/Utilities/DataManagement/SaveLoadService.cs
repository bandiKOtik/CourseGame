using Assets.Scripts.Utilities.DataManagement.DataRepository;
using Assets.Scripts.Utilities.DataManagement.KeysStorage;
using Assets.Scripts.Utilities.DataManagement.Serializers;
using Assets.Scripts.Utilities.SaveScreen;
using Cysharp.Threading.Tasks;
using System;

namespace Assets.Scripts.Utilities.DataManagement
{
    public class SaveLoadService : ISaveLoadService
    {
        private readonly IDataSerializer _serializer;
        private readonly IDataKeysStorage _dataKeysStorage;
        private readonly IDataRepository _repository;
        private readonly ISaveScreen _saveScreen;

        public SaveLoadService(
            IDataSerializer serializer,
            IDataKeysStorage dataKeysStorage,
            IDataRepository repository,
            ISaveScreen saveScreen)
        {
            _serializer = serializer;
            _dataKeysStorage = dataKeysStorage;
            _repository = repository;
            _saveScreen = saveScreen;
        }

        public async UniTask Save<TData>(TData data) where TData : ISaveData
        {
            _saveScreen.Show();

            string serializedData = _serializer.Serialize(data);
            string key = _dataKeysStorage.GetKeyFor<TData>();

            await _repository.Write(key, serializedData);

            _saveScreen.Hide();
        }

        public async UniTask<TData> Load<TData>() where TData : ISaveData
        {
            string key = _dataKeysStorage.GetKeyFor<TData>();
            string serializedData = "";
            serializedData = await _repository.Read(key);
            TData data = _serializer.Deserialize<TData>(serializedData);
            return data;
        }

        public async UniTask Remove<TData>() where TData : ISaveData
        {
            string key = _dataKeysStorage.GetKeyFor<TData>();
            await _repository.Remove(key);
        }

        public async UniTask<bool> Exists<TData>() where TData : ISaveData
        {
            string key = _dataKeysStorage.GetKeyFor<TData>();
            return await _repository.Exists(key);
        }
    }
}