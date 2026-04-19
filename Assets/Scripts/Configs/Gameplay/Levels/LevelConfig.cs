using Assets.Scripts.Configs.Gameplay.Levels.Stages;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Configs.Gameplay.Levels
{
    [CreateAssetMenu(menuName = "Configs/Gameplay/Levels/LevelConfig", fileName = "LevelConfig")]
    public class LevelConfig : ScriptableObject
    {
        [field: SerializeField] public float MainBaseHealth { get; private set; } = 3;

        [SerializeField] private List<StageConfig> _stageConfigs;
        public List<StageConfig> StageConfigs => _stageConfigs;
    }
}