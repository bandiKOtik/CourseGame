using UnityEngine;

namespace Assets.Scripts.Runtime.Gameplay.Features.InputManagement
{
    public interface IInputService
    {
        bool IsEnabled { get; set; }
        bool AttackRequest { get; }
        Vector3 Direction { get; }
    }
}