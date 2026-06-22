using Cysharp.Threading.Tasks;
using System;

namespace Assets.Scripts.Utilities.DataManagement.DataRepository
{
    public interface IDataRepository
    {
        UniTask<string> Read(string key);

        UniTask Write(string key, string serializedData);

        UniTask Remove(string key);

        UniTask<bool> Exists(string key);
    }
}