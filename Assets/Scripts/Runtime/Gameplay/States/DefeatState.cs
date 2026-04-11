using Assets.Scripts.Runtime.Gameplay.Features.InputManagement;
using Assets.Scripts.Utilities.CoroutinesManagement;
using Assets.Scripts.Utilities.SceneManagement;
using Assets.Scripts.Utilities.StateMachineCore;
using UnityEngine;

namespace Assets.Scripts.Runtime.Gameplay.States
{
    public class DefeatState : EndgameState, IUpdateableState
    {
        private readonly SceneSwitcherService _sceneSwitcher;
        private ICoroutinesPerformer _performer;

        public DefeatState(
            SceneSwitcherService sceneSwitcher,
            ICoroutinesPerformer performer,
            IInputService inputService) : base(inputService)
        {
            _sceneSwitcher = sceneSwitcher;
            _performer = performer;
        }

        public override void Enter()
        {
            base.Enter();

            Debug.LogWarning("You loose! Click \"Q\" to exit!");
        }

        public void Update(float deltaTime)
        {
            if (Input.GetKeyDown(KeyCode.Q))
                _performer.StartPerform(_sceneSwitcher.SwitchAsync(Scenes.MainMenu));
        }
    }
}
