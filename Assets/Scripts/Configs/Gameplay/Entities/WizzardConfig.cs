using UnityEngine;

namespace Assets.Scripts.Configs.Gameplay.Entities
{
    [CreateAssetMenu(menuName = "Configs/Gameplay/Entities/WizzardConfig", fileName = "WizzardConfig")]
    public class WizzardConfig : EntityConfig
    {
        [field: SerializeField] public string PrefabPath { get; set; } = "Entities/GameplayEnemies/Wizzard";
        [field: SerializeField, Min(0)] public float MaxHealth { get; private set; } = 3;
        [field: SerializeField, Min(0)] public float DeathProcessTime { get; private set; } = 1;
    }
}
