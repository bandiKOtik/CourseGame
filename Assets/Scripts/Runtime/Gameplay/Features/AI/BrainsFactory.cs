using Assets.Scripts.Configs.Meta.Wallet;
using Assets.Scripts.Infrastructure.ConfigsManagement;
using Assets.Scripts.Infrastructure.DI_Container;
using Assets.Scripts.Meta.Features.Wallet;
using Assets.Scripts.Runtime.Gameplay.EntitiesCore;
using Assets.Scripts.Runtime.Gameplay.EntitiesCore.Factory;
using Assets.Scripts.Runtime.Gameplay.Features.AI.States;
using Assets.Scripts.Runtime.Gameplay.Features.InputManagement;
using Assets.Scripts.Runtime.Gameplay.Features.PauseFeature;
using Assets.Scripts.Runtime.Gameplay.Features.StagesFeature;
using Assets.Scripts.Utilities.Conditions;
using Assets.Scripts.Utilities.Reactive;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Runtime.Gameplay.Features.AI
{
    public class BrainsFactory
    {
        private readonly AIBrainsContext _context;
        private readonly EntitiesLifeContext _lifeContext;
        private readonly IPauseService _pauseService;
        private readonly IInputService _inputService;
        private readonly StageProviderService _stageProvider;
        private readonly ConfigsProviderService _configsProvider;
        private readonly WalletService _walletService;

        public BrainsFactory(
            AIBrainsContext aIBrainsContext,
            EntitiesLifeContext entitiesLifeContext,
            IPauseService pauseService,
            IInputService inputService,
            StageProviderService stageProvider,
            ConfigsProviderService configsProvider,
            WalletService walletService)
        {
            _context = aIBrainsContext;
            _lifeContext = entitiesLifeContext;
            _pauseService = pauseService;
            _inputService = inputService;
            _stageProvider = stageProvider;
            _configsProvider = configsProvider;
            _walletService = walletService;
        }

        public StateMachineBrain CreateTargetWalkBrain(Entity entity, ITargetSelector selector)
        {
            FindTargetState findTargetState = new(selector, _lifeContext, entity);
            TargetMovementState movementState = new(entity);
            EmptyState waitingState = new();

            ReactiveVariable<Entity> target = entity.CurrentTarget;

            AIStateMachine movementStateMachine = new();
            movementStateMachine.AddState(movementState);
            movementStateMachine.AddState(waitingState);

            ICompositeCondition findStateToMovementCondition = new CompositeCondition()
                .Add(new FuncCondition(() => target.Value != null));

            ICompositeCondition movementToFindStateCondition = new CompositeCondition(LogicOperations.Or)
                .Add(new FuncCondition(() => target.Value == null));

            movementStateMachine.AddTransition(movementState, waitingState, movementToFindStateCondition);
            movementStateMachine.AddTransition(waitingState, movementState, findStateToMovementCondition);

            AIParallelState parallel = new(findTargetState, movementStateMachine);

            AIStateMachine behavior = new();

            behavior.AddState(parallel);

            var brain = new StateMachineBrain(behavior);
            _context.SetFor(entity, brain);

            return brain;
        }

        public StateMachineBrain CreateExplosiveShooterBrain(Entity entity)
        {
            var explodeState = new InputRaycastExplosionState(entity, _inputService);

            var config = _configsProvider.GetConfig<GamePriceConfig>();

            IReadOnlyDictionary<CurrencyTypes, int> minePrice = config.GetMinePrice();

            var setMineState = new InputPlantMineState(
                //_entitiesFactory,
                _inputService,
                _walletService,
                minePrice);

            var emptyState = new EmptyState();

            ReactiveVariable<Vector3> target = new(Input.mousePosition);

            ICompositeCondition mineToExplosionCondition = new CompositeCondition()
                .Add(new FuncCondition(() => _stageProvider.CurrentStageResult.Value == StageResults.Uncompleted));

            ICompositeCondition explosionToMineCondition = new CompositeCondition()
                .Add(new FuncCondition(() => _stageProvider.CurrentStageResult.Value == StageResults.Completed));

            ICompositeCondition toEndgameState = new CompositeCondition()
                .Add(new FuncCondition(() => _stageProvider.CurrentStageResult.Value == StageResults.Completed))
                .Add(new FuncCondition(() => _stageProvider.HasNextStage == false));

            ICondition toPauseState = new FuncCondition(() => _pauseService.IsPaused);
            ICondition fromPauseState = new FuncCondition(() => _pauseService.IsPaused == false);

            AIStateMachine behavior = new();

            behavior.AddState(explodeState);
            behavior.AddState(setMineState);
            behavior.AddState(emptyState);

            behavior.AddTransition(explodeState, emptyState, toPauseState);                     // When game is paused
            behavior.AddTransition(setMineState, emptyState, toPauseState);                     // ***
            behavior.AddTransition(emptyState, explodeState, fromPauseState);                   // Returning from pause
            behavior.AddTransition(emptyState, setMineState, fromPauseState);                   // ***

            behavior.AddTransition(explodeState, emptyState, toEndgameState);                   // Endgame
            behavior.AddTransition(setMineState, emptyState, toEndgameState);                   // ***

            behavior.AddTransition(setMineState, explodeState, mineToExplosionCondition);       // In-round state
            behavior.AddTransition(explodeState, setMineState, explosionToMineCondition);       // Between rounds

            StateMachineBrain brain = new(behavior);
            _context.SetFor(entity, brain);

            return brain;
        }
    }
}