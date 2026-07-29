using Assets.Scripts.Configs.Gameplay.Levels;
using Assets.Scripts.Configs.Gameplay.Levels.Stages;
using Assets.Scripts.Utilities.Reactive;
using UnityEngine;

namespace Assets.Scripts.Runtime.Gameplay.Features.StagesFeature
{
    public class StageProgress
    {
        private readonly LevelConfig _levelConfig;
        private readonly ReactiveVariable<int> _stageNumber = new();
        private readonly ReactiveVariable<StageResults> _result = new();

        public StageProgress(LevelConfig levelConfig)
        {
            _levelConfig = levelConfig;
        }

        public IReadOnlyVariable<int> CurrentStageNumber => _stageNumber;
        public IReadOnlyVariable<StageResults> CurrentStageResult => _result;
        public int StagesCount => _levelConfig.StageConfigs.Count;
        public bool HasNextStage => _stageNumber.Value < StagesCount;

        public StageConfig CurrentStageConfig => _levelConfig.StageConfigs[_stageNumber.Value - 1];

        public void Advance()
        {
            _stageNumber.Value++;
            _result.Value = StageResults.Uncompleted;
        }

        public void Complete() => _result.Value = StageResults.Completed;
    }
}
