using Assets.Scripts.Runtime.Gameplay.EntitiesCore;
using Assets.Scripts.Runtime.Gameplay.EntitiesCore.Systems;
using Assets.Scripts.Utilities.Reactive;

namespace Assets.Scripts.Runtime.Gameplay.Features.Attack
{
    public class SelfDestroyAfterAttackSystem : IInitializableSystem
    {
        private ReactiveEvent _attackRequest;
        private ReactiveVariable<bool> _mustDestroy;

        public void OnInit(Entity entity)
        {
            _attackRequest = entity.StartAttackRequest;
            _mustDestroy = entity.MustSelfDestroy;

            _attackRequest.Subscribe(OnAttackRequested);
        }

        private void OnAttackRequested() => _mustDestroy.Value = true;
    }
}