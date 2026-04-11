using Assets.Scripts.Utilities.Reactive;
using System;

namespace Assets.Scripts.Runtime.Gameplay.Features.StagesFeature
{
    public interface IStage : IDisposable
    {
        IReadOnlyEvent Completed {  get; }
        void Start();
        void Update(float deltaTime);
        void CleanUp();
    }
}
