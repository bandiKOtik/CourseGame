using Assets.Scripts.Configs.Meta.Wallet;
using Assets.Scripts.Infrastructure.DI_Container;
using Assets.Scripts.Infrastructure.DIRegistrations;
using Assets.Scripts.Infrastructure.Gameplay;
using Assets.Scripts.Meta;
using Assets.Scripts.Meta.Features.Wallet;
using Assets.Scripts.Meta.Statistics;
using Assets.Scripts.Runtime.Gameplay;
using Assets.Scripts.Runtime.Gameplay.EntitiesCore;
using Assets.Scripts.Runtime.InputManagement;
using Assets.Scripts.Utilities.CoroutinesManagement;
using Assets.Scripts.Utilities.SceneManagement;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Infrastructure.ConfigsManagement.Bootstraps
{
    public class GameplayBootstrap : SceneBootstrap
    {
        private DIContainer _container;
        private IInputHandler _inputHandler;
        private GameSession _session;
        private GameplayContextRegistrations _contextRegistrations = new();

        [SerializeField] private TestGameplayExample _gameplayExample;
        private EntitiesLifeContext _lifeContext;

        private bool _initialized = false;

        public override void ProcessRegistrations(DIContainer container, IInputSceneArgs sceneArgs = null)
        {
            _container = container;

            if (sceneArgs is not GameplayInputArgs args)
                throw new ArgumentException(
                    nameof(sceneArgs) + " is not match with " + typeof(GameplayInputArgs));

            Debug.Log("<color=green>Current level</color>: " + args.LevelNumber);

            _contextRegistrations.Process(_container, args);
        }

        public override IEnumerator Initialize()
        {
            _session = _container.Resolve<GameSession>();

            _inputHandler = _container.Resolve<IInputHandler>();

            var config = _container
                .Resolve<ConfigsProviderService>()
                .GetConfig<GamePriceConfig>();

            IReadOnlyDictionary<CurrencyTypes, int> winCash = config.GetWinCashback();

            IReadOnlyDictionary<CurrencyTypes, int> defeatCash = config.GetDefeatPrice();

            StatisticManageService manager = new(
                _session,
                _container.Resolve<WalletService>(),
                _container.Resolve<PlayedGamesStatistic>(),
                winCash,
                defeatCash);

            _lifeContext = _container.Resolve<EntitiesLifeContext>();

            yield return _container
                .Resolve<ICoroutinesPerformer>()
                .StartPerform(_gameplayExample
                .Initialize(_container));

            yield break;
        }

        private void Update()
        {
            if (_initialized == false)
                return;

            _inputHandler.Update();
            _lifeContext?.Update(Time.deltaTime);
        }

        public override void Run()
        {
            _initialized = true;
            _gameplayExample.Run();
        }
    }
}