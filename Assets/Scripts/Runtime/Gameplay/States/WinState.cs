using Assets.Scripts.Infrastructure.Gameplay;
using Assets.Scripts.Meta.Features.LevelsProgression;
using Assets.Scripts.Runtime.Gameplay.Features.InputManagement;
using Assets.Scripts.Utilities.CoroutinesManagement;
using Assets.Scripts.Utilities.DataManagement.DataProviders;
using Assets.Scripts.Utilities.SceneManagement;
using Assets.Scripts.Utilities.StateMachineCore;
using UnityEngine;

namespace Assets.Scripts.Runtime.Gameplay.States
{
    public class WinState : EndgameState, IUpdateableState
    {
        private readonly LevelsProgressionService _levelsProgression;
        private readonly GameplayInputArgs _args;
        private readonly PlayerDataProvider _dataProvider;
        private readonly SceneSwitcherService _sceneSwitcher;
        private ICoroutinesPerformer _performer;

        public WinState(
            GameplayInputArgs args,
            LevelsProgressionService levelsProgression,
            PlayerDataProvider dataProvider,
            SceneSwitcherService sceneSwitcher,
            ICoroutinesPerformer performer,
            IInputService inputService) : base (inputService)
        {
            _levelsProgression = levelsProgression;
            _args = args;
            _dataProvider = dataProvider;
            _sceneSwitcher = sceneSwitcher;
            _performer = performer;
        }

        public override void Enter()
        {
            base.Enter();

            Debug.LogWarning("You won! Click \"Q\" to exit!");

            _levelsProgression.AddLevelToCompleted(_args.LevelNumber);

            _performer.StartPerform(_dataProvider.SaveAsync());
        }

        public void Update(float deltaTime)
        {
            if (Input.GetKeyDown(KeyCode.Q))
                _performer.StartPerform(_sceneSwitcher.SwitchAsync(Scenes.MainMenu));
        }
    }
}
