using Assets.Scripts.Utilities.Reactive;

namespace Assets.Scripts.Runtime.Gameplay.Features.StagesFeature
{
    public class PreperationInputService
    {
        private ReactiveVariable<bool> _isTriggered = new();

        public ReactiveVariable<bool> IsReady => _isTriggered;

        public void SetReady() => _isTriggered.Value = true;

        public void CleanUp() => _isTriggered.Value = false;
    }
}