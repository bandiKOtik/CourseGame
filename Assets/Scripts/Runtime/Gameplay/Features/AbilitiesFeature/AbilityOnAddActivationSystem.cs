using Assets.Scripts.Runtime.Gameplay.EntitiesCore;
using Assets.Scripts.Runtime.Gameplay.EntitiesCore.Systems;

namespace Assets.Scripts.Runtime.Gameplay.Features.AbilitiesFeature
{
    public class AbilityOnAddActivationSystem : IInitializableSystem, IDisposableSystem
    {
        private AbilitiesList _abilitiesList;

        public void OnInit(Entity entity)
        {
            _abilitiesList = entity.Abilities;

            _abilitiesList.Added += OnAbilityAdded;

            foreach (var ability in _abilitiesList.Elements)
                ability.Activate();
        }

        public void OnDispose()
        {
            _abilitiesList.Added -= OnAbilityAdded;
        }

        private void OnAbilityAdded(Ability ability) => ability.Activate();
    }
}