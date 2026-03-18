using Assets.Scripts.Runtime.Gameplay.EntitiesCore;
using Assets.Scripts.Runtime.Gameplay.EntitiesCore.Systems;
using Assets.Scripts.Utilities.Reactive;

namespace Assets.Scripts.Runtime.Gameplay.Features.LifeCycle
{
    public class ApplyHealthToMaxSystem : IInitializableSystem
    {
        private ReactiveVariable<float> _max;
        private ReactiveVariable<float> _current;

        public void OnInit(Entity entity)
        {
            _max = entity.MaxHealth;
            _current = entity.CurrentHealth;
            _current.Value = _max.Value;
        }
    }
}