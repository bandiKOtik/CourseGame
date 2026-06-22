using UnityEngine;

namespace Assets.Scripts.Configs.Gameplay.Loot
{
    [CreateAssetMenu(menuName = "Configs/Gameplay/Loot/HealthLoot", fileName = "HealthLootConfig")]
    public class HealthLootConfig : LootConfig
    {
        [field: SerializeField] public float Health { get; private set; }
    }
}