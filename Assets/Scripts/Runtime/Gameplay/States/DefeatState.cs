using Assets.Scripts.Meta;
using Assets.Scripts.Runtime.Gameplay.Features.InputManagement;
using Assets.Scripts.Runtime.Gameplay.Features.PauseFeature;
using Assets.Scripts.Runtime.UI.Gameplay;
using Assets.Scripts.Utilities.DataManagement.DataProviders;
using Assets.Scripts.Utilities.StateMachineCore;
using Cysharp.Threading.Tasks;

namespace Assets.Scripts.Runtime.Gameplay.States
{
    public class DefeatState : EndgameState, IUpdateableState
    {
        private readonly StatisticManageService _statistics;
        private readonly PlayerDataProvider _dataProvider;

        private readonly GameplayPopupService _popupService;

        public DefeatState(
            StatisticManageService statistics,
            PlayerDataProvider provider,
            IInputService inputService,
            GameplayPopupService popupService,
            IPauseService pauseService) : base(inputService, pauseService)
        {
            _statistics = statistics;
            _dataProvider = provider;
            _popupService = popupService;
        }

        public override void Enter()
        {
            base.Enter();

            _statistics.DefeatRewards();

            _dataProvider.SaveAsync().Forget();

            _popupService.OpenDefeatPopup();
        }

        public void Update(float deltaTime)
        {
        }
    }
}