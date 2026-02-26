using Assets.Scripts.Meta.Features.Wallet;
using Assets.Scripts.Runtime.UI.CommonViews;
using Assets.Scripts.Runtime.UI.Core;
using Assets.Scripts.Utilities.Factory.UI;
using System.Collections.Generic;

namespace Assets.Scripts.Runtime.UI.Wallet
{
    public class WalletPresenter : IPresenter
    {
        private readonly WalletService _walletService;
        private readonly ProjectPresentersFactory _presentersFactory;
        private readonly ViewsFactory _viewsFactory;

        private readonly IconTextListView _view;

        private readonly List<CurrencyPresenter> _presenters = new();

        public WalletPresenter(
            WalletService walletService,
            ProjectPresentersFactory presentersFactory,
            ViewsFactory viewsFactory,
            IconTextListView view)
        {
            _walletService = walletService;
            _presentersFactory = presentersFactory;
            _viewsFactory = viewsFactory;
            _view = view;
        }

        public void Initialize()
        {
            foreach (var currencyType in _walletService.AvaiableCurrencies)
            {
                var currencyView = _viewsFactory.Create<IconTextView>(ViewIDs.CurrencyView);

                _view.Add(currencyView);

                var currencyPresenter = _presentersFactory.CreateCurrencyPresenter(
                    currencyView,
                    _walletService.GetCurrency(currencyType),
                    currencyType);

                currencyPresenter.Initialize();
                _presenters.Add(currencyPresenter);
            }
        }

        public void Dispose()
        {
            foreach (var presenter in _presenters)
            {
                _view.Remove(presenter.View);
                _viewsFactory.Release(presenter.View);
                presenter.Dispose();
            }

            _presenters.Clear();
        }
    }
}