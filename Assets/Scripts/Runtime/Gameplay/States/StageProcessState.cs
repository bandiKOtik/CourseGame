using Assets.Scripts.Runtime.Gameplay.Features.StagesFeature;
using Assets.Scripts.Utilities.StateMachineCore;

namespace Assets.Scripts.Runtime.Gameplay.States
{
    public class StageProcessState : State, IUpdateableState
    {
        private StageProviderService _stageProvider;

        public StageProcessState(StageProviderService stageProvider)
        {
            _stageProvider = stageProvider;
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

            _stageProvider.CleanUpCurrent();
        }
    }
}
