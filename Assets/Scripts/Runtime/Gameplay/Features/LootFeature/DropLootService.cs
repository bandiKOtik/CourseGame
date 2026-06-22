using Assets.Scripts.Configs.Gameplay.Loot;
using Assets.Scripts.Runtime.Gameplay.EntitiesCore;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Runtime.Gameplay.Features.LootFeature
{
    public class DropLootService
    {
        private LootListConfig _listConfig;
        private LootFactory _lootFactory;

        public DropLootService(LootListConfig listConfig, LootFactory lootFactory)
        {
            _listConfig = listConfig;
            _lootFactory = lootFactory;
        }

        public void DropLootFor(Entity entity)
        {
            Transform entityTransform = entity.Transform;

            List<ExperienceLootConfig> expConfigs = _listConfig.LootConfigs
                .Where(loot => loot.GetType() == typeof(ExperienceLootConfig))
                .Cast<ExperienceLootConfig>()
                .ToList();

            if (expConfigs.Count > 0)
                DropExp(entityTransform.position, expConfigs[Random.Range(0, expConfigs.Count)]);

            DropHealth(entityTransform.position);
        }

        private void DropExp(Vector3 position, ExperienceLootConfig experienceLootConfig)
        {
            int expInOnePotion = 300;

            if (experienceLootConfig.Experience < expInOnePotion)
            {
                _lootFactory.CreateExperienceLoot(experienceLootConfig.PrefabPath, position, experienceLootConfig.Experience);
            }
            else
            {
                int restExp = experienceLootConfig.Experience % expInOnePotion;

                int potionsNumber = (experienceLootConfig.Experience - restExp) / expInOnePotion;

                for (int i = 0; i < potionsNumber; i++)
                    _lootFactory.CreateExperienceLoot(experienceLootConfig.PrefabPath, position, expInOnePotion);
            }
        }

        private void DropHealth(Vector3 position)
        {
            List<HealthLootConfig> healthConfigs = _listConfig.LootConfigs
                .Where(loot => loot.GetType() == typeof(HealthLootConfig))
                .Cast<HealthLootConfig>()
                .ToList();

            if (healthConfigs.Count > 0 && Random.Range(0, 100) > 50)
            {
                var healthLootConfig = healthConfigs[Random.Range(0, healthConfigs.Count)];

                _lootFactory.CreateHealthLoot(healthLootConfig.PrefabPath, position, healthLootConfig.Health);
            }
        }
    }
}