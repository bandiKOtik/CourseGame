using Assets.Scripts.Runtime.Gameplay.EntitiesCore;
using Assets.Scripts.Runtime.Gameplay.EntitiesCore.Systems;
using Assets.Scripts.Utilities.Reactive;

namespace Assets.Scripts.Runtime.Gameplay.Features.Capabilities
{
    public class EnergyRechargeSystem : IInitializableSystem, IUpdateableSystem
    {
        public ReactiveVariable<float> _currentEnergy;
        public ReactiveVariable<float> _maxEnergy;
        public ReactiveVariable<float> _chargePerSecond;

        public void OnInit(Entity entity)
        {
            _currentEnergy = entity.CurrentEnergy;
            _maxEnergy = entity.MaxEnergy;
            _chargePerSecond = entity.ChargeAmountPerSecond;

            if (_chargePerSecond.Value < 0)
                throw new System.ArgumentException("Charge Per Second cannot be below zero");
        }

        public void OnUpdate(float deltaTime)
        {
            if (CanRecharge() == false)
                return;

            _currentEnergy.Value += _chargePerSecond.Value * deltaTime;
            //Debug.Log($"{_currentEnergy.Value} / {_maxEnergy.Value}");
        }

        private bool CanRecharge() => _currentEnergy.Value < _maxEnergy.Value;
    }
}