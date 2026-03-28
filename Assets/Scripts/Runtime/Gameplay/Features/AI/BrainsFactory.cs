using Assets.Scripts.Infrastructure.DI_Container;
using Assets.Scripts.Runtime.Gameplay.EntitiesCore;
using Assets.Scripts.Runtime.Gameplay.Features.AI.States;
using Assets.Scripts.Runtime.Gameplay.Features.InputManagement;
using Assets.Scripts.Utilities.Conditions;
using Assets.Scripts.Utilities.Reactive;
using Assets.Scripts.Utilities.Timer;
using System;
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

        public StateMachineBrain CreateRandomTeleportationBrain(Entity entity, float cooldown)
        {
            var stateMachine = CreateRandomTeleportStateMachine(entity, cooldown);
            StateMachineBrain brain = new(stateMachine);

            _context.SetFor(entity, brain);

            return brain;
        }

        public StateMachineBrain CreateTeleportToWeakestBrain(Entity entity, ITargetSelector selector, float cooldown)
        {
            var behavior = CreateTeleportToWeakestMachine(entity, cooldown);
            ReactiveVariable<Entity> target = entity.CurrentTarget;

            FindTargetState findTargetState = new(selector, _lifeContext, entity);
            AIParallelState parallelState = new(findTargetState, behavior);

            AIStateMachine rootMachine = new();
            rootMachine.AddState(parallelState);

            var brain = new StateMachineBrain(rootMachine);
            _context.SetFor(entity, brain);

            return brain;
        }

        public StateMachineBrain CreateRandomWalkBrain(Entity entity)
        {
            var stateMachine = CreateRandomMovementStateMachine(entity);
            StateMachineBrain brain = new(stateMachine);

            _context.SetFor(entity, brain);

            return brain;
        }

        public StateMachineBrain CreateAutoAttackWhenStandBrain(Entity entity, ITargetSelector selector)
        {
            var combatState = CreateAutoAttackStateMachine(entity);
            var movementState = new PlayerInputMovementState(entity, _inputService);
            ReactiveVariable<Entity> target = entity.CurrentTarget;

            ICompositeCondition movementToCombatCondition = new CompositeCondition()
                .Add(new FuncCondition(() => target.Value != null))
                .Add(new FuncCondition(() => _inputService.Direction == Vector3.zero));

            ICompositeCondition combatToMovementCondition = new CompositeCondition(LogicOperations.Or)
                .Add(new FuncCondition(() => target.Value == null))
                .Add(new FuncCondition(() => _inputService.Direction != Vector3.zero));

            AIStateMachine behavior = new();

            behavior.AddState(combatState);
            behavior.AddState(movementState);

            behavior.AddTransition(movementState, combatState, movementToCombatCondition);
            behavior.AddTransition(combatState, movementState, combatToMovementCondition);

            FindTargetState findTargetState = new(selector, _lifeContext, entity);
            AIParallelState parallelState = new(findTargetState, behavior);

            AIStateMachine rootMachine = new();
            rootMachine.AddState(parallelState);

            var brain = new StateMachineBrain(rootMachine);
            _context.SetFor(entity, brain);

            return brain;
        }

        public StateMachineBrain CreateManualShooterBrain(Entity entity)
        {
            var combatState = CreateManualAttackStateMachine(entity);
            var movementState = new PlayerInputMovementState(entity, _inputService);
            ReactiveVariable<Vector3> target = new(Input.mousePosition);

            ICompositeCondition movementToCombatCondition = new CompositeCondition()
                .Add(new FuncCondition(() => target.Value != null))
                .Add(new FuncCondition(() => _inputService.Direction == Vector3.zero));

            ICompositeCondition combatToMovementCondition = new CompositeCondition(LogicOperations.Or)
                .Add(new FuncCondition(() => target.Value == null))
                .Add(new FuncCondition(() => _inputService.Direction != Vector3.zero));

            AIStateMachine behavior = new();

            behavior.AddState(combatState);
            behavior.AddState(movementState);

            behavior.AddTransition(movementState, combatState, movementToCombatCondition);
            behavior.AddTransition(combatState, movementState, combatToMovementCondition);

            //FindTargetState findTargetState = new(selector, _lifeContext, entity);
            //AIParallelState parallelState = new(findTargetState, behavior);

            //AIStateMachine rootMachine = new();
            //rootMachine.AddState(parallelState);

            var brain = new StateMachineBrain(behavior);
            _context.SetFor(entity, brain);

            return brain;
        }

        private AIStateMachine CreateRandomTeleportStateMachine(Entity entity, float cooldown)
        {
            RandomTeleportWithCooldownState teleportState = new(entity, cooldown);

            AIStateMachine stateMachine = new();

            stateMachine.AddState(teleportState);

            return stateMachine;
        }

        private AIStateMachine CreateTeleportToWeakestMachine(Entity entity, float cooldown)
        {
            TeleportToTargetWithCooldownState teleportState = new(entity, cooldown);
            EmptyState restoreState = new();

            ICondition canMove = entity.CanMove;
            ReactiveVariable<float> energy = entity.CurrentEnergy;
            ReactiveVariable<float> maxEnergy = entity.MaxEnergy;
            ReactiveVariable<Entity> currentTarget = entity.CurrentTarget;

            ICondition teleportToRestoreCondition = new FuncCondition(()
                => energy.Value < maxEnergy.Value * 0.4f);

            ICompositeCondition restoreToTeleportCondition = new CompositeCondition()
                .Add(canMove)
                .Add(new FuncCondition(() => energy.Value > maxEnergy.Value * 0.7f));

            var stateMachine = new AIStateMachine();

            stateMachine.AddState(teleportState);
            stateMachine.AddState(restoreState);

            stateMachine.AddTransition(teleportState, restoreState, teleportToRestoreCondition);
            stateMachine.AddTransition(restoreState, teleportState, restoreToTeleportCondition);

            return stateMachine;
        }

        private AIStateMachine CreateRandomMovementStateMachine(Entity entity)
        {
            List<IDisposable> disposables = new();
            RandomMovementState randomMovementState = new(entity, .5f);
            EmptyState emptyState = new();

            var movementTimer = _timerFactory.Create(2f);
            disposables.Add(movementTimer);
            disposables.Add(randomMovementState.Entered.Subscribe(movementTimer.Restart));

            var idleTimer = _timerFactory.Create(3f);
            disposables.Add(idleTimer);
            disposables.Add(emptyState.Entered.Subscribe(idleTimer.Restart));

            FuncCondition movementEndCondition = new(() => movementTimer.IsOver);
            FuncCondition idleEndCondition = new(() => idleTimer.IsOver);

            AIStateMachine stateMachine = new AIStateMachine(disposables);

            stateMachine.AddState(randomMovementState);
            stateMachine.AddState(emptyState);

            stateMachine.AddTransition(randomMovementState, emptyState, movementEndCondition);
            stateMachine.AddTransition(emptyState, randomMovementState, idleEndCondition);

            return stateMachine;
        }

        private AIStateMachine CreateAutoAttackStateMachine(Entity entity)
        {
            RotateToTargetState rotateState = new(entity);
            AttackTriggerState attackState = new(entity);

            ICondition canAttack = entity.CanStartAttack;
            Transform transform = entity.Transform;
            ReactiveVariable<Entity> currentTarget = entity.CurrentTarget;

            ICompositeCondition rotateToAttackCondition = new CompositeCondition()
                .Add(canAttack)
                .Add(new FuncCondition(() =>
                {
                    Entity target = currentTarget.Value;

                    if (target == null)
                        return false;

                    float angleToTarget = Quaternion.Angle(
                        transform.rotation,
                        Quaternion.LookRotation(target.Transform.position - transform.position));

                    return angleToTarget < 1f;
                }));

            ReactiveVariable<bool> inAttackProcess = entity.InAttackProcess;

            ICondition attackToRotateCondition = new FuncCondition(() => inAttackProcess.Value == false);

            var stateMachine = new AIStateMachine();

            stateMachine.AddState(rotateState);
            stateMachine.AddState(attackState);

            stateMachine.AddTransition(rotateState, attackState, rotateToAttackCondition);
            stateMachine.AddTransition(attackState, rotateState, attackToRotateCondition);

            return stateMachine;
        }

        private AIStateMachine CreateManualAttackStateMachine(Entity entity)
        {
            RotateToCursorState rotateState = new(entity);
            AttackTriggerState attackState = new(entity);

            ICondition canAttack = entity.CanStartAttack;
            Transform transform = entity.Transform;

            ICompositeCondition rotateToAttackCondition = new CompositeCondition()
                .Add(canAttack)
                .Add(new FuncCondition(() => _inputService.AttackRequest));

            ReactiveVariable<bool> inAttackProcess = entity.InAttackProcess;

            ICondition attackToRotateCondition = new FuncCondition(() => inAttackProcess.Value == false);

            var stateMachine = new AIStateMachine();

            stateMachine.AddState(rotateState);
            stateMachine.AddState(attackState);

            stateMachine.AddTransition(rotateState, attackState, rotateToAttackCondition);
            stateMachine.AddTransition(attackState, rotateState, attackToRotateCondition);

            return stateMachine;
        }
    }
}