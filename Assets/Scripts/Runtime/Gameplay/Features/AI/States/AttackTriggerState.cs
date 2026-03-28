using Assets.Scripts.Runtime.Gameplay.EntitiesCore;
using Assets.Scripts.Utilities.Reactive;
using Assets.Scripts.Utilities.StateMachineCore;

namespace Assets.Scripts.Runtime.Gameplay.Features.AI.States
{
    public class AttackTriggerState : State, IUpdateableState
    {
        private ReactiveEvent _attackRequest;

        public AttackTriggerState(Entity entity)
        {
            _attackRequest = entity.StartAttackRequest;
        }

        public override void Enter()
        {
            base.Enter();

            _attackRequest.Invoke();
        }

        public void Update(float deltaTime)
        {
        }
    }
}