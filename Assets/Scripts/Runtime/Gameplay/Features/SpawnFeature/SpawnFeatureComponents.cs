using Assets.Scripts.Runtime.Gameplay.EntitiesCore;
using Assets.Scripts.Utilities.Reactive;

namespace Assets.Scripts.Runtime.Gameplay.Features.SpawnFeature
{
    public class SpawnInitialTime : IEntityComponent
    {
        public ReactiveVariable<float> Value;
    }

    public class SpawnCurrentTime : IEntityComponent
    {
        public ReactiveVariable<float> Value;
    }

    public class InSpawnProcess : IEntityComponent
    {
        public ReactiveVariable<bool> Value;
    }
}