using Assets.Scripts.Runtime.Gameplay.EntitiesCore;
using Assets.Scripts.Runtime.Gameplay.EntitiesCore.Systems;
using Assets.Scripts.Utilities.Reactive;
using System;

namespace Assets.Scripts.Runtime.Gameplay.Features.LootFeature
{
    public class CollectExperienceToTargetSystem : IInitializableSystem, IDisposableSystem
    {
        private ReactiveVariable<Entity> _target;
        private ReactiveVariable<float> _experience;
        private ReactiveVariable<bool> _isCollected;

        private IDisposable collectedChangeDisposable;

        public void OnInit(Entity entity)
        {
            _target = entity.CurrentTarget;
            _experience = entity.Experience;
            _isCollected = entity.IsCollected;

            collectedChangeDisposable = _isCollected.Subscribe(OnCollectedChanged);
        }

        public void OnDispose()
        {
            collectedChangeDisposable.Dispose();
        }

        private void OnCollectedChanged(bool arg1, bool isCollected)
        {
            if (isCollected)
                _target.Value.Experience.Value += _experience.Value;
        }
    }
}