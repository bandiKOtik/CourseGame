using Assets.Scripts.Configs.Meta.Wallet;
using Assets.Scripts.Infrastructure.ConfigsManagement;
using Assets.Scripts.Infrastructure.DI_Container;
using Assets.Scripts.Meta.Wallet;
using Assets.Scripts.Runtime.UI.CommonViews;
using Assets.Scripts.Runtime.UI.Wallet;
using Assets.Scripts.Utilities.Reactive;

namespace Assets.Scripts.Utilities.Factory.UI
{
    public class ProjectPresentersFactory
    {
        private readonly DIContainer _container;

        public ProjectPresentersFactory(DIContainer container)
        {
            _container = container;
        }

        public CurrencyPresenter CreateCurrencyPresenter(
            IconTextView view,
            IReadOnlyVariable<int> currency,
            CurrencyTypes currencyType)
        {
            var config = _container
                .Resolve<ConfigsProviderService>()
                .GetConfig<CurrencyIconsConfig>();

            return new(currency, currencyType, config, view);
        }

        public WalletPresenter CreateWalletPresenter(IconTextListView view)
        {
            var walletService = _container.Resolve<WalletService>();
            var viewsFactory = _container.Resolve<ViewsFactory>();

            return new(walletService, this, viewsFactory, view);
        }
    }
}
