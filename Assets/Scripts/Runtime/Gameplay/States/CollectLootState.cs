using Assets.Scripts.Runtime.Gameplay.Features.LootFeature;
using Assets.Scripts.Runtime.Gameplay.Features.MainBaseBuilding;
using Assets.Scripts.Utilities.StateMachineCore;

namespace Assets.Scripts.Runtime.Gameplay.States
{
    public class CollectLootState : State, IUpdateableState
    {
        private LootPullingService _lootPullingService;
        private MainBaseHolderService _heroHolder;

        public CollectLootState(LootPullingService lootPullingService, MainBaseHolderService heroHolder)
        {
            _lootPullingService = lootPullingService;
            _heroHolder = heroHolder;
        }

        public override void Enter()
        {
            base.Enter();

            _lootPullingService.PullTo(_heroHolder.MainBase);
        }

        public override void Exit()
        {
            base.Exit();

            _lootPullingService.Reset();
        }

        public void Update(float deltaTime)
        {
        }
    }
}