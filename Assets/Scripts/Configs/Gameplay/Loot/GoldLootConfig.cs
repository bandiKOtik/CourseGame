using UnityEngine;

namespace Assets.Scripts.Configs.Gameplay.Loot
{
    [CreateAssetMenu(menuName = "Configs/Gameplay/Loot/GoldLoot", fileName = "GoldLootConfig")]
    public class GoldLootConfig : LootConfig
    {
        [field: SerializeField] public int Gold { get; private set; }
    }
}