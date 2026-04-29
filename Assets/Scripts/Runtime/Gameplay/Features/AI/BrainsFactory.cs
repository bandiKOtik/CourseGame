using Assets.Scripts.Configs.Meta.Wallet;
using Assets.Scripts.Infrastructure.ConfigsManagement;
using Assets.Scripts.Infrastructure.DI_Container;
using Assets.Scripts.Meta.Features.Wallet;
using Assets.Scripts.Runtime.Gameplay.EntitiesCore;
using Assets.Scripts.Runtime.Gameplay.EntitiesCore.Factory;
using Assets.Scripts.Runtime.Gameplay.Features.AI.States;
using Assets.Scripts.Runtime.Gameplay.Features.InputManagement;
using Assets.Scripts.Runtime.Gameplay.Features.StagesFeature;
using Assets.Scripts.Utilities.Conditions;
using Assets.Scripts.Utilities.Reactive;
using Assets.Scripts.Utilities.Timer;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Runtime.Gameplay.Features.AI
{
    public class BrainsFactory
    {
        private readonly DIContainer _container;
        private readonly TimerServiceFactory _timerFactory;
        private readonly AIBrainsContext _context;
        private readonly IInputService _inputService;
        private readonly EntitiesLifeContext _lifeContext;

        public BrainsFactory(DIContainer container)
        {
            _container = container;
            _timerFactory = _container.Resolve<TimerServiceFactory>();
            _context = _container.Resolve<AIBrainsContext>();
            _inputService = _container.Resolve<IInputService>();
            _lifeContext = _container.Resolve<EntitiesLifeContext>();
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
            var stageProvider = _container.Resolve<StageProviderService>();

            var explodeState = new InputRaycastExplosionState(entity, _container.Resolve<IInputService>());

            var config = _container
                .Resolve<ConfigsProviderService>()
                .GetConfig<GamePriceConfig>();

            IReadOnlyDictionary<CurrencyTypes, int> minePrice = config.GetMinePrice();

            var setMineState = new InputPlantMineState(
                _container.Resolve<EntitiesFactory>(),
                _container.Resolve<IInputService>(),
                _container.Resolve<WalletService>(),
                minePrice);

            var endgameState = new EmptyState();

            ReactiveVariable<Vector3> target = new(Input.mousePosition);

            ICompositeCondition mineToExplosionCondition = new CompositeCondition()
                .Add(new FuncCondition(() => stageProvider.CurrentStageResult.Value == StageResults.Uncompleted));

            ICompositeCondition explosionToMineCondition = new CompositeCondition()
                .Add(new FuncCondition(() => stageProvider.CurrentStageResult.Value == StageResults.Completed));

            ICompositeCondition toEndgameState = new CompositeCondition()
                .Add(new FuncCondition(() => stageProvider.CurrentStageResult.Value == StageResults.Completed))
                .Add(new FuncCondition(() => stageProvider.HasNextStage == false));

            AIStateMachine behavior = new();

            behavior.AddState(explodeState);
            behavior.AddState(setMineState);
            behavior.AddState(endgameState);

            behavior.AddTransition(explodeState, endgameState, toEndgameState);
            behavior.AddTransition(setMineState, endgameState, toEndgameState);

            behavior.AddTransition(explodeState, setMineState, explosionToMineCondition);
            behavior.AddTransition(setMineState, explodeState, mineToExplosionCondition);

            StateMachineBrain brain = new(behavior);
            _context.SetFor(entity, brain);

            return brain;
        }
    }
}