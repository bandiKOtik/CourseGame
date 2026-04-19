using Assets.Scripts.Runtime.Gameplay.EntitiesCore;
using Assets.Scripts.Runtime.Gameplay.Features.DamageFeature;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Runtime.Gameplay.Features.AI.States
{
    public class NearestDamageableTargetSelector : ITargetSelector
    {
        private Entity _source;
        private Transform _sourceTransform;

        public NearestDamageableTargetSelector(Entity source)
        {
            _source = source;
            _sourceTransform = _source.Transform;
        }

        public Entity SelectTargetFrom(IEnumerable<Entity> targets)
        {
            IEnumerable<Entity> selectedTargets = targets.Where(target =>
            {
                bool result = target.HasComponent<TakeDamageRequest>();

                if (target.TryGetCanApplyDamage(out var canApplyDamage))
                    result = result && canApplyDamage.Evaluate();

                if (_source.TryGetTeam(out var sourceTeam) && target.TryGetTeam(out var targetTeam))
                    result = result && (sourceTeam.Value != targetTeam.Value);

                result = result && (target != _source);

                return result;
            });

            if (selectedTargets.Any() == false)
                return null;

            Entity closest = selectedTargets.First();
            float minDistance = GetDistanceTo(closest);

            foreach (Entity target in selectedTargets)
            {
                float distance = GetDistanceTo(target);

                if (distance < minDistance)
                {
                    minDistance = distance;
                    closest = target;
                }
            }

            return closest;
        }

        private float GetDistanceTo(Entity target) => (_sourceTransform.position - target.Transform.position).magnitude;
    }
}