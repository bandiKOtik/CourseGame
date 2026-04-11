using Assets.Scripts.Utilities.Simple;
using UnityEngine;

namespace Assets.Scripts.Configs.Gameplay.Entities
{
    [CreateAssetMenu(menuName = "Configs/Gameplay/Entities/HeroConfig", fileName = "HeroConfig")]
    public class HeroConfig : EntityConfig
    {
        [field: SerializeField] public string PrefabPath { get; set; } = "Entities/PlayerCharacters/Hero";
        [field: SerializeField, Min(0)] public float MoveSpeed { get; private set; } = ConstValues.BaseMovementSpeed;
        [field: SerializeField, Min(0)] public float RotationSpeed { get; private set; } = ConstValues.BaseRotationSpeed;
        [field: SerializeField, Min(0)] public float AttackProcessTime { get; private set; } = .8f;
        [field: SerializeField, Min(0)] public float AttackDelayTime { get; private set; } = .5f;
        [field: SerializeField, Min(0)] public float AttackCooldown { get; private set; } = .3f;
        [field: SerializeField, Min(0)] public float InstantAttackDamage { get; private set; } = 1;
        [field: SerializeField, Min(0)] public float MaxHealth { get; private set; } = 3;
        [field: SerializeField, Min(0)] public float DeathProcessTime { get; private set; } = 2;
    }
}