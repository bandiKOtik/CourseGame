using Assets.Scripts.Utilities.Simple;
using UnityEngine;

namespace Assets.Scripts.Configs.Gameplay.Entities
{
    [CreateAssetMenu(menuName = "Configs/Gameplay/Entities/MainBaseConfig", fileName = "MainBaseConfig")]
    public class DefendableBuildingConfig : ScriptableObject
    {
        [field: SerializeField] public string PrefabPath { get; private set; } = "Entities/Objects/DefendableBuilding";
        [field: SerializeField, Min(0)] public float MaxHealth { get; private set; } = 2;
    }
}
