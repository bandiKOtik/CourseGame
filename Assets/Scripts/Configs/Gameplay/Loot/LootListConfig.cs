using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Configs.Gameplay.Loot
{
    [CreateAssetMenu(menuName = "Configs/Gameplay/Loot/LootList", fileName = "LootListConfig")]
    public class LootListConfig : ScriptableObject
    {
        [SerializeField] private List<LootConfig> _lootConfigs;

        public IReadOnlyList<LootConfig> LootConfigs => _lootConfigs;

        public LootConfig GetLootBy(string id) => _lootConfigs.First(loot => loot.ID == id);
    }
}