using Assets.Scripts.Runtime.Gameplay.Features.StatFeature;
using System;
using UnityEngine;

namespace Assets.Scripts.Configs.Gameplay.Abilities
{
    [CreateAssetMenu(
        menuName = "Configs/Gameplay/Abilities/StatChangeAbilityConfig",
        fileName = "StatChangeAbilityConfig")]
    public class StatChangeAbilityConfig : AbilityConfig
    {
        [field: SerializeField] public StatTypes StatTypes { get; private set; }

        [SerializeField] private StatChangeOperation _operaton;
        [SerializeField] private float _value;

        public Func<float, float> GetApplyEffect()
        {
            switch (_operaton)
            {
                case StatChangeOperation.Add:
                    return stat => stat += _value;

                case StatChangeOperation.Multiply:
                    return stat => stat *= _value;

                default:
                    throw new NotImplementedException("Not implemented operation type");
            }
        }

        private enum StatChangeOperation
        {
            Add,
            Multiply
        }
    }
}