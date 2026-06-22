using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Configs.Gameplay
{
    [CreateAssetMenu(menuName = "Configs/Gameplay/ExperienceConfig", fileName = "ExperienceConfig")]
    public class ExperienceForUpgradeLevelConfig : ScriptableObject
    {
        [SerializeField] private List<float> _experienceForLevel;

        public int MaxLevel => _experienceForLevel.Count;

        public float GetExperienceFor(int level) => _experienceForLevel[level - 1];
    }
}