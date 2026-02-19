using Assets.Scripts.Configs.Meta.Wallet;
using Assets.Scripts.Meta.Wallet;
using Assets.Scripts.Runtime.UI.CommonViews;
using Assets.Scripts.Utilities.Reactive;
using System;

namespace Assets.Scripts.Runtime.UI.Wallet
{
    public class CurrencyPresenter : IPresenter
    {
        private readonly IReadOnlyVariable<int> _currency;
        private readonly CurrencyTypes _currencyType;
        private readonly CurrencyIconsConfig _iconsConfig;

        private readonly IconTextView _view;
        private IDisposable _subscribe;

        public CurrencyPresenter(
            IReadOnlyVariable<int> currency,
            CurrencyTypes currencyType,
            CurrencyIconsConfig iconsConfig,
            IconTextView view)
        {
            _currency = currency;
            _currencyType = currencyType;
            _iconsConfig = iconsConfig;
            _view = view;
        }

        public IconTextView View => _view;

        public void Initialize()
        {
            _view.SetText(_currency.Value.ToString());
            _view.SetIcon(_iconsConfig.GetSpriteFor(_currencyType));

            _subscribe = _currency.Subscribe(OnChanged);
        }

        public void Dispose() => _subscribe.Dispose();

        private void OnChanged(int oldValue, int newValue) => UpdateValue(newValue);

        private void UpdateValue(int value) => _view.SetText(value.ToString());
    }
}
