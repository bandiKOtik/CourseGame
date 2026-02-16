using Assets.Scripts.Configs.Meta.GameModeConfigs;
using Assets.Scripts.Infrastructure.Gameplay;
using Assets.Scripts.Meta.Statistics;
using Assets.Scripts.Meta.Wallet;
using Assets.Scripts.Runtime.Gameplay;
using Assets.Scripts.Utilities.CoroutinesManagement;
using Assets.Scripts.Utilities.DataManagement.DataProviders;
using Assets.Scripts.Utilities.SceneManagement;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Runtime.InputManagement
{
    public class GameModeSelector : IInputHandler
    {
        private ICoroutinesPerformer _performer;
        private SceneSwitcherService _sceneSwitcher;
        private PlayerDataProvider _playerDataProvider;

        private GameModeConfig _gameModeConfig;
        private WalletService _wallet;
        private PlayedGamesStatistic _statistic;

        private GameMode _currentGamemode;
        private bool _requested = false;

        private IReadOnlyDictionary<CurrencyTypes, int> _gameEnterPrices;

        public GameModeSelector(
            ICoroutinesPerformer performer,
            SceneSwitcherService sceneSwitcher,
            PlayerDataProvider playerDataProvider,
            GameModeConfig gameModeConfig,
            WalletService walletService,
            PlayedGamesStatistic statistic,
            IReadOnlyDictionary<CurrencyTypes, int> gameEnterPrices)
        {
            _performer = performer;
            _sceneSwitcher = sceneSwitcher;
            _playerDataProvider = playerDataProvider;
            _gameModeConfig = gameModeConfig;
            _wallet = walletService;
            _statistic = statistic;
            _gameEnterPrices = gameEnterPrices;
        }

        public void Update() => ListenUserInput();

        private void ListenUserInput()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                foreach (var curr in _wallet.AvaiableCurrencies)
                    Debug.Log(curr + " : " + _wallet.GetCurrency(curr).Value);

                foreach (var stat in _statistic.AvaiableStatistics)
                    Debug.Log(stat + " : " + _statistic.GetStatValue(stat));
            }

            if (Input.GetKeyDown(KeyCode.R))
            {
                Debug.Log("User data reset.");
                _playerDataProvider.Reset();
            }

            if (Input.GetKeyUp(KeyCode.S))
                _performer.StartPerform(_playerDataProvider.SaveAsync());

            if (Input.GetKeyDown(KeyCode.Alpha1))
                TryEnter(GameMode.Numbers);

            if (Input.GetKeyDown(KeyCode.Alpha2))
                TryEnter(GameMode.Letters);

            if (_requested)
            {
                _requested = false;

                foreach (var price in _gameEnterPrices)
                    _wallet.Spend(price.Key, price.Value);

                GameplayInputArgs args = new(_currentGamemode, _gameModeConfig);

                _performer
                    .StartPerform(_sceneSwitcher
                    .ProcessSwitchTo(Scenes.Gameplay, args));
            }
        }

        private void TryEnter(GameMode mode)
        {
            foreach (var price in _gameEnterPrices)
            {
                if (_wallet.Enough(price.Key, price.Value) == false)
                {
                    Debug.Log($"Not enough {price.Key} to enter." +
                        $"You have: {_wallet.GetCurrency(price.Key).Value}, game price is: {price.Value}");
                    return;
                }
            }

            _currentGamemode = mode;
            _requested = true;
        }
    }
}