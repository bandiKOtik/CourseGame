using Assets.Scripts.Meta;
using Assets.Scripts.Runtime.Gameplay.Features.InputManagement;
using Assets.Scripts.Runtime.UI.Gameplay;
using Assets.Scripts.Utilities.CoroutinesManagement;
using Assets.Scripts.Utilities.DataManagement.DataProviders;
using Assets.Scripts.Utilities.StateMachineCore;
using UnityEngine;

namespace Assets.Scripts.Runtime.Gameplay.States
{
    public class WinState : EndgameState, IUpdateableState
    {
        private readonly StatisticManageService _statistics;
        private readonly PlayerDataProvider _dataProvider;
        private readonly ICoroutinesPerformer _performer;

        private readonly GameplayPopupService _popupService;

        public WinState(
            StatisticManageService statistics,
            PlayerDataProvider dataProvider,
            ICoroutinesPerformer performer,
            IInputService inputService,
            GameplayPopupService popupService) : base(inputService)
        {
            _statistics = statistics;
            _dataProvider = dataProvider;
            _performer = performer;
            _popupService = popupService;
        }

        public override void Enter()
        {
            base.Enter();

            _statistics.ApplyWinRewards();

            _performer.StartPerform(_dataProvider.SaveAsync());

            _popupService.OpenWinPopup();
        }

        public void Update(float deltaTime)
        {
        }
    }
}