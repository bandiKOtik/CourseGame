using Assets.Scripts.Utilities.DataManagement.DataRepository;
using Assets.Scripts.Utilities.DataManagement.KeysStorage;
using Assets.Scripts.Utilities.DataManagement.Serializers;
using Assets.Scripts.Utilities.SaveScreen;
using System;
using System.Collections;

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

        public IEnumerator Save<TData>(TData data) where TData : ISaveData
        {
            _saveScreen.Show();

            string serializedData = _serializer.Serialize(data);
            string key = _dataKeysStorage.GetKeyFor<TData>();

            yield return _repository.Write(key, serializedData);

            _saveScreen.Hide();
        }

        public IEnumerator Load<TData>(Action<TData> onLoad) where TData : ISaveData
        {
            string key = _dataKeysStorage.GetKeyFor<TData>();
            string serializedData = "";
            yield return _repository.Read(key, result => serializedData = result);
            TData data = _serializer.Deserialize<TData>(serializedData);
            onLoad?.Invoke(data);
        }

        public IEnumerator Remove<TData>() where TData : ISaveData
        {
            string key = _dataKeysStorage.GetKeyFor<TData>();
            yield return _repository.Remove(key);
        }

        public IEnumerator Exists<TData>(Action<bool> onExistsResult) where TData : ISaveData
        {
            string key = _dataKeysStorage.GetKeyFor<TData>();
            yield return _repository.Exists(key, result => onExistsResult?.Invoke(result));
        }
    }
}