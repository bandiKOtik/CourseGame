using Assets.Scripts.Runtime.Gameplay;
using Assets.Scripts.Utilities.Sequence;
using UnityEngine;

namespace Assets.Scripts.Infrastructure.Configs
{
    [CreateAssetMenu(menuName = "Configs/Gameplay/GameMode", fileName = "GameModeConfig")]
    public class GameModeConfig : ScriptableObject
    {
        [field: SerializeField] public GameMode Current { get; private set; }
        [field: SerializeField] public SequenceGenerationConfig SequenceConfig { get; private set; }
    }
}