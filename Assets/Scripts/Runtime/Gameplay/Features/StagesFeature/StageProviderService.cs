using Assets.Scripts.Configs.Gameplay.Levels;
using Assets.Scripts.Utilities.Reactive;
using System;

namespace Assets.Scripts.Runtime.Gameplay.Features.StagesFeature
{
    public class StageProviderService : IDisposable
    {
        private readonly StagesFactory _stagesFactory;
        private readonly StageProgress _progress;

        private IStage _currentStage;
        private IDisposable _stageEndedDisposable;

        public StageProviderService(StagesFactory stagesFactory, StageProgress progress)
        {
            _stagesFactory = stagesFactory;
            _progress = progress;
        }

        public IReadOnlyVariable<int> CurrentStageNumber => _progress.CurrentStageNumber;
        public IReadOnlyVariable<StageResults> CurrentStageResult => _progress.CurrentStageResult;
        public int StagesCount => _progress.StagesCount;
        public bool HasNextStage => _progress.HasNextStage;

        public void StartCurrent()
        {
            _stageEndedDisposable = _currentStage.Completed.Subscribe(OnStageCompleted);
            _currentStage.Start();
        }

        private void OnStageCompleted() => _progress.Complete();

        public void UpdateCurrent(float deltaTime) => _currentStage.Update(deltaTime);

        public void SwitchToNext()
        {
            if (HasNextStage == false)
                throw new InvalidOperationException("Stages count reached");

            if (_currentStage != null)
                CleanUpCurrent();

            _progress.Advance();
            _currentStage = _stagesFactory.Create(_progress.CurrentStageConfig);
        }

        public void CleanUpCurrent() => _currentStage.CleanUp();

        public void Dispose()
        {
            _currentStage?.Dispose();
            _stageEndedDisposable?.Dispose();
        }
    }
}