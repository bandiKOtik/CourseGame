using Assets.Scripts.Runtime.Gameplay.EntitiesCore;
using Assets.Scripts.Utilities.Reactive;
using Assets.Scripts.Utilities.StateMachineCore;

namespace Assets.Scripts.Runtime.Gameplay.Features.AI.States
{
    public class FindTargetState : State, IUpdateableState
    {
        private ITargetSelector _selector;
        private EntitiesLifeContext _lifeContext;
        private ReactiveVariable<Entity> _target;

        public FindTargetState(ITargetSelector selector, EntitiesLifeContext lifeContext, Entity entity)
        {
            _selector = selector;
            _lifeContext = lifeContext;
            _target = entity.CurrentTarget;
        }

        public void Update(float deltaTime)
        {
            _target.Value = _selector.SelectTargetFrom(_lifeContext.Entities);
        }
    }
}