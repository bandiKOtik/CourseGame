using System;

namespace Assets.Scripts.Runtime.Gameplay.Features.AI
{
    public interface IBrain : IDisposable
    {
        void Enable();

        void Disable();

        void Update(float deltaTime);
    }
}