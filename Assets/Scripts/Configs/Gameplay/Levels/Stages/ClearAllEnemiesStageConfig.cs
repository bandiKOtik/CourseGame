using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Configs.Gameplay.Levels.Stages
{
    [CreateAssetMenu(menuName = "Configs/Gameplay/Stages/ClearAllEnemiesStage",
        fileName = "ClearAllEnemiesStage")]
    public class ClearAllEnemiesStageConfig : StageConfig
    {
        [SerializeField] private List<EnemyItemConfig> _enemyItems;
        public IReadOnlyList<EnemyItemConfig> EnemyItems => _enemyItems;
    }
}
