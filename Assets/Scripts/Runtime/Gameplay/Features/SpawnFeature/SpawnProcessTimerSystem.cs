using Assets.Scripts.Runtime.Gameplay.EntitiesCore;
using Assets.Scripts.Runtime.Gameplay.EntitiesCore.Systems;
using Assets.Scripts.Utilities.Reactive;

namespace Assets.Scripts.Runtime.Gameplay.Features.SpawnFeature
{
    public class SpawnProcessTimerSystem : IInitializableSystem, IUpdateableSystem
    {
        private ReactiveVariable<float> _initialTime;
        private ReactiveVariable<float> _currentTime;

        private ReactiveVariable<bool> _inProcess;

        public void OnInit(Entity entity)
        {
            _initialTime = entity.SpawnInitialTime;
            _currentTime = entity.SpawnCurrentTime;
            _inProcess = entity.InSpawnProcess;

            _currentTime.Value = 0;
            _inProcess.Value = true;
        }

        public void OnUpdate(float deltaTime)
        {
            if (_inProcess.Value == false)
                return;

            _currentTime.Value += deltaTime;

            if (_currentTime.Value > _initialTime.Value)
                _inProcess.Value = false;
        }
    }
}