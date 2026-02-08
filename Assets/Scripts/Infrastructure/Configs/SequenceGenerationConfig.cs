using UnityEngine;

namespace Assets.Scripts.Utilities.Sequence
{
    [CreateAssetMenu(menuName = "Configs/Gameplay/SequenceGeneration", fileName = "SequenceGenerationConfig")]
    public class SequenceGenerationConfig : ScriptableObject
    {
        [field: SerializeField] public string Sequence { get; private set; }
        [field: SerializeField, Min(1)] public int MinLength { get; private set; } = 1;
        [field: SerializeField, Min(1)] public int MaxLength { get; private set; } = 1;
    }
}