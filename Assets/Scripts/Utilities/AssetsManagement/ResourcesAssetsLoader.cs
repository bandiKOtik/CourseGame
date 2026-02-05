using UnityEngine;

namespace Assets.Scripts.Utilities.AssetsManagement
{
    public class ResourcesAssetsLoader
    {
        public T Load<T>(string resourcesPath) where T : Object
            => Resources.Load<T>(resourcesPath);
    }
}