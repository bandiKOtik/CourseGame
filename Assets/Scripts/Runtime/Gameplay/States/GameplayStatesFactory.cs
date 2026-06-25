using Assets.Scripts.Configs.Meta.Wallet;
using Assets.Scripts.Infrastructure.ConfigsManagement;
using Assets.Scripts.Infrastructure.DI_Container;
using Assets.Scripts.Infrastructure.Gameplay;
using Assets.Scripts.Meta;
using Assets.Scripts.Meta.Features.Wallet;
using Assets.Scripts.Runtime.Gameplay.Features.InputManagement;
using Assets.Scripts.Runtime.Gameplay.Features.LootFeature;
using Assets.Scripts.Runtime.Gameplay.Features.MainBaseBuilding;
using Assets.Scripts.Runtime.Gameplay.Features.PauseFeature;
using Assets.Scripts.Runtime.Gameplay.Features.StagesFeature;
using Assets.Scripts.Runtime.UI.Gameplay;
using Assets.Scripts.Utilities.Conditions;
using Assets.Scripts.Utilities.DataManagement.DataProviders;
using System.Collections.Generic;

namespace Assets.Scripts.Runtime.Gameplay.States
{
    public class GameplayStatesFactory
    {
        private readonly ConfigsProviderService _configsProvider;
        private readonly StatisticManageService _statisticManageService;
        private readonly PlayerDataProvider _playerDataProvider;
        private readonly IInputService _inputService;
        private readonly GameplayPopupService _gameplayPopupService;
        private readonly IPauseService _pauseService;
        private readonly PreperationInputService _preperationInputService;
        private readonly StageProviderService _stageProviderService;
        private readonly WalletService _walletService;
        private readonly LootPullingService _lootPullingService;
        private readonly MainBaseHolderService _heroHolder;

        public GameplayStatesFactory(
            ConfigsProviderService configsProvider,
            StatisticManageService statisticManageService,
            PlayerDataProvider playerDataProvider,
            IInputService inputService,
            GameplayPopupService gameplayPopupService,
            IPauseService pauseService,
            PreperationInputService preperationInputService,
            StageProviderService stageProviderService,
            WalletService walletService,
            LootPullingService lootPullingService,
            MainBaseHolderService heroHolder)
        {
            _configsProvider = configsProvider;
            _statisticManageService = statisticManageService;
            _playerDataProvider = playerDataProvider;
            _inputService = inputService;
            _gameplayPopupService = gameplayPopupService;
            _pauseService = pauseService;
            _preperationInputService = preperationInputService;
            _stageProviderService = stageProviderService;
            _walletService = walletService;
            _lootPullingService = lootPullingService;
            _heroHolder = heroHolder;
        }

        public PreperationState CreatePreperationState() => new(_preperationInputService);

        public StageProcessState CreateStageProcessState()
        {
            var config = _configsProvider.GetConfig<GamePriceConfig>();

            IReadOnlyDictionary<CurrencyTypes, int> winCash = config.GetWinCashback();

            return new(_stageProviderService, _walletService, winCash);
        }

        public CollectLootState CreateCollectLootState()
        {
            return new(_lootPullingService, _heroHolder);
        }

        public WinState CreateWinState()
        {
            return new(
                _statisticManageService,
                _playerDataProvider,
                _inputService,
                _gameplayPopupService,
                _pauseService);
        }

        public DefeatState CreateDefeatState()
        {
            return new(
                _statisticManageService,
                _playerDataProvider,
                _inputService,
                _gameplayPopupService,
                _pauseService);
        }

        public GameplayStateMachine CreateGameplayStateMachine(GameplayInputArgs args)
        {
            var coreLoopState = CreateCoreLoopState();
            var winState = CreateWinState();
            var defeatState = CreateDefeatState();

            ICompositeCondition coreLoopToWinStateCondition = new CompositeCondition()
                .Add(new FuncCondition(() => _stageProviderService.CurrentStageResult.Value == StageResults.Completed))
                .Add(new FuncCondition(() => _stageProviderService.HasNextStage == false));

            ICompositeCondition coreLoopToDefeatStateCondition = new CompositeCondition()
                .Add(new FuncCondition(() =>
                {
                    if (_heroHolder.MainBase != null)
                        return _heroHolder.MainBase.IsDead.Value;

                    return false;
                }));

            GameplayStateMachine gameplayCycle = new();

            gameplayCycle.AddState(coreLoopState);
            gameplayCycle.AddState(winState);
            gameplayCycle.AddState(defeatState);

            gameplayCycle.AddTransition(coreLoopState, winState, coreLoopToWinStateCondition);
            gameplayCycle.AddTransition(coreLoopState, defeatState, coreLoopToDefeatStateCondition);

            return gameplayCycle;
        }

        public GameplayStateMachine CreateCoreLoopState()
        {
            var collectLootState = CreateCollectLootState();
            var preperationState = CreatePreperationState();
            var stageProcessState = CreateStageProcessState();

            ICompositeCondition preperationToStageProcessCondition = new CompositeCondition()
                .Add(new FuncCondition(() => _preperationInputService.IsReady.Value))
                .Add(new FuncCondition(() => _stageProviderService.HasNextStage));

            FuncCondition stageProcessToCollectStateCondition =
                new(() => _stageProviderService.CurrentStageResult.Value == StageResults.Completed);

            FuncCondition collectStateToPreperationCondition =
                new(() => _lootPullingService.AllCollected.Value);

            GameplayStateMachine coreLoopState = new();

            coreLoopState.AddState(preperationState);
            coreLoopState.AddState(collectLootState);
            coreLoopState.AddState(stageProcessState);

            coreLoopState.AddTransition(preperationState, stageProcessState, preperationToStageProcessCondition);
            coreLoopState.AddTransition(stageProcessState, collectLootState, stageProcessToCollectStateCondition);
            coreLoopState.AddTransition(collectLootState, preperationState, collectStateToPreperationCondition);

            return coreLoopState;
        }
    }
}