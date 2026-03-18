using Assets.Scripts.Runtime.Gameplay.EntitiesCore;
using Assets.Scripts.Runtime.Gameplay.EntitiesCore.Systems;
using Assets.Scripts.Utilities.Reactive;
using System;

namespace Assets.Scripts.Runtime.Gameplay.Features.Attack.AreaAttack
{
    public class DamageAfterTeleportSystem : IInitializableSystem, IDisposableSystem
    {
        private ReactiveEvent _achieved;
        private ReactiveEvent _attackRequest;

        private IDisposable _subscription;

        public void OnInit(Entity entity)
        {
            _achieved = entity.TeleportDestinationAchieved;
            _attackRequest = entity.StartAttackRequest;

            _subscription = _achieved.Subscribe(OnDestinationAchieved);
        }

        public void OnDispose()
        {
            _subscription.Dispose();
        }

        private void OnDestinationAchieved()
        {
            _attackRequest.Invoke();
        }
    }
}