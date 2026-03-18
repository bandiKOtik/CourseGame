using Assets.Scripts.Runtime.Gameplay.EntitiesCore;
using Assets.Scripts.Utilities.Conditions;
using Assets.Scripts.Utilities.Reactive;
using UnityEngine;

namespace Assets.Scripts.Runtime.Gameplay.Features.MovementFeature
{
    public class MustRequestTeleport : IEntityComponent
    {
        public ICompositeCondition Value;
    }

    public class TeleportRequest : IEntityComponent
    {
        public ReactiveEvent Value;
    }

    public class TeleportEnergyPrice : IEntityComponent
    {
        public ReactiveVariable<float> Value;
    }

    public class TeleportRadius : IEntityComponent
    {
        public ReactiveVariable<float> Value;
    }

    public class PositionFound : IEntityComponent
    {
        public ReactiveEvent<Vector3> Value;
    }

    public class TeleportDestinationAchieved : IEntityComponent
    {
        public ReactiveEvent Value;
    }
}