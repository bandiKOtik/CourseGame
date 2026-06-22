using System;
using System.Collections.Generic;

namespace Assets.Scripts.Runtime.Gameplay.Features.StatFeature
{
    public class StatsEffectList
    {
        public event Action<IStatsEffect> Added;

        public event Action<IStatsEffect> Removed;

        private List<IStatsEffect> _elements = new();

        public IReadOnlyList<IStatsEffect> Elements => _elements;

        public virtual void Add(IStatsEffect element)
        {
            _elements.Add(element);
            Added?.Invoke(element);
        }

        public virtual void Remove(IStatsEffect element)
        {
            _elements.Remove(element);
            Removed?.Invoke(element);
        }
    }
}