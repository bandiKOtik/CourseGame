using Assets.Scripts.Configs.Gameplay.Entities;
using System;
using UnityEngine;

namespace Assets.Scripts.Configs.Gameplay.Levels.Stages
{
    [Serializable]
    public class EnemyItemConfig
    {
        [field: SerializeField] public Vector3 SpawnPosition {  get; private set; }
        [field: SerializeField] public EntityConfig EnemyConfig { get; private set; }
    }
}
