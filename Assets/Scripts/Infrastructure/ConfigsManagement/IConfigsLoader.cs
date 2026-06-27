using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;

namespace Assets.Scripts.Infrastructure.ConfigsManagement
{
    public interface IConfigsLoader
    {
        UniTask LoadAsync(Action<Dictionary<Type, object>> onConfigsLoaded);
    }
}