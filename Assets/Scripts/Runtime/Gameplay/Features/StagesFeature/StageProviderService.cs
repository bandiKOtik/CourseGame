using Assets.Scripts.Configs.Gameplay.Levels;
using Assets.Scripts.Utilities.Reactive;
using System;

namespace Assets.Scripts.Runtime.Gameplay.Features.StagesFeature
{
    public class StageProviderService : IDisposable
    {
        private StagesFactory _stagesFactory;

        private LevelConfig _levelConfig;
        private ReactiveVariable<int> _stageNumber = new();
        private ReactiveVariable<StageResults> _result = new();

        private IStage _currentStage;

        private IDisposable _stageEndedDisposable;

        public StageProviderService(StagesFactory stagesFactory, LevelConfig levelConfig)
        {
            _stagesFactory = stagesFactory;
            _levelConfig = levelConfig;
        }

        public IReadOnlyVariable<int> CurrentStageNumber => _stageNumber;
        public IReadOnlyVariable<StageResults> CurrentStageResult => _result;
        public int StagesCount => _levelConfig.StageConfigs.Count;

        public void StartCurrent()
        {
            _stageEndedDisposable = _currentStage.Completed.Subscribe(OnStageCompleted);
            _currentStage.Start();
        }

        private void OnStageCompleted() => _result.Value = StageResults.Completed;

        public void UpdateCurrent(float deltaTime) => _currentStage.Update(deltaTime);

        public bool HasNextStage => CurrentStageNumber.Value < StagesCount;

        public void SwitchToNext()
        {
            if (HasNextStage == false)
                throw new InvalidOperationException("Stages count reached");

            if (_currentStage != null)
                CleanUpCurrent();

            _stageNumber.Value++;
            _result.Value = StageResults.Uncompleted;

            _currentStage = _stagesFactory.Create(_levelConfig.StageConfigs[_stageNumber.Value - 1]);
        }

        public void CleanUpCurrent() => _currentStage.CleanUp();

        public void Dispose()
        {
            _currentStage?.Dispose();
            _stageEndedDisposable?.Dispose();
        }
    }
}
