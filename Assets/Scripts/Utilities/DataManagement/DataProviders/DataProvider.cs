using Assets.Scripts.Utilities.CoroutinesManagement;
using System;
using System.Collections;
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

        public IEnumerator SaveAsync()
        {
            UpdateDataFromWriters();

            yield return _saveLoadService.Save(_data);
        }

        public IEnumerator LoadAsync()
        {
            yield return _saveLoadService.Load<TData>(loadedData => _data = loadedData);

            SendDataToReaders();
        }

        public IEnumerator CheckExistsAsync(Action<bool> onResult)
        {
            yield return _saveLoadService.Exists<TData>(result => onResult(result));
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