using Assets.Scripts.Utilities.Simple;
using UnityEngine;

namespace Assets.Scripts.Configs.Gameplay.Entities
{
    [CreateAssetMenu(menuName = "Configs/Gameplay/Entities/GhostConfig", fileName = "GhostConfig")]
    public class GhostConfig : EntityConfig
    {
        [field: SerializeField] public string PrefabPath { get; private set; } = "Entities/GameplayEnemies/Ghost";
        [field: SerializeField, Min(0)] public float MoveSpeed { get; private set; } = ConstValues.BaseMovementSpeed;
        [field: SerializeField, Min(0)] public float RotationSpeed { get; private set; } = ConstValues.BaseRotationSpeed;
        [field: SerializeField, Min(0)] public float MaxHealth { get; private set; } = 2;
        [field: SerializeField, Min(0)] public float BodyContactDamage { get; private set; } = 1;
        [field: SerializeField, Min(0)] public float DeathProcessTime { get; private set; } = 1;
    }
}