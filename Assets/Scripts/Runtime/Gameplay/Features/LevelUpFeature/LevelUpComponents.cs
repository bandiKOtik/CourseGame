using Assets.Scripts.Runtime.Gameplay.EntitiesCore;
using Assets.Scripts.Utilities.Reactive;

namespace Assets.Scripts.Runtime.Gameplay.Features.LevelUpFeature
{
    public class Experience : IEntityComponent
    {
        public ReactiveVariable<float> Value;
    }

    public class Level : IEntityComponent
    {
        public ReactiveVariable<int> Value;
    }
}