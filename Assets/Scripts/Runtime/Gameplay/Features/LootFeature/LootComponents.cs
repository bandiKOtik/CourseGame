using Assets.Scripts.Runtime.Gameplay.EntitiesCore;
using Assets.Scripts.Utilities.Conditions;
using Assets.Scripts.Utilities.Reactive;

namespace Assets.Scripts.Runtime.Gameplay.Features.LootFeature
{
    public class IsPullable : IEntityComponent
    {
    }

    public class InPullingProcess : IEntityComponent
    {
        public ReactiveVariable<bool> Value;
    }

    public class IsCollected : IEntityComponent
    {
        public ReactiveVariable<bool> Value;
    }

    public class LootIsDropped : IEntityComponent
    {
        public ReactiveVariable<bool> Value;
    }

    public class CanDropLoot : IEntityComponent
    {
        public ICompositeCondition Value;
    }
}