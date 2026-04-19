using Assets.Scripts.Runtime.Gameplay.EntitiesCore;
using Assets.Scripts.Runtime.Gameplay.Features.LifeCycle;
using System.Collections.Generic;
using System.Linq;

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

                if (target.TryGetTransform(out var transform))
                    result = result && transform != null;

                if (_source.TryGetTeam(out var sourceTeam) && target.TryGetTeam(out var targetTeam))
                    result = result && (sourceTeam.Value != targetTeam.Value);

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

            return colsest;
        }
    }
}