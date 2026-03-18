using Assets.Scripts.Runtime.Gameplay.EntitiesCore;
using Assets.Scripts.Utilities.Conditions;
using Assets.Scripts.Utilities.Reactive;

namespace Assets.Scripts.Runtime.Gameplay.Features.DamageFeature
{
    public class CanApplyDamage : IEntityComponent
    {
        public ICompositeCondition Value;
    }

    public class TakeDamageRequest : IEntityComponent
    {
        public ReactiveEvent<float> Value;
    }

    public class TakeDamageEvent : IEntityComponent
    {
        public ReactiveEvent<float> Value;
    }

    public class DamageInitialized : IEntityComponent
    {
        public ReactiveVariable<bool> Value;
    }
}