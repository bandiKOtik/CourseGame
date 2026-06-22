using UnityEngine;

namespace Assets.Scripts.Configs.Gameplay.Loot
{
    public abstract class LootConfig : ScriptableObject
    {
        [field: SerializeField] public string ID { get; private set; }
        [field: SerializeField] public string PrefabPath { get; private set; }
    }
}