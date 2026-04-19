using Assets.Scripts.Configs.Meta.Wallet;
using Assets.Scripts.Infrastructure.ConfigsManagement;
using Assets.Scripts.Infrastructure.DI_Container;
using Assets.Scripts.Infrastructure.Gameplay;
using Assets.Scripts.Meta;
using Assets.Scripts.Meta.Features.Wallet;
using Assets.Scripts.Runtime.Gameplay.Features.InputManagement;
using Assets.Scripts.Runtime.Gameplay.Features.MainBaseBuilding;
using Assets.Scripts.Runtime.Gameplay.Features.StagesFeature;
using Assets.Scripts.Utilities.Conditions;
using Assets.Scripts.Utilities.CoroutinesManagement;
using Assets.Scripts.Utilities.DataManagement.DataProviders;
using Assets.Scripts.Utilities.SceneManagement;
using System.Collections.Generic;

namespace Assets.Scripts.Runtime.Gameplay.States
{
    public class GameplayStatesFactory
    {
        private readonly DIContainer _container;

        public GameplayStatesFactory(DIContainer container)
        {
            _container = container;
        }

        public PreperationState CreatePreperationState() => new(_container.Resolve<PreperationInputService>());

        public StageProcessState CreateStageProcessState()
        {
            var config = _container
                .Resolve<ConfigsProviderService>()
                .GetConfig<GamePriceConfig>();

            IReadOnlyDictionary<CurrencyTypes, int> winCash = config.GetWinCashback();

            return new(_container.Resolve<StageProviderService>(), _container.Resolve<WalletService>(), winCash);
        }

        public WinState CreateWinState()
        {
            return new(
                _container.Resolve<StatisticManageService>(),
                _container.Resolve<PlayerDataProvider>(),
                _container.Resolve<SceneSwitcherService>(),
                _container.Resolve<ICoroutinesPerformer>(),
                _container.Resolve<IInputService>());
        }

        public DefeatState CreateDefeatState()
        {
            return new(
                _container.Resolve<StatisticManageService>(),
                _container.Resolve<PlayerDataProvider>(),
                _container.Resolve<SceneSwitcherService>(),
                _container.Resolve<ICoroutinesPerformer>(),
                _container.Resolve<IInputService>());
        }

        public GameplayStateMachine CreateGameplayStateMachine(GameplayInputArgs args)
        {
            var preperationTrigger = _container.Resolve<PreperationInputService>();
            var stageProvider = _container.Resolve<StageProviderService>();
            var baseHolder = _container.Resolve<MainBaseHolderService>();

            var coreLoopState = CreateCoreLoopState();
            var winState = CreateWinState();
            var defeatState = CreateDefeatState();

            ICompositeCondition coreLoopToWinStateCondition = new CompositeCondition()
                .Add(new FuncCondition(() => stageProvider.CurrentStageResult.Value == StageResults.Completed))
                .Add(new FuncCondition(() => stageProvider.HasNextStage == false));

            ICompositeCondition coreLoopToDefeatStateCondition = new CompositeCondition()
                .Add(new FuncCondition(() =>
                {
                    if (baseHolder.MainBase != null)
                        return baseHolder.MainBase.IsDead.Value;

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
            var preperationTrigger = _container.Resolve<PreperationInputService>();
            var stageProvider = _container.Resolve<StageProviderService>();

            PreperationState preperationState = CreatePreperationState();
            StageProcessState stageProcessState = CreateStageProcessState();

            ICompositeCondition preperationToStageProcessCondition = new CompositeCondition()
                .Add(new FuncCondition(() => preperationTrigger.IsReady.Value))
                .Add(new FuncCondition(() => stageProvider.HasNextStage));

            FuncCondition stageProcessToPreperationCondition =
                new(() => stageProvider.CurrentStageResult.Value == StageResults.Completed);

            GameplayStateMachine coreLoopState = new();

            coreLoopState.AddState(preperationState);
            coreLoopState.AddState(stageProcessState);

            coreLoopState.AddTransition(preperationState, stageProcessState, preperationToStageProcessCondition);
            coreLoopState.AddTransition(stageProcessState, preperationState, stageProcessToPreperationCondition);

            return coreLoopState;
        }
    }
}
