using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;

namespace Assets.Scripts.Utilities.DataManagement.DataProviders
{
    public abstract class DataProvider<TData> where TData : ISaveData
    {
        private readonly ISaveLoadService _saveLoadService;

        private readonly List<IDataReader<TData>> _readers = new();
        private readonly List<IDataWriter<TData>> _writers = new();

        private TData _data;

        protected DataProvider(ISaveLoadService saveLoadService)
        {
            _saveLoadService = saveLoadService;
        }

        public void RegisterReader(IDataReader<TData> reader)
        {
            if (_readers.Contains(reader))
                throw new ArgumentException("Reader already in register: " + (nameof(reader)));

            _readers.Add(reader);
        }

        public void RegisterWriter(IDataWriter<TData> writer)
        {
            if (_writers.Contains(writer))
                throw new ArgumentException("Writer already in register: " + (nameof(writer)));

            _writers.Add(writer);
        }

        public async UniTask SaveAsync()
        {
            UpdateDataFromWriters();

            await _saveLoadService.Save(_data);
        }

        public async UniTask LoadAsync()
        {
            _data = await _saveLoadService.Load<TData>();

            SendDataToReaders();
        }

        public async UniTask<bool> ExistsAsync()
        {
            return await _saveLoadService.Exists<TData>();
        }

        public void Reset()
        {
            _data = GetOriginData();

            SendDataToReaders();
        }

        protected abstract TData GetOriginData();

        private void SendDataToReaders()
        {
            foreach (var reader in _readers)
                reader.ReadFrom(_data);
        }

        private void UpdateDataFromWriters()
        {
            foreach (var writer in _writers)
                writer.WriteTo(_data);
        }
    }
}