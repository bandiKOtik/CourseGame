using Assets.Scripts.Configs.Gameplay.Abilities;
using Assets.Scripts.Runtime.Gameplay.EntitiesCore;
using System.Collections.Generic;
using System.Linq;

namespace Assets.Scripts.Runtime.Gameplay.Features.AbilitiesDropingFeature
{
    public class AbilityDropingService
    {
        private readonly AbilitiesConfigsContainer _abilitiesContainer;
        private readonly AbilityDropingRulesService _rules;

        public AbilityDropingService(AbilitiesConfigsContainer abilitiesContainer, AbilityDropingRulesService rules)
        {
            _abilitiesContainer = abilitiesContainer;
            _rules = rules;
        }

        public List<AbilityConfig> Drop(int count, Entity entity)
        {
            List<AbilityConfig> avaiableList
                = new List<AbilityConfig>(
                    _abilitiesContainer
                    .AbilityConfigs
                    .Where(option => _rules.IsAvailable(option, entity)));

            List<AbilityConfig> selectedList = new();

            for (int i = 0; i < count; i++)
            {
                AbilityConfig selectedAbility = avaiableList[UnityEngine.Random.Range(0, avaiableList.Count)];
                selectedList.Add(selectedAbility);
                avaiableList.Remove(selectedAbility);
            }

            return selectedList;
        }
    }
}