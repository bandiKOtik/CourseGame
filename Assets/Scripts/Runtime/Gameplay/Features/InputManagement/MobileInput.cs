using UnityEngine;

namespace Assets.Scripts.Runtime.Gameplay.Features.InputManagement
{
    public class MobileInput : IInputService
    {
        public bool IsEnabled { get; set; }
        public Vector3 Direction { get; }

        public bool AttackRequest => throw new System.NotImplementedException();
    }
}