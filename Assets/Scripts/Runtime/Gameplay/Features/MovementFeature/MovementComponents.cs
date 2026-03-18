using Assets.Scripts.Runtime.Gameplay.EntitiesCore;
using Assets.Scripts.Utilities.Conditions;
using Assets.Scripts.Utilities.Reactive;
using UnityEngine;

namespace Assets.Scripts.Runtime.Gameplay.Features.MovementFeature
{
    public class CanMove : IEntityComponent
    {
        public ICompositeCondition Value;
    }

    public class MoveDirection : IEntityComponent
    {
        public ReactiveVariable<Vector3> Value;
    }

    public class MoveSpeed : IEntityComponent
    {
        public ReactiveVariable<float> Value;
    }

    public class IsMoving : IEntityComponent
    {
        public ReactiveVariable<bool> Value;
    }
}