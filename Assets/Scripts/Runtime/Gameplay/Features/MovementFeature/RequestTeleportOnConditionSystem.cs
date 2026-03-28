using Assets.Scripts.Runtime.Gameplay.EntitiesCore;
using Assets.Scripts.Runtime.Gameplay.EntitiesCore.Systems;
using Assets.Scripts.Utilities.Conditions;
using Assets.Scripts.Utilities.Reactive;

namespace Assets.Scripts.Runtime.Gameplay.Features.MovementFeature
{
    public class RequestTeleportOnConditionSystem : IInitializableSystem, IUpdateableSystem
    {
        private ICompositeCondition _mustRequest;
        private ReactiveEvent _request;

        public void OnInit(Entity entity)
        {
            _mustRequest = entity.MustRequestTeleport;
            _request = entity.TeleportRequest;
        }

        public void OnUpdate(float deltaTime)
        {
            if (_mustRequest.Evaluate())
            {
                _request.Invoke();
            }
        }
    }
}