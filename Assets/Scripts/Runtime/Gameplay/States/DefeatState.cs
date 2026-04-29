using Assets.Scripts.Meta;
using Assets.Scripts.Runtime.Gameplay.Features.InputManagement;
using Assets.Scripts.Runtime.UI.Gameplay;
using Assets.Scripts.Utilities.CoroutinesManagement;
using Assets.Scripts.Utilities.DataManagement.DataProviders;
using Assets.Scripts.Utilities.SceneManagement;
using Assets.Scripts.Utilities.StateMachineCore;
using UnityEngine;

namespace Assets.Scripts.Runtime.Gameplay.States
{
    public class DefeatState : EndgameState, IUpdateableState
    {
        private readonly StatisticManageService _statistics;
        private readonly PlayerDataProvider _dataProvider;
        private ICoroutinesPerformer _performer;

        private readonly GameplayPopupService _popupService;

        public DefeatState(
            StatisticManageService statistics,
            PlayerDataProvider provider,
            ICoroutinesPerformer performer,
            IInputService inputService,
            GameplayPopupService popupService) : base(inputService)
        {
            _statistics = statistics;
            _dataProvider = provider;
            _performer = performer;
            _popupService = popupService;
        }

        public override void Enter()
        {
            base.Enter();

            _statistics.DefeatRewards();

            _performer.StartPerform(_dataProvider.SaveAsync());

            _popupService.OpenDefeatPopup();
        }

        public void Update(float deltaTime)
        {
        }
    }
}