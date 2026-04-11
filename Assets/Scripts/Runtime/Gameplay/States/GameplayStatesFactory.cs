using Assets.Scripts.Infrastructure.DI_Container;
using Assets.Scripts.Infrastructure.Gameplay;
using Assets.Scripts.Meta.Features.LevelsProgression;
using Assets.Scripts.Runtime.Gameplay.Features.InputManagement;
using Assets.Scripts.Runtime.Gameplay.Features.MainHero;
using Assets.Scripts.Runtime.Gameplay.Features.StagesFeature;
using Assets.Scripts.Utilities.Conditions;
using Assets.Scripts.Utilities.CoroutinesManagement;
using Assets.Scripts.Utilities.DataManagement.DataProviders;
using Assets.Scripts.Utilities.SceneManagement;

namespace Assets.Scripts.Runtime.Gameplay.States
{
    public class GameplayStatesFactory
    {
        private readonly DIContainer _container;

        public GameplayStatesFactory(DIContainer container)
        {
            _container = container;
        }

        public PreperationState CreatePreperationState() => new(_container.Resolve<PreperationTriggerService>());

        public StageProcessState CreateStageProcessState() => new(_container.Resolve<StageProviderService>());

        public WinState CreateWinState(GameplayInputArgs args)
        {
            return new(args,
                _container.Resolve<LevelsProgressionService>(),
                _container.Resolve<PlayerDataProvider>(),
                _container.Resolve<SceneSwitcherService>(),
                _container.Resolve<ICoroutinesPerformer>(),
                _container.Resolve<IInputService>());
        }

        public DefeatState CreateDefeatState()
        {
            return new(
                _container.Resolve<SceneSwitcherService>(),
                _container.Resolve<ICoroutinesPerformer>(),
                _container.Resolve<IInputService>());
        }

        public GameplayStateMachine CreateGameplayStateMachine(GameplayInputArgs args)
        {
            // Заменить триггер на IInputService ивент активации режима
            var preperationTrigger = _container.Resolve<PreperationTriggerService>();
            var stageProvider = _container.Resolve<StageProviderService>();
            var heroHoler = _container.Resolve<MainHeroHolderService>();

            var coreLoopState = CreateCoreLoopState();
            var winState = CreateWinState(args);
            var defeatState = CreateDefeatState();

            ICompositeCondition coreLoopToWinStateCondition = new CompositeCondition()
                .Add(new FuncCondition(() => preperationTrigger.HasContact.Value))
                .Add(new FuncCondition(() => stageProvider.CurrentStageResult.Value == StageResults.Completed))
                .Add(new FuncCondition(() => stageProvider.HasNextStage == false));

            ICompositeCondition coreLoopToDefeatStateCondition = new CompositeCondition()
                .Add(new FuncCondition(() =>
                {
                    if (heroHoler.MainHero != null)
                        return heroHoler.MainHero.IsDead.Value;

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
            var preperationTrigger = _container.Resolve<PreperationTriggerService>();
            var stageProvider = _container.Resolve<StageProviderService>();

            PreperationState preperationState = CreatePreperationState();
            StageProcessState stageProcessState = CreateStageProcessState();

            ICompositeCondition preperationToStageProcessCondition = new CompositeCondition()
                .Add(new FuncCondition(() => preperationTrigger.HasContact.Value))
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
