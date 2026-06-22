using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.IO;

namespace Assets.Scripts.Utilities.DataManagement.DataRepository
{
    public class LocalFileDataRepository : IDataRepository
    {
        private readonly string _folderPath;
        private readonly string _saveFileExtension;

        public LocalFileDataRepository(string folderPath, string saveFileExtension)
        {
            _folderPath = folderPath;
            _saveFileExtension = saveFileExtension;
        }

        public async UniTask<bool> Exists(string key)
        {
            bool exists = File.Exists(FullPathFor(key));
            await UniTask.CompletedTask;
            return exists;
        }

        public async UniTask<string> Read(string key)
        {
            string text = File.ReadAllText(FullPathFor(key));
            await UniTask.CompletedTask;
            return text;
        }

        public async UniTask Remove(string key)
        {
            File.Delete(FullPathFor(key));
            await UniTask.CompletedTask;
        }

        public async UniTask Write(string key, string serializedData)
        {
            File.WriteAllText(FullPathFor(key), serializedData);
            await UniTask.CompletedTask;
        }

        private string FullPathFor(string key)
            => Path.Combine(_folderPath, key) + "." + _saveFileExtension;
    }
}