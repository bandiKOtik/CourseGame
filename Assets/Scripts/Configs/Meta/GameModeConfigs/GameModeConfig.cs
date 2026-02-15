using Assets.Scripts.Runtime.Gameplay;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Configs.Meta.GameModeConfigs
{
    [CreateAssetMenu(menuName = "Configs/Gameplay/GameMode", fileName = "GameModeConfig")]
    public class GameModeConfig : ScriptableObject
    {
        [SerializeField] private List<SequenceType> _sequenceTypes;

        public SequenceGenerationConfig GetSequenceConfig(GameMode mode)
        {
            var config = _sequenceTypes.First(config => config.ModeFor == mode).SequenceConfig;

            if (config == null)
                throw new NullReferenceException("No configs for mode: " + nameof(mode));

            return config;
        }

        [Serializable]
        private class SequenceType
        {
            [field: SerializeField] public GameMode ModeFor { get; private set; }
            [field: SerializeField] public SequenceGenerationConfig SequenceConfig { get; private set; }
        }
    }
}