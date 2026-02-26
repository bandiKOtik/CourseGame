using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Configs.Gameplay.Levels
{
    [CreateAssetMenu(menuName = "Configs/Gameplay/Levels/LevelsListConfig", fileName = "LevelsListConfig")]
    public class LevelsListConfig : ScriptableObject
    {
        [SerializeField] private List<LevelConfig> _levels;

        public IReadOnlyList<LevelConfig> Levels => _levels;

        public LevelConfig GetLevelByNumber(int value) => _levels[value - 1];
    }
}
