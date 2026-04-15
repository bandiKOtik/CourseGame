using Assets.Scripts.Meta;
using Assets.Scripts.Runtime.Gameplay.Features.InputManagement;
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
        private readonly SceneSwitcherService _sceneSwitcher;
        private ICoroutinesPerformer _performer;

        public DefeatState(
            StatisticManageService statistics,
            PlayerDataProvider provider,
            SceneSwitcherService sceneSwitcher,
            ICoroutinesPerformer performer,
            IInputService inputService) : base(inputService)
        {
            _statistics = statistics;
            _dataProvider = provider;
            _sceneSwitcher = sceneSwitcher;
            _performer = performer;
        }

        public override void Enter()
        {
            base.Enter();

            _statistics.DefeatRewards();

            Debug.LogWarning("You loose! Click \"Q\" to exit!");

            _performer.StartPerform(_dataProvider.SaveAsync());
        }

        public void Update(float deltaTime)
        {
            if (Input.GetKeyDown(KeyCode.Q))
                _performer.StartPerform(_sceneSwitcher.SwitchAsync(Scenes.MainMenu));
        }
    }
}
