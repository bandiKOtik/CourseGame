using Assets.Scripts.Runtime.Gameplay.EntitiesCore;
using Assets.Scripts.Runtime.Gameplay.EntitiesCore.Systems;
using Assets.Scripts.Utilities.Reactive;
using System;

namespace Assets.Scripts.Runtime.Gameplay.Features.LootFeature
{
    public class CollectHealthToTargetSystem : IInitializableSystem, IDisposableSystem
    {
        private ReactiveVariable<Entity> _target;
        private ReactiveVariable<float> _health;
        private ReactiveVariable<bool> _isCollected;

        private IDisposable collectedChangeDisposable;

        public void OnInit(Entity entity)
        {
            _target = entity.CurrentTarget;
            _health = entity.CurrentHealth;
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
            {
                var currentHealth = _target.Value.CurrentHealth;
                var maxHealth = _target.Value.MaxHealth;

                if (currentHealth.Value + _health.Value > maxHealth.Value)
                {
                    currentHealth.Value = maxHealth.Value;
                    return;
                }

                currentHealth.Value += _health.Value;
            }
        }
    }
}