using Assets.Scripts.Runtime.Gameplay.EntitiesCore;
using Assets.Scripts.Runtime.Gameplay.Features.LifeCycle;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Runtime.Gameplay.Features.AI.States
{
    public class WeakestDamageableTargetSelector : ITargetSelector
    {
        private Entity _source;

        public WeakestDamageableTargetSelector(Entity source)
        {
            _source = source;
        }

        public Entity SelectTargetFrom(IEnumerable<Entity> targets)
        {
            IEnumerable<Entity> selectedTargets = targets.Where(target =>
            {
                bool result = target.HasComponent<CurrentHealth>();

                if (target.TryGetCanApplyDamage(out var canApplyDamage))
                    result = result && canApplyDamage.Evaluate();

                result = result && (target != _source);

                return result;
            });

            if (selectedTargets.Any() == false)
                return null;

            Entity colsest = selectedTargets.First();
            float minHealth = float.MaxValue;

            foreach (Entity target in selectedTargets)
            {
                if (target.CurrentHealth.Value < minHealth)
                {
                    minHealth = target.CurrentHealth.Value;
                    colsest = target;
                }
            }
            Debug.Log("Target found!");
            return colsest;
        }
    }
}