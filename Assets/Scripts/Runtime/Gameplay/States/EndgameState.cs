using Assets.Scripts.Runtime.Gameplay.Features.InputManagement;
using Assets.Scripts.Utilities.StateMachineCore;

namespace Assets.Scripts.Runtime.Gameplay.States
{
    public abstract class EndgameState : State
    {
        private readonly IInputService _inputService;

        protected EndgameState(IInputService inputService)
        {
            _inputService = inputService;
        }

        public override void Enter()
        {
            base.Enter();

            _inputService.IsEnabled = false;
        }

        public override void Exit()
        {
            base.Exit();

            _inputService.IsEnabled = true;
        }
    }
}
