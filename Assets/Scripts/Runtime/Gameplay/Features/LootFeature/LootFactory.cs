using Assets.Scripts.Infrastructure.DI_Container;
using Assets.Scripts.Runtime.Gameplay.EntitiesCore;
using Assets.Scripts.Runtime.Gameplay.EntitiesCore.Factory;
using UnityEngine;

namespace Assets.Scripts.Runtime.Gameplay.Features.LootFeature
{
    public class LootFactory
    {
        private EntitiesFactory _entitiesFactory;
        private EntitiesLifeContext _lifeContext;

        public LootFactory(DIContainer container)
        {
            _entitiesFactory = container.Resolve<EntitiesFactory>();
            _lifeContext = container.Resolve<EntitiesLifeContext>();
        }

        public Entity CreateHealthLoot(string prefabPatrh, Vector3 position, float amount)
        {
            Entity pullableBase = _entitiesFactory.CreatePullable(prefabPatrh, position);

            pullableBase
                .AddCurrentHealth(new(amount))
                .AddSystem(new CollectHealthToTargetSystem());

            _lifeContext.Add(pullableBase);

            return pullableBase;
        }

        public Entity CreateExperienceLoot(string prefabPatrh, Vector3 position, int amount)
        {
            Entity pullableBase = _entitiesFactory.CreatePullable(prefabPatrh, position);

            pullableBase
                .AddExperience(new(amount))
                .AddSystem(new CollectExperienceToTargetSystem());

            _lifeContext.Add(pullableBase);

            return pullableBase;
        }
    }
}