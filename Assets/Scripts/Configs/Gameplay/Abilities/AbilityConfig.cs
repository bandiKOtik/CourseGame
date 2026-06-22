using UnityEngine;

namespace Assets.Scripts.Configs.Gameplay.Abilities
{
    public class AbilityConfig : ScriptableObject
    {
        [field: SerializeField] public string ID { get; private set; }

        [field: SerializeField] public string Name { get; private set; }
        [field: SerializeField] public string Description { get; private set; }
        [field: SerializeField] public Sprite Icon { get; private set; }
    }
}