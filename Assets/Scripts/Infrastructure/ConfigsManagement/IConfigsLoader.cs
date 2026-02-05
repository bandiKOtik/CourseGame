using System;
using System.Collections;
using System.Collections.Generic;

namespace Assets.Scripts.Infrastructure.ConfigsManagement
{
    public interface IConfigsLoader
    {
        IEnumerator LoadAsync(Action<Dictionary<Type, object>> onConfigsLoaded);
    }
}