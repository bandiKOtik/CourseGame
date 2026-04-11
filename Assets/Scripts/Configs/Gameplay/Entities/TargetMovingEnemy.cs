using Assets.Scripts.Utilities.Simple;
using UnityEngine;

namespace Assets.Scripts.Configs.Gameplay.Entities
{
    [CreateAssetMenu(menuName = "Configs/Gameplay/Entities/TargetMovingEnemy", fileName = "TargetMovingEnemy")]
    public class TargetMovingEnemy : EntityConfig
    {
        [field: SerializeField] public string PrefabPath { get; private set; } = "Entities/GameplayEnemies/TargetMovingEnemy";
        [field: SerializeField, Min(0)] public float MoveSpeed { get; private set; } = ConstValues.BaseMovementSpeed;
        [field: SerializeField, Min(0)] public float RotationSpeed { get; private set; } = ConstValues.BaseRotationSpeed;
        [field: SerializeField, Min(0)] public float MaxHealth { get; private set; } = 2;
        [field: SerializeField, Min(0)] public float BodyContactDamage { get; private set; } = 1;
    }
}
