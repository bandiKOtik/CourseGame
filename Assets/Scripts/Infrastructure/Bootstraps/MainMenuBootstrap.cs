using Assets.Scripts.Infrastructure.ConfigsManagement.Bootstraps;
using Assets.Scripts.Infrastructure.DI_Container;
using Assets.Scripts.Infrastructure.DIRegistrations;
using Assets.Scripts.Meta.Wallet;
using Assets.Scripts.Runtime.InputManagement;
using Assets.Scripts.Utilities.CoroutinesManagement;
using Assets.Scripts.Utilities.DataManagement;
using Assets.Scripts.Utilities.DataManagement.DataProviders;
using Assets.Scripts.Utilities.SceneManagement;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Infrastructure.Bootstraps
{
    public class MainMenuBootstrap : SceneBootstrap
    {
        private const int GamePrice = 10;
        private DIContainer _container;
        private IInputHandler _inputHandler;
        private MainMenuContextRegistrations _contextRegistrations = new();

        private PlayerData _playerData;
        private WalletService _walletService;

        private bool _initialized = false;

        public override void ProcessRegistrations(DIContainer container, IInputSceneArgs sceneArgs = null)
        {
            _container = container;

            _contextRegistrations.Process(_container);
        }

        public override IEnumerator Initialize()
        {
            ICoroutinesPerformer coroutinesPerformer = _container.Resolve<ICoroutinesPerformer>();

            _inputHandler = _container.Resolve<IInputHandler>();

            _container.Resolve<PlayerDataProvider>();

            _walletService = _container.Resolve<WalletService>();

            yield break;
        }

        private void Update()
        {
            if (_initialized && _walletService.Enough(CurrencyTypes.Gold, GamePrice))
            {
                _walletService.Spend(CurrencyTypes.Gold, GamePrice);
                _inputHandler.Update();
            }
            else
            {
                Debug.LogWarning("Not enough gold to play.");
            }
        }

        public override void Run() => _initialized = true;
    }
}