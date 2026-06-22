using Cysharp.Threading.Tasks;
using System;

namespace Assets.Scripts.Utilities.DataManagement
{
    public interface ISaveLoadService
    {
        UniTask<TData> Load<TData>() where TData : ISaveData;

        UniTask Save<TData>(TData data) where TData : ISaveData;

        UniTask Remove<TData>() where TData : ISaveData;

        UniTask<bool> Exists<TData>() where TData : ISaveData;
    }
}