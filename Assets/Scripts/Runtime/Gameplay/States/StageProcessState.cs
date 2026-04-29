using Assets.Scripts.Meta.Features.Wallet;
using Assets.Scripts.Runtime.Gameplay.Features.StagesFeature;
using Assets.Scripts.Utilities.StateMachineCore;
using System.Collections.Generic;

namespace Assets.Scripts.Runtime.Gameplay.States
{
    public class StageProcessState : State, IUpdateableState
    {
        private readonly StageProviderService _stageProvider;
        private readonly WalletService _walletService;
        private IReadOnlyDictionary<CurrencyTypes, int> _winCashback;

        public StageProcessState(
            StageProviderService stageProvider,
            WalletService wallet,
            IReadOnlyDictionary<CurrencyTypes, int> winCashback)
        {
            _stageProvider = stageProvider;
            _walletService = wallet;
            _winCashback = winCashback;
        }

        public override void Enter()
        {
            base.Enter();

            _stageProvider.SwitchToNext();
            _stageProvider.StartCurrent();
        }

        public void Update(float deltaTime)
        {
            _stageProvider.UpdateCurrent(deltaTime);
        }

        public override void Exit()
        {
            base.Exit();

            foreach (var cashback in _winCashback)
                _walletService.Append(cashback.Key, cashback.Value);

            _stageProvider.CleanUpCurrent();
        }
    }
}