using Assets.Scripts.Runtime.Gameplay.EntitiesCore;
using Assets.Scripts.Runtime.Gameplay.Features.DamageFeature;
using Assets.Scripts.Runtime.Gameplay.Features.TeamsFeature;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Runtime.Gameplay.Features.AI.States
{
    public class EntityTeamTargetSelector : ITargetSelector
    {
        private Entity _source;
        private Transform _sourceTransform;
        private Teams _targetTeam;

        public EntityTeamTargetSelector(Entity source, Teams targetTeam)
        {
            _source = source;
            _sourceTransform = _source.Transform;
            _targetTeam = targetTeam;

            if (_source.TryGetTeam(out var sourceTeam))
                if (sourceTeam.Value == _targetTeam)
                    throw new System.ArgumentException("Source team & target team are the same");
        }

        public Entity SelectTargetFrom(IEnumerable<Entity> targets)
        {
            IEnumerable<Entity> selectedTargets = targets.Where(target =>
            {
                bool result = target.HasComponent<TakeDamageRequest>();

                if (target.TryGetCanApplyDamage(out var canApplyDamage))
                    result = result && canApplyDamage.Evaluate();

                if (target.TryGetTeam(out var targetTeam))
                    result = result && (targetTeam.Value == _targetTeam);

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
