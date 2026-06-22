using UnityEngine;

namespace Assets.Scripts.Configs.Gameplay.Loot
{
    [CreateAssetMenu(menuName = "Configs/Gameplay/Loot/ExperienceLoot", fileName = "ExperienceLootConfig")]
    public class ExperienceLootConfig : LootConfig
    {
        [field: SerializeField] public int Experience { get; private set; }
    }
}