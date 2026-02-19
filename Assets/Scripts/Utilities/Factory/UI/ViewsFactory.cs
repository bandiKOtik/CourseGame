using Assets.Scripts.Runtime.UI;
using Assets.Scripts.Utilities.AssetsManagement;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Utilities.Factory.UI
{
    public partial class ViewsFactory
    {
        private readonly ResourcesAssetsLoader _assetsLoader;
        private readonly Dictionary<string, string> _viewIdToResourcesPath = new()
        {
            { ViewIDs.CurrencyView, "UI/View/IconTextView" }
        };

        public ViewsFactory(ResourcesAssetsLoader assetsLoader)
        {
            _assetsLoader = assetsLoader;
        }

        public TView Create<TView>(string viewId, Transform parent = null)
            where TView : MonoBehaviour, IView
        {
            if (_viewIdToResourcesPath.TryGetValue(viewId, out string resourcePath) == false)
                throw new System.ArgumentException("Resource path ar incorrect or empty: "
                    + viewId + " for " + (typeof(TView)));

            GameObject prefab = _assetsLoader.Load<GameObject>(resourcePath);
            GameObject instance = UnityEngine.Object.Instantiate(prefab, parent);
            TView view = instance.GetComponent<TView>();

            if (view == null)
                throw new System.InvalidOperationException(
                    typeof(TView) + " not found on " + nameof(instance));

            return view;
        }

        public void Release<TView>(TView view) where TView : MonoBehaviour, IView
        {
            Object.Destroy(view.gameObject);
        }
    }
}
